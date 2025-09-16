using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PartListSelector.Template
{
    public class Part : BindableBase
    {
        #region members of class
        private string _Name = "";
        private string _Category = "";
        private string _Subcategory = "";
        private string _Description = "";
        private string _MTBF = "";
        private string _Package = "";
        private string _Manufacturer = "";

        public string Name
        {
            set { SetProperty(ref _Name, value); }
            get { return _Name; }
        }

        public string Category
        {
            set { SetProperty(ref _Category, value); }
            get { return _Category; }
        }

        public string Subcategory
        {
            set { SetProperty(ref _Subcategory, value); }
            get { return _Subcategory; }
        }

        public string MTBF
        {
            set { SetProperty(ref _MTBF, value); }
            get { return _MTBF; }
        }

        public string Description
        {
            set { SetProperty(ref _Description, value); }
            get { return _Description; }
        }

        public string Package
        {
            set { SetProperty(ref _Package, value); }
            get { return _Package; }
        }

        public string Manufacturer
        {
            set { SetProperty(ref _Manufacturer, value); }
            get { return _Manufacturer; }
        }

        //public ObservableCollection<Part> PartCollection;
        #endregion

        #region constructor
        /// <summary>
        /// blank constructor for part initiation
        /// </summary>
        public Part()
        {

        }
        /// <summary>
        /// constructor for initializing new part entry
        /// </summary>
        /// <param name="name"></param>
        /// <param name="category"></param>
        /// <param name="subcategory"></param>
        /// <param name="description"></param>
        /// <param name="manufacturer"></param>
        public Part(string name, string category, string subcategory, string description,   string manufacturer) { 
            _Name = name;
            _Category = category;
            _Subcategory = subcategory;
            _Description = description;            
            _Manufacturer = manufacturer;                        
        }

        /// <summary>
        /// function to initialize and set part member parameters
        /// </summary>
        /// <param name="element"></param>
        public Part(XElement element)
        {
            //PartCollection = new ObservableCollection<Part>();
            _Name = element.Value;
            foreach (XAttribute a in element.Attributes())
            {
                if (a.Name == "Name")
                {
                    _Name = a.Value;
                }
                else if (a.Name == "mtbf" | a.Name == "MTBF")
                {
                    _MTBF = a.Value;
                }
                else if (a.Name == "des" | a.Name == "Description")
                {
                    _Description = a.Value;
                }
                else if (a.Name == "pack" | a.Name == "Package")
                {
                    _Package = a.Value;
                }
                else if (a.Name == "cat" | a.Name == "Category")
                {
                    _Category = a.Value;
                }
                else if (a.Name == "scat" | a.Name == "Subcategory")
                {
                    _Subcategory = a.Value;
                }
                else if (a.Name == "mftr" | a.Name == "Manufacturer")
                {
                    _Manufacturer = a.Value;
                }
            }

        }

        #endregion

        #region get command implementation

        /// <summary>
        /// Part data formatted as xml node
        /// </summary>
        /// <returns> XElement</returns>
        public XElement toXElement()
        {
            XElement element = new XElement("Part");
            if (_Name.Count() > 0)
            {
                element.Add(new XAttribute(nameof(Name), _Name));
            }
            if (_Category.Count() > 0)
            {
                element.Add(new XAttribute(nameof(Category), _Category));
            }
            if (_Subcategory.Count() > 0)
            {
                element.Add(new XAttribute(nameof(Subcategory), _Subcategory));
            }
            if (_MTBF.Count() > 0)
            {
                element.Add(new XAttribute(nameof(MTBF), _MTBF));
            }
            if (_Description.Count() > 0)
            {
                element.Add(new XAttribute(nameof(Description), _Description));
            }
            if (_Manufacturer.Count() > 0)
            {
                element.Add(new XAttribute(nameof(Manufacturer), _Manufacturer));
            }
            return element;
        }
        #endregion
    }
}
