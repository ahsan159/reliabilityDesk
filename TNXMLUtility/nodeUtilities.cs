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
    public static class nodeUtilities
    {
        public static List<part> readPartsfromXML(string fileName)
        {
            XmlReader reader = XmlReader.Create(fileName);
            XElement root = XElement.Load(reader);
            List<part> partList = new List<part>();
            if (root.Name.Equals("PartList"))
            {
                partList = readPartList(root.Descendants());
            }
            return partList;
        }
        private static List<part> readPartList(IEnumerable<XElement> partElements)
        {
            List<part> pl = new List<part>();
            foreach(XElement part in partElements)
            {
                part p = new part();
                p.setPartData(part);
                pl.Add(p);
            }
            return pl;
        }
        private static part readPartfromXElement(XElement ele)
        {
            part p = new part();
            p.setPartData(ele);
            return p;
        }
    }
}
