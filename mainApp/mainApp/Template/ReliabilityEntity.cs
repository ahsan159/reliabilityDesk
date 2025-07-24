using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace mainApp.Template
{
    public class ReliabilityEntity : IComparable
    {
        #region define variables for reliability entity
        /// <summary>
        /// This will hold all the data required for 
        /// reliability entities 
        /// </summary>
        /// 
        public string id = Guid.NewGuid().ToString();
        public string Name { get; set; }
        public ReliabilityEntityType EntityType { get; set; }
        public string MTBF { get; set; } = "";
        public string Reliability { get; set; } = "";
        public string Header { get; set; } = "";
        public string Description { get; set; } = "";
        public string Designators { get; set; } = "";
        public ReliabilityEntity? Parent { get; set; } = null;
        public ObservableCollection<ReliabilityEntity> Child { get; set; } = null;
        public int Count { get; private set; } = 0;
        #endregion

        #region constructor
        /// <summary>
        /// Basic Constructor
        /// </summary>
        /// <param name="_name"></param>
        /// <param name="_entity"></param>
        public ReliabilityEntity(string _name, string _entity)
        {
            Name = _name;
            if (_entity == "Part")
            {
                EntityType = ReliabilityEntityType.Part;
            }
            else if (_entity == "Assembly")
            {
                EntityType = ReliabilityEntityType.Assembly;
            }
            else if (_entity == "Project")
            {
                EntityType = ReliabilityEntityType.Project;
            }
            Child = new ObservableCollection<ReliabilityEntity>();

        }
        /// <summary>
        /// addition with MTBF value
        /// </summary>
        /// <param name="_name"></param>
        /// <param name="_entity"></param>
        /// <param name="_MTBF"></param>
        public ReliabilityEntity(string _name, string _entity, double _MTBF)
        {
            Name = _name;
            if (_entity == "Part")
            {
                EntityType = ReliabilityEntityType.Part;
            }
            else if (_entity == "Assembly")
            {
                EntityType = ReliabilityEntityType.Assembly;
            }
            else if (_entity == "Project")
            {
                EntityType = ReliabilityEntityType.Project;
            }
            MTBF = _MTBF.ToString();
            Child = new ObservableCollection<ReliabilityEntity>();
        }
        public ReliabilityEntity(string _name, double _MTBF)
        {
            Name = _name;
            EntityType = ReliabilityEntityType.Part;
            MTBF = _MTBF.ToString();
            Child = new ObservableCollection<ReliabilityEntity>();
        }
        public ReliabilityEntity(string _name, ReliabilityEntityType _entity)
        {
            Name = _name;
            EntityType = _entity;
            Child = new ObservableCollection<ReliabilityEntity>();
        }
        public ReliabilityEntity(string _name, ReliabilityEntityType _entity, string _MTBF)
        {
            Name = _name;
            EntityType = _entity;
            MTBF = _MTBF;
            Child = new ObservableCollection<ReliabilityEntity>();
        }
        public ReliabilityEntity()
        {
            Child = new ObservableCollection<ReliabilityEntity>();
        }
        public ReliabilityEntity(XElement element)
        {
            Child = new ObservableCollection<ReliabilityEntity>();
            if (element.Name == "Part")
            {
                EntityType = ReliabilityEntityType.Part;
            }
            else if (element.Name == "Assembly")
            {
                EntityType = ReliabilityEntityType.Assembly;
            }
            else if (element.Name == "Project")
            {
                EntityType = ReliabilityEntityType.Project;
            }

            if (element.HasAttributes)
            {
                foreach (XAttribute a in element.Attributes())
                {
                    if (a.Name == "Name")
                    {
                        Name = a.Value;
                    }
                    else if (a.Name == "MTBF")
                    {
                        MTBF = a.Value;
                    }
                    else if (a.Name == "Reliability")
                    {
                        Reliability = a.Value;
                    }
                    else if (a.Name == "Header")
                    {
                        Header = a.Value;
                    }
                    else if (a.Name == "Description")
                    {
                        Description = a.Value;
                    }
                    else if (a.Name == "Designator")
                    {
                        Designators = a.Value;
                    }
                }
            }
            if (element.HasElements)
            {
                foreach (XElement e in element.Elements())
                {
                    var SingleChild = new ReliabilityEntity(e);
                    Child.Add(SingleChild);

                }
            }
        }


        #endregion

        public void setBase(string _name, string _entity)
        {
            Name = _name;
            if (_entity == "Part")
            {
                EntityType = ReliabilityEntityType.Part;
            }
            else if (_entity == "Assembly")
            {
                EntityType = ReliabilityEntityType.Assembly;
            }
            else if (_entity == "Project")
            {
                EntityType = ReliabilityEntityType.Project;
                Parent = null;
            }
        }
        public void setMTBF(string _MTBF)
        {
            MTBF = _MTBF;
        }
        public void setMTBF(double _MTBF)
        {
            MTBF = _MTBF.ToString();
        }
        public void AddChild(ReliabilityEntity rel)
        {
            Child.Add(rel);
            Count = Count + 1;
        }
        public void SetParent(ReliabilityEntity _rel)
        {
            Parent = _rel;
        }
        /// <summary>
        /// Cannot use foreach due to runtime error
        /// due to change in object cannot enumerate
        /// </summary>
        /// <param name="rel"></param>
        public void RemoveChild(ReliabilityEntity rel)
        {
            for (int i = 0; i < Child.Count; i++)
            {
                if (rel.CompareTo(Child[i]) == 1)
                {
                    Child.RemoveAt(i);
                    Count = Count - 1;
                }
                else
                {
                    Child[i].RemoveChild(rel);
                }
            }
        }
        public int CompareTo(object? obj)
        {
            if (obj == null)
            {
                return 0;
            }
            ReliabilityEntity otherRel = obj as ReliabilityEntity;
            if (otherRel == null)
            {
                return 0;
            }
            else
            {
                if (id == otherRel.id)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }

            }
        }
        public XElement GetXElement()
        {
            XElement element = new XElement(EntityType.ToString());
            if (Name.Length > 0)
            {
                element.Add(new XAttribute(nameof(Name), Name));
            }
            if (MTBF.Length > 0)
            {
                element.Add(new XAttribute(nameof(MTBF), MTBF));
            }
            if (Reliability.Length > 0)
            {

                element.Add(new XAttribute(nameof(Reliability), Reliability));
            }
            if (Description.Length > 0)
            {
                element.Add(new XAttribute(nameof(Description), Description));
            }
            if (Designators.Length > 0)
            {
                element.Add(new XAttribute(nameof(Designators), Designators));
            }
            foreach (ReliabilityEntity c in Child)
            {
                element.Add(c.GetXElement());
            }
            return element;
        }
    }
}
