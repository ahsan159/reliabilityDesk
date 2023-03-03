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
    public class part
    {
        private string name;
        private string cat;
        private string scat;
        private string cmID;
        private string des;
        private double MTBF;
        private double FR;
        private string package;
        private string heritage;
        private string radiationData;
        private string reliabilityData;
        private string outgassingData;
        private string manufacturer;
        private string user;
        private DateTime added;
        private bool analysis;
        public part()
        {
            name = "noname";
        }

        public part(string name, string manufacturer)
        {
            this.name = name;
            this.manufacturer = manufacturer;
            analysis = false;
        }
        public part(string category, string subcategory, string partName, string manufacturer, string description)
        {
            analysis = false;
            name = partName;
            cat = category;
            scat = subcategory;
            this.manufacturer = manufacturer;
            des = description;
        }
        public void setPartData(string category, string subcategory, string partName, string manufacturer, string description)
        {
            analysis = false;
            name = partName;
            cat = category;
            scat = subcategory;
            this.manufacturer = manufacturer;
            des = description;
        }
        public void setReliabilityData(double MTBF, double FR)
        {
            analysis = false;
            this.MTBF = MTBF;
            this.FR = FR;
        }
        public void setReliabilityData(string MTBF)
        {
            analysis = false;
            this.MTBF = double.Parse(MTBF);
        }
        public void setcmID(string id)
        {
            analysis = false;
            cmID = id;
        }
        public void setPartData(XElement ele)
        {
            analysis = false;
            this.name = ele.Value;
            if (ele.HasAttributes)
            {
                foreach(XAttribute a in ele.Attributes())
                {
                    switch (a.Name.ToString())
                    {
                        case "dateAdded":
                            this.added = DateTime.Parse(a.Value.ToString());
                            break;
                        case "MTBF":
                            this.MTBF = double.Parse(a.Value.ToString());
                            break;
                        case "category":
                            this.cat = a.Value.ToString();
                            break;
                        case "subcategory":
                            this.scat = a.Value.ToString();
                            break;
                        case "manufacturer":
                            this.manufacturer = a.Value.ToString();
                            break;
                        case "description":
                            this.des = a.Value.ToString();
                            break;
                        case "package":
                            this.package = a.Value.ToString();
                            break;
                        case "cmID":
                            this.cmID = a.Value.ToString();
                            break;
                    }
                }
            }
        }
        public void setPartData(TreeNode node)
        {
            analysis = false;
            if(node.Name.Equals("Part"))
            {
                this.name = node.Text;
                foreach (TreeNode n in node.Nodes)
                {
                    switch(n.Name)
                    {
                        case "dateAdded":
                            this.added = DateTime.Parse(n.Text);
                            break;
                        case "MTBF":
                            this.MTBF = double.Parse(n.Text);
                            break;                    
                        case "category":
                            this.cat = n.Text;
                            break;
                        case "subcategory":
                            this.scat = n.Text;
                            break;
                        case "manufacturer":
                            this.manufacturer = n.Text;
                            break;
                        case "description":
                            this.des = n.Text;
                            break;
                        case "package":
                            this.package = n.Text;
                            break;
                        case "cmID":
                            this.cmID = n.Text;
                            break;
                    }
                }
            }
            else
            {
                
            }
        }
    }
}
