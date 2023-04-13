using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using System.Xml;
using System.IO;
using System.Xml.Linq;
using TNXMLUtility;

namespace Reliability_Desk
{

    public partial class relDesk : Form
    {
        private DataSet currentData;
        private DataTable activePartList;
        //private assembly mainProject;
        private project mainProject;
        private string projectFileName;
        public relDesk()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            //statusStrip.Text = "Application Started";
            //Add images to tree view from assembly and parts only
            panelProperties.Hide();
            copyNode = null;
            tableLayoutPanel1.Dock = DockStyle.Fill;
            imageListProjectTree = new ImageList();
            imageListProjectTree.Images.Add(Image.FromFile("../../Asm.png"));
            imageListProjectTree.Images.Add(Image.FromFile("../../Prt.png"));
            imageListProjectTree.Images.Add(Image.FromFile("../../Prj.png"));
            imageListProjectTree.Images.Add(Image.FromFile("../../Pro.png"));
            projectTree.ImageList = imageListProjectTree;
            statuslabel.Text = " Program started";
            statusStrip.Refresh();            
            textBox.Text = textBox.Text + "program started \n";
            //projectFileName = "./myTestFile1.xml";
            projectFileName = "./updateProject.xml";
            mainProject = new project(projectFileName);            
            projectTree.Nodes.Clear();
            projectTree.Nodes.Add(mainProject.getNode());
            projectTree.ExpandAll();
            loadPartlistToolStripMenuItem_Click(sender, e);
        }

