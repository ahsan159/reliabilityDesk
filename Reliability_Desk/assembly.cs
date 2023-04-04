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
        private TreeNode tree;
        private int isTemporary;
        private string filePath;
        private string sourcePath;
        int NodeCount;
        IList<assembly> childAssemblies;
        List<part> childParts;
        IList<assembly> parent;

        assembly()
        {
            parent = null;
            mainFile = string.Empty;
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
            attrib[i++] = new XAttribute("source", sourcePath);
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
    }
}
