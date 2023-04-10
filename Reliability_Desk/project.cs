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
    class project
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
        List<assembly> childAssemblies = new List<assembly>();
        List<part> childParts = new List<part>();
        IList<assembly> parent;
        store storeInstance = store.instance();

        public project()
        {
            name = string.Empty;
            parent = null;
            mainFile = string.Empty;

        }
        public project(string path)
        {
            filePath = path;
            XmlReaderSettings xmlreadersettings = new XmlReaderSettings();
            xmlreadersettings.IgnoreWhitespace = false;
            XmlReader reader = XmlReader.Create(path, xmlreadersettings);
            XElement ele = XElement.Load(reader);
            setAssemblyData(ele, "");
            reader.Close();
            reader.Dispose();
        }
        public string getName()
        {
            return name;
        }
        public void setFullPath(string s)
        {
            fullPath = s;
        }
        public string getFullPath()
        {
            return fullPath;
        }
        public void setAssemblyData(XElement ele, string parent)
        {
            setAssemblyData(ele);
            fullPath = parent.Trim() + "," + name.Trim();
        }
        public void setAssemblyData(XElement ele)
        {
            node = new TreeNode("Project", 0, 0);
            node.Name = "Project";
            if (ele.Name == "Project")
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
                                storeInstance.add(a);
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
                                //childParts.Add(p);
                                storeInstance.add(p);
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
            foreach (XAttribute a in attrib)
            {
                ele.Add(a);
            }
            foreach (assembly a in childAssemblies)
            {
                ele.Add(a.getXML());
            }
            foreach (part p in childParts)
            {
                ele.Add(p.getXML());
            }
            return ele;
        }
        public TreeNode getNode()
        {
            //MessageBox.Show(node.Text);
            return node;
        }
        public assembly findAssembly(string s)
        {
            //MessageBox.Show(getFullPath(), "FOUND P1");
            assembly selectedAssembly = null;
            foreach (assembly a in childAssemblies)
            {
                //MessageBox.Show(a.getFullPath(), "FOUND P2");
                if (a.findAssembly(s) != null)
                {
                    //MessageBox.Show(a.getFullPath(), "MATCH");
                    return a.findAssembly(s);
                }
            }

            return selectedAssembly;

        }
        public void addChildAssembly(assembly a)
        {
            childAssemblies.Add(a);
        }
        public void addChildAssembly(string path, string name)
        {
            if (fullPath == path)
            {
                assembly a = new assembly(name, fullPath);
                childAssemblies.Add(a);
            }
            else
            {
                for (int i = 0; i < childAssemblies.Count;i++ )
                {
                    //MessageBox.Show("adding " + path + " " + name, "project Addition");
                    if (childAssemblies[i].addAssembly(path, name))
                    {
                        break;
                    }
                }
            }

        }
        public void addChildPart(part p)
        {
            childParts.Add(p);
        }
        public int assemblyCount()
        {
            int i = childAssemblies.Count;
            foreach (assembly a in childAssemblies)
            {
                i += a.assemblyCount();
            }
            return i;
        }
        public void renameSub(string newName, string oldName, string path)
        {
            for(int i = 0; i < childAssemblies.Count; i++)
            {
                childAssemblies[i].renameSub(newName, oldName, path);
            }
        }
    }
}