        private void menuMain_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("version 0.1", "About", MessageBoxButtons.OK);
        }

        private void projectTree_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            // this function displays the right click menu of nodes in project tree
            if (e.Button == MouseButtons.Right)
            {
                statuslabel.Text = e.Node.ToString();
                statusStrip.Refresh();
                //Setting x,y parameters from current cursor as default menu position is  top left corner
                treeNodeMenu.Show(Cursor.Position.X, Cursor.Position.Y);
                projectTree.SelectedNode = e.Node;
            }
            try
            {
                statuslabel.Text = projectTree.SelectedNode.Name + ":" + projectTree.SelectedNode.Text + " selected";
                statusStrip.Refresh();
            }
            catch(Exception exp)
            {
                statuslabel.Text = "Node Selected";
                statusStrip.Refresh();
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //Adding subassembly context menu
            store storeInstance = store.instance();
            textBox.Text += storeInstance.count();
            TreeNode tn = projectTree.SelectedNode;
            string st = tn.Text.Trim();
            while (tn.Parent != null)
            {
                st = tn.Parent.Text.Trim() + "," + st;
                tn = tn.Parent;
            }
            //MessageBox.Show(st);
            assembly a = mainProject.findAssembly(st);
            assembly aNew = new assembly("new subassembly" + projectTree.Nodes.Count.ToString(), a.getFullPath());
            storeInstance.add(aNew);
            a.addAssembly(aNew);
            textBox.Text += storeInstance.count();

            projectTree.Nodes.Clear();
            projectTree.Nodes.Add(mainProject.getNode());
            projectTree.ExpandAll();
            statuslabel.Text = "New assembly " + "new subassembly" + projectTree.Nodes.Count.ToString() + " added";
            statusStrip.Refresh();
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            //removing subassembly menu
            // note with this version of program all the child assemblies will be deleted as well
            TreeNode tn = projectTree.SelectedNode;
            string nodeName = tn.Name + ":" + tn.Text;
            string st;
            st = tn.Text.Trim();
            while (tn.Parent != null)
            {
                st = tn.Parent.Text.Trim() + "," + st;
                tn = tn.Parent;
            }
            mainProject.deleteItem(st);
            projectTree.Nodes.Clear();
            projectTree.Nodes.Add(mainProject.getNode());
            statuslabel.Text = nodeName + " deleted";
            statusStrip.Refresh();
        }

        private void renameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //renaming of node             
            string name_old = projectTree.SelectedNode.Text;
            string nameText = Prompt.ShowDialog("Enter New Name: ", "Rename");
            if (nameText.Length != 0)
            {
                TreeNode tn = projectTree.SelectedNode;
                string st = tn.Text.Trim();
                while (tn.Parent != null)
                {
                    st = tn.Parent.Text.Trim() + "," + st;
                    tn = tn.Parent;
                }
                st = st.Substring(0, st.LastIndexOf(','));
                mainProject.renameSub(nameText, name_old, st);
                projectTree.Nodes.Clear();
                projectTree.Nodes.Add(mainProject.getNode());
                projectTree.ExpandAll();

                statuslabel.Text = "renamed to " + nameText;
                statusStrip.Refresh();
            }
        }
        private void displayTree(ref RichTextBox textbox, List<XmlNode> parent, int level)
        {
            // only verbose for displaying the project tree this function runs recursively
            foreach (var node in parent)
            {
                textbox.Text = textbox.Text + level.ToString() + "\t" + node.Name + ":";
                textbox.Text = textbox.Text + node.Value + "\t";
                //textbox.Text = textbox.Text + node.Attributes.Count;
                textbox.Text = textbox.Text + "\n";
                if (node.HasChildNodes)
                {
                    displayTree(ref textBox, node.ChildNodes.Cast<XmlNode>().ToList(), level + 1);
                }
            }
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            //Add part
            projectTree.BeginUpdate();
            if (projectTree.SelectedNode != null)
            {

                projectTree.SelectedNode.Nodes.Add("Part", "new part" + projectTree.Nodes.Count.ToString(), 1, 1);
                statuslabel.Text = "Added new part successfully";
                statusStrip.Refresh();
            }
            projectTree.EndUpdate();
        }

        private void saveProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Save the project in xml type of file
            string fileName = "myproject.prj";
            // this stream create the text file for project future versions will enable 
            // program to be loaded from this file. Current version will only support loading
            // from xml type of files
            FileStream fs = new FileStream(fileName, FileMode.Create); 
            Byte[] bt = new UTF8Encoding(true).GetBytes(fileName);
            fs.Write(bt, 0, bt.Length);
            bt = new UTF8Encoding(true).GetBytes("This is file\n");
            fs.Write(bt, 0, bt.Length);            
            List<TreeNode> parentNode = projectTree.Nodes.Cast<TreeNode>().ToList();
            writeProjctFile(parentNode, ref fs);
            fs.Close();
            fs.Dispose();
            // write project from tree node to xml file
            // only this file is readable in current version
            //XmlWriterSettings xmlsettings = new XmlWriterSettings();
            //xmlsettings.Indent = true;
            //xmlsettings.NewLineOnAttributes = true;
            //XmlWriter xmlwrite = XmlWriter.Create("projectTest3.xml",xmlsettings);
            //xmlwrite.WriteStartDocument();
            //writeXMLfromProject(parentNode, ref xmlwrite);
            //xmlwrite.WriteEndDocument();
            //xmlwrite.Close();
            //xmlwrite.Dispose();            
            
            XElement ele = mainProject.getXML();            
            XDocument doc = new XDocument();
            doc.Add(ele);
            //XmlWriterSettings xmlsettings = new XmlWriterSettings();
            //XmlWriter writer = XmlWriter.Create(mainProject.getFileName(),xmlsettings);
            //xmlsettings.Indent = true;
            //xmlsettings.NewLineOnAttributes = false;
            doc.Save(mainProject.getFileName());
            statuslabel.Text = mainProject.getFileName() + " updated";
            statusStrip.Refresh();
            
        }
        private int writeProjctFile(List<TreeNode> treeNode, ref FileStream fs)
        {
            // write project file and call recursively for child nodes
            foreach (TreeNode tr in treeNode)
            {
                string str = tr.Name + "," + tr.Text + "\n";
                Byte[] bytes = new UTF8Encoding(true).GetBytes(str);
                fs.Write(bytes, 0, bytes.Length);
                if (tr.Nodes != null)
                {
                    List<TreeNode> childList = tr.Nodes.Cast<TreeNode>().ToList();
                    writeProjctFile(childList, ref fs); // call recursively for childs
                }
            }
            return 1;
        }
        private void writeXMLfromProject(List<TreeNode> projectTree, ref XmlWriter xmlwrite)
        {
            // Write xml file using xmlwriter
            // call this function recursively for child nodes
            foreach (TreeNode tr in projectTree)
            {
                xmlwrite.WriteStartElement(tr.Name);
                xmlwrite.WriteString(tr.Text);
                if (tr.Nodes != null)
                {
                    List<TreeNode> childList = tr.Nodes.Cast<TreeNode>().ToList();
                    writeXMLfromProject(childList, ref xmlwrite);  // call recursively for childs
                }
                xmlwrite.WriteEndElement();
            }
        }
        private void writeProjecfromXML(string projectFile)
        {
            // write project tree from xml file
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.IgnoreWhitespace = false;
            XmlReader reader = XmlReader.Create(projectFile, settings);
            print(reader.ToString() + "\n");
            XElement element = XElement.Load(reader);
            projectTree.BeginUpdate();
            projectTree.Nodes.Clear();
            //projectTree.Nodes.Add(element.Name.ToString().Trim(),element.FirstNode.ToString().Trim()); // add top node for project
            //projectTree.SelectedNode = projectTree.TopNode; 
            //IEnumerable<XElement> subtree = element.Elements(); // get childs
            //addChildNodes(subtree); // call this function recursively for child nodes
            projectTree.Nodes.Add(TNXMLUtility.TNXMLUtility.readXML(projectFile));
            projectTree.EndUpdate();
            reader.Close();
            reader.Dispose();
            refreshIcons();
            return;
        }
        private void print(string s)
        {
            textBox.Text = textBox.Text + s;
        }
        private void addChildNodes(IEnumerable<XElement> element)
        {
            // recursive function to read xml file and create project tree
            foreach (XElement x in element) // iterate through elements
            {
                if (x.Name.ToString().Trim().Equals("Assembly"))
                {
                    projectTree.SelectedNode.Nodes.Add(x.Name.ToString().Trim(), x.FirstNode.ToString().Trim(), 0, 0);
                    //select current node for adding childers
                    projectTree.SelectedNode = projectTree.SelectedNode.Nodes[projectTree.SelectedNode.Nodes.Count - 1];
                    if (x.HasElements) // if it has elements add them recursively
                    {
                        addChildNodes(x.Elements());
                    }
                    projectTree.SelectedNode = projectTree.SelectedNode.Parent; // exit from current node add another sibling
                }
                else if (x.Name.ToString().Trim().Equals("Part"))
                {
                    // part should have no childern
                    projectTree.SelectedNode.Nodes.Add(x.Name.ToString().Trim(), x.FirstNode.ToString().Trim(), 1, 1);
                }
            }
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {            
            TreeNode tn = projectTree.SelectedNode;
            string st = "";
            if (tn.Name == "Assembly")
            {
                st = tn.Text.Trim();
                while (tn.Parent != null)
                {
                    st = tn.Parent.Text.Trim() + "," + st;
                    tn = tn.Parent;
                }
            }
            assembly a = mainProject.findAssembly(st);
            Clipboard.Clear();
            Clipboard.SetText(a.getXML().ToString());
            statuslabel.Text = tn.Text + " node copied";
            statusStrip.Refresh();
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(Clipboard.GetText());
            TreeNode tn = projectTree.SelectedNode;
            string st = "";            
            if (tn.Name.ToString().Trim() == "Assembly" || tn.Name == "Project")
            {                
                st = tn.Text.Trim();
                while (tn.Parent != null)
                {
                    st = tn.Parent.Text.Trim() + "," + st;
                    tn = tn.Parent;
                }
            }            
            try
            {
                XElement x = XElement.Parse(Clipboard.GetText());                
                if (x.Name.ToString().Trim()=="Assembly")
                {                    
                    mainProject.addChildAssembly(x, st);
                    projectTree.Nodes.Clear();
                    projectTree.Nodes.Add(mainProject.getNode());
                }

            }
            catch(Exception exp)
            {
                MessageBox.Show(exp.ToString());
            }
            Clipboard.Clear();
            statuslabel.Text = tn.Text + " pasted";
            statusStrip.Refresh();
            
        }

        private void toolStripComboBox1_Click(object sender, EventArgs e)
        {

        }
        private void refreshIcons()
        {
            projectTree.SelectedNode = projectTree.TopNode;
            textBox.Text = textBox.Text + projectTree.SelectedNode.Name + "\t" + projectTree.SelectedNode.Text + "\n";
            while (projectTree.SelectedNode.NextNode != null && projectTree.SelectedNode.FirstNode == null)
            {
                textBox.Text = textBox.Text + projectTree.SelectedNode.Name + "\t" + projectTree.SelectedNode.Text + "\n";
                projectTree.SelectedNode = projectTree.SelectedNode.NextNode;
                if (projectTree.SelectedNode.Name == "Project")
                {
                    projectTree.SelectedNode.ImageIndex = 2;
                }
                else if (projectTree.SelectedNode.Name == "Assembly")
                {
                    projectTree.SelectedNode.ImageIndex = 0;
                }
                else if (projectTree.SelectedNode.Name == "Part")
                {
                    projectTree.SelectedNode.ImageIndex = 1;
                }

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (panelProperties.Dock == DockStyle.None || panelProperties.Dock == DockStyle.Left)
            {
                panelProperties.Dock = DockStyle.Right;
                tableLayoutPanel1.Dock = DockStyle.Left;
                tableLayoutPanel1.CellBorderStyle = TableLayoutPanelCellBorderStyle.OutsetDouble;
            }
            else if (panelProperties.Dock == DockStyle.Right)
            {
                panelProperties.Dock = DockStyle.Left;
                tableLayoutPanel1.Dock = DockStyle.Right;
            }
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void projectTree_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            TreeNode tn = e.Node;
            string st = tn.Text.Trim();
            while (tn.Parent != null)
            {
                st = tn.Parent.Text.Trim() + "," + st;
                tn = tn.Parent;
            }
            projectTree.SelectedNode = e.Node;            
            if (e.Node.Name == "Part")
            {
                panelProperties.Show();
                propertiesTable.Rows.Clear();
                propertiesTable.Columns.Clear();
                propertiesTable.Columns.Add("Field", "Field");
                propertiesTable.Columns.Add("Value", "Value");
                propertiesTable.Rows.Add(e.Node.Name, e.Node.Text);                
                DataRow[] found = activePartList.Select("Name LIKE '%" + e.Node.Text.ToString().Trim() + "%'");
                DataRow displayRow = found[0];
                foreach (string s in globals.dataFields)
                {
                    propertiesTable.Rows.Add(s, displayRow[s]);
                }    
                statuslabel.Text = e.Node.Text + " node selected";
                statusStrip.Refresh();
            }
            else if (e.Node.Name == "Assembly")
            {                
                assembly a = mainProject.findAssembly(st);
                if (a != null)
                {

                    panelProperties.Show();
                    propertiesTable.Rows.Clear();
                    propertiesTable.Columns.Clear();
                    propertiesTable.Columns.Add("Field", "Field");
                    propertiesTable.Columns.Add("Value", "Value");
                    propertiesTable.Rows.Add(e.Node.Name, e.Node.Text);
                    statuslabel.Text = e.Node.Text + " node selected";
                    statusStrip.Refresh();
                }
                else
                {
                    MessageBox.Show("NULL", "mainDesk");
                }
            }
            else
            {
                panelProperties.Hide();
            }
        }

        private void loadPartlistToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //List<part> partList = TNXMLUtility.nodeUtilities.readPartsfromXML("../../partListTest1.xml");
            List<part> partList = TNXMLUtility.nodeUtilities.readPartsfromXML("./PLNew.xml");
            activePartList = new DataTable("activePartList");
            foreach (string s in globals.dataFields)
            {
                activePartList.Columns.Add(s);
            }
            foreach (part p in partList)
            {
                activePartList.Rows.Add(p.getData());
            }
            statuslabel.Text = "active part list loaded";
            statusStrip.Refresh();
        }

        private void addNewPartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            store storeInstance = store.instance();
            textBox.Text += storeInstance.count() + Environment.NewLine;
            if (projectTree.SelectedNode.Name == "Assembly")// || projectTree.SelectedNode.Name == "Project")
            {
                TreeNode tn = projectTree.SelectedNode;
                string st = tn.Text.Trim();
                while (tn.Parent != null)
                {
                    st = tn.Parent.Text.Trim() + "," + st;
                    tn = tn.Parent;
                }
                //MessageBox.Show(st);
                textBox.Text += storeInstance.count() + Environment.NewLine;
                assembly a = storeInstance.findAssembly(st);                
                textBox.Text = "";
                foreach (assembly asm in storeInstance.getAssemblies())
                {
                    textBox.Text += asm.getFullPath() + "\n";
                }
                //selectPart partFrm = new selectPart("../../partListTest1.xml"); old part list
                selectPart partFrm = new selectPart("./PLNew.xml"); // should be active part list instead of hardcore selection
                partFrm.ShowDialog();
                part partSelected = partFrm.selectedPart;
                string[] partData = partSelected.getData();
                partFrm.Dispose();                
                string filterExp1 = partData[(int)globals.fieldEnum.Name];
                string filterExp2 = partData[(int)globals.fieldEnum.cmID];
                //string filterExp3 = partData[(int)globals.fieldEnum.Package];
                //string filterExp4 = partData[(int)globals.fieldEnum.Grade];
                //string filterExp5 = partData[(int)globals.fieldEnum.Radiation];               

                DataRow[] rows = activePartList.Select("Name LIKE '%" + filterExp1 + "%' AND  cmID LIKE '%" + filterExp2 + "%'");
                textBox.Text += "\n" + rows[0][0] + "," + rows[0][1] + "," + rows[0][2] + "," + rows[0][3] + "," + rows[0][4] + "," + rows[0][5];
                if (rows.Count() > 0)
                {                    
                    mainProject.addChildPart(partSelected, st);
                    projectTree.Nodes.Clear();
                    projectTree.Nodes.Add(mainProject.getNode());
                }
                statuslabel.Text = partSelected.getName() + " part added";
                statusStrip.Refresh();
            }
        }

        private void addSubassemblyContextMenu_Click(object sender, EventArgs e)
        {
            //Adding subassembly context menu
            store storeInstance = store.instance();
            textBox.Text += storeInstance.count();
            TreeNode tn = projectTree.SelectedNode;
            string st = tn.Text.Trim();
            while (tn.Parent != null)
            {
                st = tn.Parent.Text.Trim() + "," + st;
                tn = tn.Parent;
            }
            //MessageBox.Show(st);
            assembly a = mainProject.findAssembly(st);
            string parentPath;
            try
            {
                parentPath = a.getFullPath();
            }
            catch(Exception exp)
            {
                parentPath = mainProject.getName();                
            }
            //MessageBox.Show(a.getFullPath(), "mainDesk");
            //assembly aNew = new assembly("new subassembly" + projectTree.Nodes.Count.ToString(), a.getFullPath());
            //storeInstance.add(aNew);
            //a.addAssembly(aNew);
            mainProject.addChildAssembly(parentPath, "subassembly" + mainProject.assemblyCount().ToString());
            textBox.Text += mainProject.assemblyCount().ToString();

            projectTree.Nodes.Clear();
            projectTree.Nodes.Add(mainProject.getNode());
            statuslabel.Text = "Assembly:" + "subassembly" + mainProject.assemblyCount().ToString() + "added";
            statusStrip.Refresh();
        }

        private void collapseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            projectTree.CollapseAll();
        }

        private void expandAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            projectTree.ExpandAll();
        }

        private void openProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Project File (*.prj)|*.prj|XML File (*.xml)|*.xml|All Files (*.*)|*.*";
            dlg.FilterIndex = 2;
            dlg.RestoreDirectory = true;
            dlg.InitialDirectory = System.IO.Directory.GetCurrentDirectory();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                mainProject = new project(dlg.FileName);
                projectTree.Nodes.Clear();
                projectTree.Nodes.Add(mainProject.getNode());
                projectTree.ExpandAll();
            }
            dlg.Dispose();
            statuslabel.Text = "Project loaded";
            statusStrip.Refresh();
        }

        private void closeProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            projectTree.Nodes.Clear();
            mainProject.clear();
            statuslabel.Text = "Project closed";
            statusStrip.Refresh();

        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // write project from tree node to xml file
            // only this file is readable in current version
            //xmlwrite.WriteStartDocument();
            //writeXMLfromProject(parentNode, ref xmlwrite);
            //xmlwrite.WriteEndDocument();
            //xmlwrite.Close();
            //xmlwrite.Dispose();            

            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "Project File (*.prj)|*.prj|XML File (*.xml)|*.xml|All Files (*.*)|*.*";
            dlg.FilterIndex = 2;
            dlg.RestoreDirectory = true;
            dlg.InitialDirectory = System.IO.Directory.GetCurrentDirectory();
            if (dlg.ShowDialog() == DialogResult.OK)
            {   
                //XmlWriterSettings xmlsettings = new XmlWriterSettings();
                //xmlsettings.Indent = true;
                //xmlsettings.NewLineOnAttributes = false;
                //XmlWriter xmlwrite = XmlWriter.Create(dlg.FileName, xmlsettings);
                XElement ele = mainProject.getXML();
                XDocument doc = new XDocument();
                doc.Add(ele);
                doc.Save(dlg.FileName);
            }
            dlg.Dispose();
            //MessageBox.Show("done");
            statuslabel.Text = "Project Saved";
            statusStrip.Refresh();
        }

        private void closeProperties_Click(object sender, EventArgs e)
        {
            panelProperties.Hide();
        }

    }
}
