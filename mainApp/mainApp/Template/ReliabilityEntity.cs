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

        public ReliabilityCalculation CalculationType { get; set; } = ReliabilityCalculation.Series;
        public ObservableCollection<ReliabilityEntity> Child { get; set; } = null;
        public int Count { get; private set; } = 0;
        public string Quantity { get; set; } = "1";
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
                    else if (a.Name == "id")
                    {
                        id = a.Value;
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
                    else if (a.Name == "CalculationType")
                    {
                        if (a.Value == "Parallel")
                        {
                            CalculationType = ReliabilityCalculation.Parallel;
                        }
                        else
                        {
                            CalculationType = ReliabilityCalculation.Series;
                        }
                    }
                    else if (a.Name == "Quantity")
                    {
                        Quantity = a.Value;
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

        #region member function 
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
        #endregion

        #region help functions to manage project tree
        /// <summary>
        /// Add Child if it is not part
        /// </summary>
        /// <param name="rel"></param>
        public void AddChild(ReliabilityEntity rel)
        {
            if (EntityType != ReliabilityEntityType.Part)
            {
                Child.Add(rel);
            }
        }
        /// <summary>
        /// Setting parent is for future expected use
        /// </summary>
        /// <param name="_rel"></param>
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
        /// <summary>
        /// function used when project is being saved and all the 
        /// reliability entities and thier children are being fetched
        /// and converted into xml data file
        /// </summary>
        /// <returns></returns>
        public XElement GetXElement()
        {
            XElement element = new XElement(EntityType.ToString());
            if (id.Length > 0)
            {
                element.Add(new XAttribute(nameof(id), id));
            }
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
            if (CalculationType == ReliabilityCalculation.Series || CalculationType == ReliabilityCalculation.Parallel)
            {
                element.Add(new XAttribute(nameof(CalculationType), CalculationType.ToString()));
            }
            if (Quantity.Length > 0)
            {
                element.Add(new XAttribute(nameof(Quantity), Quantity));
            }
            foreach (ReliabilityEntity c in Child)
            {
                element.Add(c.GetXElement());
            }
            return element;
        }
        #endregion

        #region reliability Calculation
        /// <summary>
        /// Thisi is the original functional that will calculate reliability
        /// and MTBF of entity using subassemblies
        /// </summary>
        /// <param name="TimeHour"></param>
        public void CalculateReliability(double TimeHour)
        {
            // variable to store reliability of assembly/project
            double finalReliability = 1;
            double finalQuantity = -1;
            if (!double.TryParse(Quantity, out finalQuantity))
            {
                finalQuantity = -1;
            }
            if (EntityType == ReliabilityEntityType.Part)
            {
                double MTBFDouble;
                if (!double.TryParse(MTBF, out MTBFDouble))
                {
                    //do something if conversino fails
                    //throw Exception
                    Reliability = "-1";
                }
                else
                {
                    double ReliabilityDouble = Math.Exp(-TimeHour / MTBFDouble);
                    if (CalculationType == ReliabilityCalculation.Series)
                    {
                        ReliabilityDouble = Math.Pow(ReliabilityDouble, finalQuantity);
                    }
                    else if (CalculationType == ReliabilityCalculation.Parallel)
                    {
                        ReliabilityDouble = 1 - Math.Pow((1 - ReliabilityDouble), finalQuantity);
                    }
                    Reliability = ReliabilityDouble.ToString();
                }
            }
            else
            {
                if (Child.Count > 0)
                {
                    foreach (ReliabilityEntity c in Child)
                    {
                        c.CalculateReliability(TimeHour);
                        finalReliability *= double.Parse(c.Reliability);
                    }
                    Reliability = finalReliability.ToString();
                    double MTBFCalculation = -TimeHour / Math.Log(finalReliability);
                    MTBF = MTBFCalculation.ToString();
                }
                else
                {
                    MTBF = "1";
                    Reliability = "1";
                }
            }
        }
        #endregion
    }
}
