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

namespace TNXMLUtility
{
    public static class TNXMLUtility
    {
        //private static IEnumerable<TreeNode>;
        //private static TreeNode[];
        public static void creatXML(string xmlFile, TreeNode nodeList)
        {
            XmlWriterSettings xmlsettings = new XmlWriterSettings();
            xmlsettings.Indent = true;
            xmlsettings.NewLineOnAttributes = true;
            XmlWriter xmlwrite = XmlWriter.Create(xmlFile, xmlsettings);
            xmlwrite.WriteStartDocument();
            TreeNodeCollection nodes = nodeList.Nodes;
            List<TreeNode> tr = nodes.Cast<TreeNode>().ToList<TreeNode>();
            writeXMLfromTreeNode(tr, ref xmlwrite);
            xmlwrite.WriteEndDocument();
            xmlwrite.Close();
            xmlwrite.Dispose();
        }
        private static void writeXMLfromTreeNode(List<TreeNode> projectTree, ref XmlWriter xmlwrite)
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
                    writeXMLfromTreeNode(childList, ref xmlwrite);  // call recursively for childs
                }
                xmlwrite.WriteEndElement();
            }
        }
        public static TreeNode readXML(string projectFile)
        {
            // write project tree from xml file            
            TreeNode tn = new TreeNode();
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.IgnoreWhitespace = false;
            XmlReader reader = XmlReader.Create(projectFile, settings);
            //print(reader.ToString() + "\n");
            XElement element = XElement.Load(reader);
            IEnumerable<XElement> subtree = element.Elements(); // get childs            
            tn.Name = element.Name.ToString().Trim();
            tn.Text = element.FirstNode.ToString().Trim();
            if (element.HasAttributes)
            {
                addChildAttributes(element.Attributes(), tn);
            }
            addChildNodes(subtree, tn);
            reader.Close();
            reader.Dispose();
            return tn;
        }
        private static void addChildNodes(IEnumerable<XElement> elements, TreeNode parent)
        {
            //this xml reading and adding nodes is quiet elusive for me and sometimes
            // i really feel frustrated for it. But this piece of code is working
            // 14-Feb edited code to process attributes as child nodes
            foreach (XElement x in elements)
            {
                if (x.Name.ToString().Trim().Equals("Assembly"))
                {
                    // 'Assembly' will come here
                    parent.Nodes.Add(x.Name.ToString().Trim(), x.FirstNode.ToString().Trim(),0);
                    if (x.HasElements) // Add Child if they are available
                    {
                        addChildNodes(x.Elements(), parent.Nodes[parent.Nodes.Count - 1]);
                    }
                    //parent.Nodes.Add(ctn);                                      
                    if (x.HasAttributes)
                    {
                        addChildAttributes(x.Attributes(), parent.Nodes[parent.Nodes.Count - 1]);
                    }                    
                }
                else if (x.Name.ToString().Trim().Equals("Part"))
                {
                    // 'Part' will come here
                    parent.Nodes.Add(x.Name.ToString().Trim(), x.FirstNode.ToString().Trim(),1);
                    if (x.HasAttributes)
                    {
                        addChildAttributes(x.Attributes(), parent.Nodes[parent.Nodes.Count - 1]);
                    }
                }
            }
        }
        private static void addChildAttributes(IEnumerable<XAttribute> attributes, TreeNode parent)
        {
            // this function will add the attributes to node either part or assembly
            foreach (XAttribute a in attributes)
            {
                parent.Nodes.Add(a.Name.ToString().Trim(), a.Value.ToString().Trim(),3);
            }
        }
        //private static void addChildNodes(IEnumerable<XElement> elements, TreeNode parent)
        //{
        //    //this xml reading and adding nodes is quiet elusive for me and sometimes
        //    // i really feel frustrated for it. But this piece of code is working
        //    foreach (XElement x in elements)
        //    {
        //        if (x.HasElements)
        //        {
        //            // 'Assembly' will come here
        //            parent.Nodes.Add(x.Name.ToString().Trim(), x.FirstNode.ToString().Trim());
        //            addChildNodes(x.Elements(), parent.Nodes[parent.Nodes.Count - 1]);
        //            //parent.Nodes.Add(ctn);                    
        //        }
        //        else
        //        {
        //            // 'Part' will come here
        //            parent.Nodes.Add(x.Name.ToString().Trim(), x.FirstNode.ToString().Trim());
        //        }
        //    }
        //}
        public static string readXMLasString(string fileName, string tag, string value)
        {
            // get text data of the selected xml node            
            string fileData = "";
            try
            {
                XElement root = XElement.Load(fileName);
                //fileData = root.ToString();
                IEnumerable<XElement> data = from e in root.DescendantsAndSelf(tag)
                                             where e.FirstNode.ToString().Equals(value)
                                             select e;
                foreach (XElement e in data)
                {
                    fileData = fileData + e.ToString() + "\n";
                }
            }
            catch (Exception e)
            {
                fileData = e.ToString();
            }
            return fileData;
        }
        public static string readXMLasString(string fileName)
        {
            // read whole xml file as string
            string fileData = "";
            try
            {
                XElement root = XElement.Load(fileName);
                fileData = root.ToString();
                //IEnumerable<XElement> data = from e in root.Elements("tag") where e.Value.ToString().Equals(value) select e;
                //fileData = data.ToString();
            }
            catch (Exception e)
            {
                fileData = e.ToString();
            }
            return fileData;
        }
        //private static TreeNode addChildNodes(IEnumerable<XElement> element)
        //{
        //    // recursive function to read xml file and create project tree

        //    //XElement[] ele =  element.ToArray();
        //    TreeNode tn = new TreeNode();
        //    TreeNode ctn = null;
        //    foreach (XElement x in element) // iterate through elements
        //    {
        //        //tn.Nodes.Add(x.Name.ToString().Trim(), x.FirstNode.ToString().Trim(), 0, 0);                
        //        FileStream fs = new FileStream("Project.TXT", FileMode.Append);
        //        Byte[] bt = new UTF8Encoding(true).GetBytes("\nPROJECT\n");
        //        fs.Write(bt, 0, bt.Length);
        //        bt = new UTF8Encoding(true).GetBytes(x.Name.ToString() + "," + x.FirstNode.ToString().Trim() + "\n");
        //        fs.Write(bt, 0, bt.Length);
        //        fs.Close();
        //        fs.Dispose();
        //        if (x.Name.ToString().Trim().Equals("Assembly"))
        //        {
        //            //projectTree.SelectedNode.Nodes.Add(x.Name.ToString().Trim(), x.FirstNode.ToString().Trim(), 0, 0);
        //            //select current node for adding childers
        //            //projectTree.SelectedNode = projectTree.SelectedNode.Nodes[projectTree.SelectedNode.Nodes.Count - 1];                                        
        //            tn.Nodes.Add(x.Name.ToString().Trim(), x.FirstNode.ToString().Trim());
        //            //tn.Nodes[tn.Nodes.Count-1].Nodes.Add("This is node" + tn.Nodes.Count);
        //            //tn.Text = "";
        //            //tn.Text = "AF";
        //            if (x.HasElements) // if it has elements add them recursively
        //            {
        //                ctn = addChildNodes(x.Elements());
        //                tn.Nodes[tn.Nodes.Count-1].Nodes.Add(ctn);//[tn.Nodes.Count - 1].Nodes.Add(ctn);                        
        //                //tn.Nodes.Add(ctn);
        //                //tn = new TreeNode("MYNODE", ctn.Nodes.Cast<TreeNode>().ToArray<TreeNode>());
        //            }
        //            //projectTree.SelectedNode = projectTree.SelectedNode.Parent; // exit from current node add another sibling
        //        }
        //        else if (x.Name.ToString().Trim().Equals("Part"))
        //        {
        //            // part should have no childern
        //            //projectTree.SelectedNode.Nodes.Add(x.Name.ToString().Trim(), x.FirstNode.ToString().Trim(), 1, 1);
        //            tn.Nodes.Add(x.Name.ToString().Trim(), x.FirstNode.ToString().Trim());
        //            //tn.Nodes = tn.Nodes[0].Nodes;
        //            //if(x.HasAttributes)
        //            //{
        //            //    print(x.FirstAttribute.ToString() + "\n");
        //            //}
        //        }
        //    }
        //    return tn;
        //}
        //private static TreeNode addChildNodes1(IEnumerable<XElement> element)
        //{
        //    // recursive function to read xml file and create project tree
        //    foreach (XElement x in element) // iterate through elements
        //    {
        //        if (x.Name.ToString().Trim().Equals("Assembly"))
        //        {
        //            //projectTree.SelectedNode.Nodes.Add(x.Name.ToString().Trim(), x.FirstNode.ToString().Trim(), 0, 0);
        //            //select current node for adding childers
        //            //projectTree.SelectedNode = projectTree.SelectedNode.Nodes[projectTree.SelectedNode.Nodes.Count - 1];
        //            if (x.HasElements) // if it has elements add them recursively
        //            {
        //                addChildNodes1(x.Elements());
        //            }
        //            //projectTree.SelectedNode = projectTree.SelectedNode.Parent; // exit from current node add another sibling
        //        }
        //        else if (x.Name.ToString().Trim().Equals("Part"))
        //        {
        //            // part should have no childern
        //            //projectTree.SelectedNode.Nodes.Add(x.Name.ToString().Trim(), x.FirstNode.ToString().Trim(), 1, 1);
        //            //if(x.HasAttributes)
        //            //{
        //            //    print(x.FirstAttribute.ToString() + "\n");
        //            //}
        //        }
        //    }
        //}
    }
}
