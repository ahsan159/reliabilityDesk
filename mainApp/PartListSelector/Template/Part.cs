using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PartListSelector.Template
{
    public class Part
    {
        #region members of class
        public string Name { get; set; }
        public string Category { get; set; }
        public string Subcategory { get; set; }
        public string Description { get; set; }
        public string MTBF { get; set; }
        public string Package { get; set; }
        public string Manufacturer { get; set; }
        #endregion
        //public ObservableCollection<Part> PartCollection;

        public Part()
        {

        }
        public Part(XElement element)
        {
            //PartCollection = new ObservableCollection<Part>();
            Name = element.Value;
            foreach (XAttribute a in element.Attributes())
            {
                if (a.Name == "Name")
                {
                    Name = a.Value;
                }
                else if (a.Name == "mtbf")
                {
                    MTBF = a.Value;
                }
                else if (a.Name == "des")
                {
                    Description = a.Value;
                }
                else if (a.Name == "pack")
                {
                    Package = a.Value;
                }
                else if (a.Name == "cat")
                {
                    Category = a.Value;
                }
                else if (a.Name == "scat")
                {
                    Subcategory = a.Value;
                }
                else if (a.Name == "mftr")
                {
                    Manufacturer = a.Value;
                }
            }

        }

        public XElement toXElement()
        {
            XElement element = new XElement(Name); ;
            element.Add(new XAttribute(nameof(Name), Name));
            element.Add(new XAttribute(nameof(Category), Category));
            element.Add(new XAttribute(nameof(Subcategory), Subcategory));
            element.Add(new XAttribute(nameof(MTBF), MTBF));
            element.Add(new XAttribute(nameof(Description), Description));
            element.Add(new XAttribute(nameof(Manufacturer), Manufacturer));

            return element;
        }
    }
}
