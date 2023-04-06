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
    class assembly
    {
        private string name;
        private string ccms;
        private string mainFile;
        private string createUser;
        private DateTime created;
        private string lastUser;
        private DateTime modified;
        private TreeNode node;
        private int isTemporary;
        private string filePath;
        private string fullPath;
        int NodeCount;
        List<assembly> childAssemblies;
        List<part> childParts;
        IList<assembly> parent;

        public assembly()
        {
            name = string.Empty;
            parent = null;
            mainFile = string.Empty;
            childParts = new List<part>();
            childAssemblies = new List<assembly>();
        }
        public string getName()
        {
            return name;
        }
        public void setAssemblyData(XElement ele, string parent)
        {
            setAssemblyData(ele);
            fullPath = parent.Trim() + "," + name;
        }
        public void setAssemblyData(XElement ele)
        {
            node = new TreeNode("Assembly", 0, 0);
            node.Name = "Assembly";
            if (ele.Name == "Assembly")
            {
                //MessageBox.Show(ele.FirstNode.ToString(), "Assembly");
                name = ele.FirstNode.ToString().Trim();
                node.Text = name.Trim();
                if (ele.HasAttributes)
                {

                }
                if (ele.HasElements)
                {
                    IEnumerable<XElement> children = ele.Elements();
                    //MessageBox.Show(children.Count().ToString());
                    int i = 0;
                    foreach (XElement e in children)
                    {
                        if (e.Name == "Assembly")
                        {
                            //MessageBox.Show(e.FirstNode.ToString(), "AssemblyFA" + name);
                            assembly a = new assembly();
                            a.setAssemblyData(e, name);
                            if (!string.IsNullOrEmpty(a.getName()))
                            {
                                //MessageBox.Show("Iteration " + i++.ToString(), "Assembly" + name);
                                childAssemblies.Add(a);
                                node.Nodes.Add(a.getNode());
                            }
                        }
                        else if (e.Name == "Part")
                        {
                            //MessageBox.Show(e.FirstNode.ToString(), "PartFA" + name);
                            part p = new part();
                            p.setPartData(e, name);
                            //TreeNode tn = new TreeNode("Part",1,1);
                            //tn.Text = "Part" + i++.ToString();
                            //node.Nodes.Add(tn);
                            if (!string.IsNullOrEmpty(p.getName()))
                            {
                                childParts.Add(p);
                                node.Nodes.Add(p.getNode());
                                //MessageBox.Show("node Added", "PartFA" + name);
                            }
                        }                        
                    }
                }
                fullPath = name;
            } 
        }

        public XElement getXML()
        {
            XElement ele = new XElement("Assembly");
            ele.Value = name;
            XAttribute[] attrib = new XAttribute[8];
            int i = 0;
            attrib[i++] = new XAttribute("ccms", ccms);
            attrib[i++] = new XAttribute("main", mainFile);
            attrib[i++] = new XAttribute("created", created.ToShortDateString());
            attrib[i++] = new XAttribute("creator", createUser);
            attrib[i++] = new XAttribute("modified", modified.ToShortDateString());
            attrib[i++] = new XAttribute("modifier", lastUser);
            attrib[i++] = new XAttribute("file", filePath);
            attrib[i++] = new XAttribute("source", fullPath);
            foreach(XAttribute a in attrib)
            {
                ele.Add(a);
            }
            foreach(assembly a in childAssemblies)
            {
                ele.Add(a.getXML());
            }
            foreach(part p in childParts)
            {
                ele.Add(p.getXML());
            }
            return ele;
        }
        public TreeNode getNode()
        {
            return node;
        }
        public void setFullPath(string s)
        {
            fullPath = s;
        }
        public string getFullPath()
        {
            return fullPath;
        }
        public void updateFullPath(string parent)
        {
            fullPath = parent + "," + name;
        }
        public assembly findAssembly(string s)
        {
            assembly selectedAssembly = null;
            foreach (assembly a in childAssemblies)
            {
                if (a.getFullPath() == s)
                {
                    selectedAssembly = a;
                }
            }
            return selectedAssembly;
        }
    }
}
