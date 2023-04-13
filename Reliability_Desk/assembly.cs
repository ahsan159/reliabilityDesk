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
using System.Globalization;
using TNXMLUtility;

namespace Reliability_Desk
{
    class assembly
    {
        private string name;
        private string ccms = "";
        private string mainFile;
        private string createUser;
        private DateTime created;
        private string lastUser;
        private DateTime modified;
        private TreeNode node;        
        private string filePath = "";
        private string fullPath;
        int NodeCount;
        List<assembly> childAssemblies = new List<assembly>();
        List<part> childParts = new List<part>();
        IList<assembly> parent;
        store storeInstance = store.instance();
        double MTBF = -1;
        
        public assembly()
        {
            name = string.Empty;
            parent = null;
            mainFile = string.Empty;
            created = DateTime.Now;
            createUser = "ahsan";
            modified = DateTime.Now;
            lastUser = "ahsan";
            node = new TreeNode("Assembly", 0, 0);
            node.Name = "Assembly";
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
            createUser = "ahsan";
            modified = DateTime.Now;
            lastUser = "ahsan";
            node = new TreeNode("Assembly", 0, 0);
            node.Name = "Assembly";
            node.Text = name;            
        }
        public void setAssemblyData(XElement ele, string parent)
        {
            name = ele.FirstNode.ToString().Trim();
            fullPath = parent.Trim() + "," + name;            
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
                    foreach(XAttribute a in ele.Attributes())
                    {
                        switch (a.Name.ToString())
                        {
                            case "dateAdded":
                                modified = DateTime.ParseExact(a.Value.ToString(), "dd-MM-yyyy", CultureInfo.CurrentCulture);
                                break;
                            case "userLast":
                                lastUser = a.Value.ToString();
                                break;
                            case "dateCreate":
                                created = DateTime.ParseExact(a.Value.ToString(), "dd-MM-yyyy", CultureInfo.CurrentCulture);
                                break;
                            case "userCreate":
                                createUser = a.Value.ToString();
                                break;
                            case "ccms":
                                ccms = a.Value.ToString();
                                break;
                            case "MTBF":
                                MTBF = double.Parse(a.Value.ToString());
                                break;
                            default:
                                break;
                        }
                    }
                }
                if (ele.HasElements)
                {
                    IEnumerable<XElement> children = ele.Elements();                                        
                    foreach (XElement e in children)
                    {
                        if (e.Name == "Assembly")
                        {                            
                            assembly a = new assembly();
                            a.setAssemblyData(e, fullPath);
                            if (!string.IsNullOrEmpty(a.getName()))
                            {
                                childAssemblies.Add(a);                                
                                storeInstance.add(a);
                                node.Nodes.Add(a.getNode());
                            }
                        }
                        else if (e.Name == "Part")
                        {
                            part p = new part();
                            p.setPartData(e, fullPath);                            
                            if (!string.IsNullOrEmpty(p.getName()))
                            {
                                childParts.Add(p);
                                storeInstance.add(p);
                                node.Nodes.Add(p.getNode());                                
                            }
                        }                        
                    }
                }
                //fullPath = name;
            } 
        }
        public string getccms()
        {
            return ccms;
        }
        public void setccms(string s)
        {
            ccms = s;
        }
        public XElement getXML()
        {
            XElement ele = new XElement("Assembly");
            ele.Value = name;
            XAttribute[] attrib = new XAttribute[8];
            int i = 0;
            attrib[i++] = new XAttribute("ccms", ccms);
            //attrib[i++] = new XAttribute("main", mainFile);
            //attrib[i++] = new XAttribute("created", created.ToShortDateString());
            //attrib[i++] = new XAttribute("creator", createUser);
            //attrib[i++] = new XAttribute("modified", modified.ToShortDateString());
            //attrib[i++] = new XAttribute("modifier", lastUser);
            attrib[i++] = new XAttribute("dateAdded", modified.ToString("dd-MM-yyyy"));
            attrib[i++] = new XAttribute("file", filePath);
            attrib[i++] = new XAttribute("source", fullPath);
            attrib[i++] = new XAttribute("MTBF", this.MTBF);
            foreach(XAttribute a in attrib)
            {
                ele.Add(a);
            }
            foreach(assembly a in childAssemblies)
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
            //MessageBox.Show(fullPath + Environment.NewLine + s, "Finding");
            if (fullPath == s)
            {                
                return this;
            }
            else
            {
                foreach (assembly a in childAssemblies)
                {                    
                    if(a.findAssembly(s) != null)
                    {
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
        public void addPart(part p, string parent)
        {
            if (fullPath == parent)
            {
                childParts.Add(p);// problematic part of the code data of part is not being updated properly
                p.setFullPath(parent + "," + p.getName()); // new line added
                node.Nodes.Add(p.getNode());
            }
            else
            {
                for (int i = 0; i < childAssemblies.Count; i++)
                {
                    childAssemblies[i].addPart(p, parent);
                }
            }
        }
        public void addAssembly(assembly a )
        {
            childAssemblies.Add(a);       
        }
        public bool addAssembly(string path, string name)
        {
            if(fullPath == path)
            {                
                assembly a = new assembly(name, path);
                childAssemblies.Add(a);
                node.Nodes.Add(a.getNode());
                return true;                
            }
            else
            {
                for (int i = 0; i < childAssemblies.Count; i++)
                {                    
                    childAssemblies[i].addAssembly(path, name);
                }
            }
            return false;
        }
        public void addAssembly(XElement ele, string path)
        {
            //MessageBox.Show(fullPath + Environment.NewLine + path, "Assembly");
            if (fullPath == path)
            {                
                assembly a = new assembly();
                a.setAssemblyData(ele, path);
                childAssemblies.Add(a);
                node.Nodes.Add(a.getNode());
            }
            else
            {
                for (int i = 0; i < childAssemblies.Count; i++)
                {
                    childAssemblies[i].addAssembly(ele, path);
                }
            }
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
            if(oldPath == fullPath)
            {
                name = newName;
                node.Text = name;
                fullPath = parentPath.Trim() + "," + newName;
                for (int i = 0; i < childAssemblies.Count;i++)
                {
                    childAssemblies[i].updatefullPath(fullPath, oldPath);
                }
                for (int i = 0; i<childParts.Count;i++)
                {
                    childParts[i].setFullPath(fullPath + "," + childParts[i].getName());
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
            string npath = newPath + "," + name;
            string opath = oldPath + "," + name;
            if (opath == fullPath)
            {
                fullPath = npath;
                foreach(part p in childParts)
                {
                    p.setFullPath(fullPath + "," + p.getName());
                }
            }
            else
            {
                for (int i = 0; i < childAssemblies.Count; i++)
                {
                    childAssemblies[i].updatefullPath(npath, opath);
                }
            }
        }

        public TreeNode refreshNode()
        {
            node.Nodes.Clear();
            node.Name = "Assembly";
            node.Text = name;
            foreach(assembly a in childAssemblies)
            {
                a.refreshNode();
                node.Nodes.Add(a.getNode());
            }
            foreach(part p in childParts)
            {
                node.Nodes.Add(p.getNode());
            }
            return node;
        }
        public void deleteItem(string path)
        {
            assembly selected = null;
            foreach (assembly a in childAssemblies)
            {
                if (a.getFullPath() == path)
                {
                    selected = a;
                    break;
                }
                a.deleteItem(path);
            }
            if (selected != null)
            {
                childAssemblies.Remove(selected);
            }

            part selectedPart = null;
            foreach (part p in childParts)
            {
                if (p.getFullPath() == path)
                {
                    selectedPart = p;
                    break;
                }
            }
            if (selectedPart != null)
            {
                childParts.Remove(selectedPart);
            }
        }
    }
}
