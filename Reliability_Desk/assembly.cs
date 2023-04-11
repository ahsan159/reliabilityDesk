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
        List<assembly> childAssemblies = new List<assembly>();
        List<part> childParts = new List<part>();
        IList<assembly> parent;
        store storeInstance = store.instance();
        
        public assembly()
        {
            name = string.Empty;
            parent = null;
            mainFile = string.Empty;
        }
        public string getName()
        {
            return name;
        }
        public assembly(string nameNew, string parent)
        {
            name = nameNew;
            fullPath = parent + "," + name;
            created = DateTime.Now;
            node = new TreeNode("Assembly", 0, 0);
            node.Name = "Assembly";
            node.Text = name;
        }
        public void setAssemblyData(XElement ele, string parent)
        {
            name = ele.FirstNode.ToString().Trim();
            fullPath = parent.Trim() + "," + name;
            //MessageBox.Show(fullPath, name);
            setAssemblyData(ele);
        }
        public void setAssemblyData(XElement ele)
        {
            node = new TreeNode("Assembly", 0, 0);
            node.Name = "Assembly";
            if (ele.Name == "Assembly" || ele.Name == "Project")
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
                            a.setAssemblyData(e, fullPath);
                            if (!string.IsNullOrEmpty(a.getName()))
                            {
                                //MessageBox.Show("Iteration " + i++.ToString(), "Assembly" + name);
                                childAssemblies.Add(a);
                                //mainNode.addChildAssembly(a);
                                storeInstance.add(a);
                                node.Nodes.Add(a.getNode());
                            }
                        }
                        else if (e.Name == "Part")
                        {
                            //MessageBox.Show(e.FirstNode.ToString(), "PartFA" + name);
                            part p = new part();
                            p.setPartData(e, fullPath);
                            //TreeNode tn = new TreeNode("Part",1,1);
                            //tn.Text = "Part" + i++.ToString();
                            //node.Nodes.Add(tn);
                            if (!string.IsNullOrEmpty(p.getName()))
                            {
                                childParts.Add(p);
                                //mainNode.addChildPart(p);
                                storeInstance.add(p);
                                node.Nodes.Add(p.getNode());
                                //MessageBox.Show("node Added", "PartFA" + name);
                            }
                        }                        
                    }
                }
                //fullPath = name;
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
            MessageBox.Show(fullPath + Environment.NewLine + s, "Finding");
            if (fullPath == s)
            {
                //MessageBox.Show(getFullPath(), "FOUND A1");
                return this;
            }
            else
            {
                foreach (assembly a in childAssemblies)
                {
                    //MessageBox.Show(a.getFullPath(), "FOUND A2");
                    if(a.findAssembly(s) != null)
                    {
                        //MessageBox.Show(a.getFullPath(), "MATCH");
                        return a.findAssembly(s);
                    }
                }
            }
            return null;
        }
        public void addPart(part p)
        {
            childParts.Add(p);            
        }
        public void addAssembly(assembly a )
        {
            childAssemblies.Add(a);       
        }
        public bool addAssembly(string path, string name)
        {
            if(fullPath == path)
            {
                //MessageBox.Show("adding " + path + " " + name + " " + getFullPath(), "assembly Addition 1");
                assembly a = new assembly(name, path);
                childAssemblies.Add(a);
                node.Nodes.Add(a.getNode());
                return true;
                //MessageBox.Show("Adding", "added");
            }
            else
            {
                for (int i = 0; i < childAssemblies.Count; i++)
                {
                    //MessageBox.Show("adding " + path + " " + name + ":" + childAssemblies[i].getFullPath(), "project Addition 2");
                    childAssemblies[i].addAssembly(path, name);
                }
            }
            return false;
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
        public void renameSub(string newName, string oldName, string parentPath)
        {
            string oldPath = parentPath.Trim() + "," + oldName.Trim();            
            //MessageBox.Show(parentPath + "," + oldName, "Assembly" + name);
            if(oldPath == fullPath)
            {
                name = newName;
                node.Text = name;
                fullPath = parentPath.Trim() + "," + newName;
                //MessageBox.Show(oldPath + Environment.NewLine + fullPath, "Changing Assembly Name");
                //parentPath = fullPath.Substring(0, fullPath.LastIndexOf(","));
                for (int i = 0; i < childAssemblies.Count;i++)
                {
                    //MessageBox.Show(fullPath + Environment.NewLine + oldPath, "changingchilds");
                    childAssemblies[i].updatefullPath(fullPath, oldPath);
                }
            }
            else
            {
                for (int i = 0; i < childAssemblies.Count; i++)
                {
                    childAssemblies[i].renameSub(newName, oldName, parentPath);
                }
            }
        }
        public void updatefullPath(string newPath, string oldPath)
        {
            //MessageBox.Show(oldPath + "," + name + Environment.NewLine + fullPath + Environment.NewLine + newPath + "," + name, "Changing Childs");
            string npath = newPath + "," + name;
            string opath = oldPath + "," + name;
            if (opath == fullPath)
            {
                fullPath = npath;
                //MessageBox.Show(oldPath + Environment.NewLine + npath, "changed");
            }
            else
            {
                for (int i = 0; i < childAssemblies.Count; i++)
                {
                    childAssemblies[i].updatefullPath(npath, opath);
                }
            }
        }
    }
}
