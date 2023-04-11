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
        private int isTemporary; // for future use
        private string filePath;
        private string fullPath;
        int NodeCount;
        List<assembly> childAssemblies = new List<assembly>();
        List<part> childParts = new List<part>();
        IList<assembly> parent; //always null for project
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
            setAssemblyData(ele);
            reader.Close();
            reader.Dispose();
        }
        public void setNew(string nameP)
        {
            name = nameP;
            fullPath = name;
            lastUser = "ahsan";
            created = DateTime.Now;
            createUser = "ahsan";
            modified = DateTime.Now;
            ccms = "";
        }
        public string getFileName()
        {
            return filePath;
        }        
        public string getName()
        {
            return name;
        }
        public string getccms()
        {
            return ccms;
        }
        public void setccms(string s)
        {
            ccms = s;
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
                name = ele.FirstNode.ToString().Trim();
                node.Text = name.Trim();                
                if (ele.HasAttributes)
                {
                    foreach(XAttribute a in ele.Attributes())
                    {                        
                        switch(a.Name.ToString())
                        {
                            case "dateLast":
                                this.modified = DateTime.ParseExact(a.Value.ToString(), "dd-MM-yyyy", CultureInfo.CurrentCulture);
                                break;
                            case "userLast":
                                this.lastUser = a.Value.ToString();
                                break;
                            case "dateCreate":
                                this.created = DateTime.ParseExact(a.Value.ToString(), "dd-MM-yyyy", CultureInfo.CurrentCulture);
                                break;
                            case "userCreate":
                                this.createUser = a.Value.ToString();
                                break;
                            case "ccms":
                                this.ccms = a.Value.ToString();
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
                            a.setAssemblyData(e, name);
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
                            p.setPartData(e, name);                            
                            if (!string.IsNullOrEmpty(p.getName()))
                            {                                
                                storeInstance.add(p);
                                node.Nodes.Add(p.getNode());                                
                            }
                        }
                    }
                }
                fullPath = name;
            }
        }

        public XElement getXML()
        {
            XElement ele = new XElement("Project");
            ele.Value = name;
            XAttribute[] attrib = new XAttribute[8];
            int i = 0;
            
            //attrib[i++] = new XAttribute("ccms", ccms);            
            //attrib[i++] = new XAttribute("main", mainFile);
            attrib[i++] = new XAttribute("dateCreate", created.ToString("dd-MM-yyyy"));
            attrib[i++] = new XAttribute("userCreate", createUser);
            attrib[i++] = new XAttribute("dateLast", modified.ToString("dd-MM-yyyy"));
            attrib[i++] = new XAttribute("userLast", lastUser);
            attrib[i++] = new XAttribute("file", filePath);
            //attrib[i++] = new XAttribute("source", fullPath);
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
            return node;
        }
        public assembly findAssembly(string s)
        {
            // search for assemblies recursively and return null if not found
            assembly selectedAssembly = null;
            foreach (assembly a in childAssemblies)
            {
                if (a.findAssembly(s) != null)
                {
                    return a.findAssembly(s);
                }
            }

            return selectedAssembly;

        }
        public void addChildAssembly(assembly a)
        {
            childAssemblies.Add(a);
        }
        public void addChildPart(part p, string parent)
        {
            foreach(assembly a in childAssemblies)
            {
                a.addPart(p, parent);
            }            
        }
        public void addChildAssembly(string path, string name)
        {
            if (fullPath == path)
            {
                // add if adding as immediate child             
                assembly a = new assembly(name, fullPath);
                childAssemblies.Add(a);
                node.Nodes.Add(a.getNode());
            }
            else
            {
                // add to childs recursively and break when when successful
                for (int i = 0; i < childAssemblies.Count;i++ )
                {
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
            NodeCount = i;
            return i;
        }
        public void renameSub(string newName, string oldName, string path)
        {
            // project cannot be renamed and child assemblies to be renamed recursively
            for(int i = 0; i < childAssemblies.Count; i++)
            {
                childAssemblies[i].renameSub(newName, oldName, path);
            }
        }
    }
}
