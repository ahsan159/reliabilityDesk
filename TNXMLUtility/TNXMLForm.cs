using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TNXMLUtility
{
    public partial class TNXMLForm : Form
    {
        public TNXMLForm()
        {
            InitializeComponent();
        }

        private void TNXMLForm_Load(object sender, EventArgs e)
        {
            //TreeNode tn1 = new TreeNode();            
            //richTextBox1.Text = richTextBox1.Text + tn.Nodes.Count.ToString();            
            treeView1.BeginUpdate();
            filePath = "./projectTest2.xml";
            TreeNode tn = new TreeNode("Project");
            tn = TNXMLUtility.readXML(filePath);
            treeView1.Nodes.Add(tn);
            treeView1.EndUpdate();
            richTextBox1.Text = richTextBox1.Text + TNXMLUtility.readXMLasString(filePath);
        }

        private void treeView1_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            // fill node text from xml file when double clicked
            richTextBox1.Text = TNXMLUtility.readXMLasString(filePath, treeView1.SelectedNode.Name.ToString().Trim(), treeView1.SelectedNode.Text.ToString().Trim()) + "\n";
            //richTextBox1.Text.Replace(">", ">>>");

        }

        private void projectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //string filePath = "";
            //string fileContent = "";
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Project File (*.prj)|*.prj|XML File (*.xml)|*.xml|All Files (*.*)|*.*";
            dlg.FilterIndex = 2;
            dlg.RestoreDirectory = true;
            dlg.InitialDirectory = System.IO.Directory.GetCurrentDirectory();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                filePath = dlg.FileName;
                treeView1.BeginUpdate();
                treeView1.Nodes.Add(TNXMLUtility.readXML(filePath));
                treeView1.EndUpdate();
                richTextBox1.Text = TNXMLUtility.readXMLasString(filePath);
            }
            dlg.Dispose();
        }

        private void exit_Click(object sender, EventArgs e)
        {
            treeView1.BeginUpdate();
            treeView1.Nodes.Clear();
            treeView1.EndUpdate();
            richTextBox1.Text = "";
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void partToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Part File (*.prt)|*.prt|XML File (*.xml)|*.xml|All Files (*.*)|*.*";
            dlg.RestoreDirectory = true;
            dlg.InitialDirectory = System.IO.Directory.GetCurrentDirectory();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                filePath = dlg.FileName;
                treeView1.BeginUpdate();
                treeView1.Nodes.Add(TNXMLUtility.readXML(filePath));
                treeView1.EndUpdate();
            }
            dlg.Dispose();
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            treeView1.BeginUpdate();
            treeView1.Nodes.Clear();
            treeView1.EndUpdate();
            richTextBox1.Text = "";
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {

        }

        private void openFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Project File (*.prj)|*.prj|XML File (*.xml)|*.xml|All Files (*.*)|*.*";
            dlg.FilterIndex = 2;
            dlg.RestoreDirectory = true;
            dlg.InitialDirectory = System.IO.Directory.GetCurrentDirectory();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                filePath = dlg.FileName;
                treeView1.BeginUpdate();
                treeView1.Nodes.Add(TNXMLUtility.readXML(filePath));
                treeView1.EndUpdate();
                richTextBox1.Text = TNXMLUtility.readXMLasString(filePath);
            }
            dlg.Dispose();
        }

        private void openPart_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Part File (*.prt)|*.prt|XML File (*.xml)|*.xml|All Files (*.*)|*.*";
            dlg.RestoreDirectory = true;
            dlg.InitialDirectory = System.IO.Directory.GetCurrentDirectory();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                filePath = dlg.FileName;
                treeView1.BeginUpdate();
                treeView1.Nodes.Add(TNXMLUtility.readXML(filePath));
                treeView1.EndUpdate();
            }
            dlg.Dispose();
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            nodeCopy = (TreeNode) treeView1.SelectedNode.Clone();
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                treeView1.SelectedNode.Nodes.Add(nodeCopy);
            }
            catch (Exception exp)
            {
                richTextBox1.Text = exp.ToString();

            }
        }

        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                treeView1.SelectedNode = e.Node;
                contextMenuStrip1.Show(Cursor.Position.X, Cursor.Position.Y);
            }
        }
    }
}
