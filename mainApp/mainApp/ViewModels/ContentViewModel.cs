using mainApp.Template;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Syncfusion.UI.Xaml.Diagram;
using Prism.Events;
using System.IO;
using System.Windows;
using Syncfusion.UI.Xaml.Diagram.Controls;
using Syncfusion.UI.Xaml.Diagram.Serializer;
using System.Windows.Media;
using mainApp.Views;
using Microsoft.Win32;
using System.Xml.Schema;
using System.Xml;
using System.Windows.Xps.Packaging;

namespace mainApp.ViewModels
{
    internal class ContentViewModel : BindableBase
    {
        #region class members
        IEventAggregator _eaSave;

        IEventAggregator _eaAddNode;

        private string ActiveFileName = "";

        public DelegateCommand<ReliabilityEntity> ItemAddCommand { get; set; }

        private ObservableCollection<NodeViewModel> _NodeCollection;

        public ObservableCollection<NodeViewModel> NodeCollection
        {
            get { return _NodeCollection; }
            set { SetProperty(ref _NodeCollection, value); }
        }

        private ObservableCollection<ConnectorViewModel> _ConnectorCollection;

        public ObservableCollection<ConnectorViewModel> ConnectorCollection
        {
            get { return _ConnectorCollection; }
            set { SetProperty(ref _ConnectorCollection, value); }
        }

        #endregion

        #region constructor
        /// <summary>
        /// Only consturctor with event aggregator for file saving 
        /// and node addition
        /// </summary>
        /// <param name="ea"></param>
        public ContentViewModel(IEventAggregator ea)
        {
            _eaSave = ea;
            _eaAddNode = ea;
            _eaSave.GetEvent<SaveDiagramFileEvent>().Subscribe(SaveDiagram);
            _eaSave.GetEvent<SolveDiagramEvent>().Subscribe(SolveDiagram);
            _eaSave.GetEvent<OpenProjectDiagramEvent>().Subscribe(LoadDiagram);
            _eaAddNode.GetEvent<AddNewNodeEvent>().Subscribe(AddItem);
            _NodeCollection = new ObservableCollection<NodeViewModel>();
            _ConnectorCollection = new ObservableCollection<ConnectorViewModel>();
            ItemAddCommand = new DelegateCommand<ReliabilityEntity>(AddItem);

            // Create Begin Node
            AnnotationCollection aCollect = new AnnotationCollection();
            TextAnnotationViewModel a = new TextAnnotationViewModel();
            a.Text = "Begin";
            a.FontSize = 24;
            a.ReadOnly = true;
            aCollect.Add(a);

            // Add points to connect with connector
            PortCollection pCollect = new PortCollection();
            NodePortViewModel PRight = new NodePortViewModel();
            PRight.NodeOffsetX = 1;
            PRight.NodeOffsetY = 0.5;
            pCollect.Add(PRight);

            NodeViewModel AddedNode = new NodeViewModel();
            AddedNode.ID = "begin";
            AddedNode.Key = "begin";
            AddedNode.OffsetX = 400;
            AddedNode.OffsetY = 500;
            AddedNode.UnitHeight = 60;
            AddedNode.UnitWidth = 120;
            AddedNode.Ports = pCollect;
            AddedNode.Annotations = aCollect;

            _NodeCollection.Add(AddedNode);

            // Create End Node
            AnnotationCollection aCollect1 = new AnnotationCollection();
            TextAnnotationViewModel a1 = new TextAnnotationViewModel();
            a1.Text = "End";
            a1.FontSize = 24;
            a1.ReadOnly = true;
            aCollect1.Add(a1);

            // Add points to connect with connector
            PortCollection pCollect1 = new PortCollection();
            NodePortViewModel PLeft = new NodePortViewModel();
            PLeft.NodeOffsetX = 0;
            PLeft.NodeOffsetY = 0.5;
            pCollect.Add(PLeft);

            NodeViewModel AddedNode1 = new NodeViewModel();
            AddedNode1.ID = "end";
            AddedNode1.Key = "end";
            AddedNode1.OffsetX = 400;
            AddedNode1.OffsetY = 500;
            AddedNode1.UnitHeight = 60;
            AddedNode1.UnitWidth = 120;
            AddedNode1.Ports = pCollect1;
            AddedNode1.Annotations = aCollect1;

            _NodeCollection.Add(AddedNode1);
        }

        #endregion

        #region Event Aggregated functions

        /// <summary>
        /// This function to solve the diagram
        /// of the project.
        /// </summary>
        /// <param name="MissionTime"></param>
        private void SolveDiagram(double MissionTime)
        {
            double reliabilityValue = 1.0;
            if (!File.Exists(ActiveFileName))
            {
                MessageBox.Show("Cannot find file: " + ActiveFileName);
                return;
            }
            XDocument doc = XDocument.Load(ActiveFileName);
            XElement rootElement = doc.Root;
            ReliabilityEntity tempProjectTree = new ReliabilityEntity(rootElement);
            Dictionary<string, double> dictionary = new Dictionary<string, double>();
            CreateDictionary(tempProjectTree, ref dictionary);
            NodeViewModel Source = _NodeCollection.First(n => n.Key.ToString() == "begin");
            while (Source.Key.ToString() != "end")
            {
                double rel;
                try
                {
                    rel = dictionary[Source.Key.ToString()];
                }
                catch (Exception e)
                {
                    rel = 1;
                }
                reliabilityValue *= rel;
                ConnectorViewModel c = _ConnectorCollection.First(c => c.SourceNode == Source);
                NodeViewModel Target = _NodeCollection.First(n => n == c.TargetNode);
                MessageBox.Show("Next Node: " + Target.Key + "\nReliability: " + reliabilityValue.ToString() + "\nRel: " + rel);
                Source = Target;
            }
            MessageBox.Show("Diagram Solved");

        }

        private void CreateDictionary(ReliabilityEntity root, ref Dictionary<string, double> dictionary)
        {
            dictionary.Add(root.id, double.Parse(root.Reliability));
            if (root.Child.Count > 0)
            {
                foreach (ReliabilityEntity e in root.Child)
                {
                    CreateDictionary(e, ref dictionary);
                }
            }
        }


        /// <summary>
        /// save diagram data
        /// saving diagram data has proven difficult in MVVM
        /// because I am unable to understand referencing from 
        /// other view models without exposing view to viewmodel
        /// therefore i have crated a dummy diagram and add
        /// nodes and ports to it and serializing dummy diagram
        /// has worked.
        /// </summary>
        /// <param name="FileName"></param>
        private void SaveDiagram(string FileName)
        {
            //MessageBox.Show(FileName);            
            SfDiagram d = new SfDiagram();
            d.Nodes = _NodeCollection;
            d.Connectors = _ConnectorCollection;
            //using (Stream str = File.Open(FileName, FileMode.Create))

            using (Stream str = File.Open(FileName, FileMode.Create))
            {
                d.Save(str);
            }
        }

        /// <summary>
        /// Load previously saved file
        /// </summary>
        /// <param name="FileName"></param>
        private void LoadDiagram(string FileName)
        {
            ActiveFileName = FileName;
            string diagramFileName = FileName + "Diagram.xml";
            //MessageBox.Show(diagramFileName);
            if (!File.Exists(diagramFileName))
            {
                MessageBox.Show("File Not Found", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Load the diagram from xml and remove schema
            XDocument doc = XDocument.Load(diagramFileName);
            XElement ele = doc.Root;
            XElement schemaLess = RemoveSchema(ele);

            XName nameNodes = "Nodes";
            XName nameConnectors = "Connectors";

            // clear any existring diagram entities
            _NodeCollection.Clear();
            _ConnectorCollection.Clear();

            // this node dictionary will be created to generate
            // connection from the previous loading xml file
            Dictionary<string, int> nodeDictionary = new Dictionary<string, int>();

            // this part will iterate through nodes and add them 
            // one by one             
            XElement nodes = schemaLess.Element(nameNodes);
            foreach (XElement n in nodes.Elements())
            {
                _NodeCollection.Add(CreateNodeXML(n));
                nodeDictionary.Add(n.Element("InternalID").Value.ToString(), _NodeCollection.Count - 1);
            }

            // this part will iterate through connectors and add them 
            // one by one             
            XElement connectors = schemaLess.Element(nameConnectors);
            foreach (XElement c in connectors.Elements())
            {
                _ConnectorCollection.Add(CreateConnectorXML(c, nodeDictionary));
            }

            #region commented region as origional code provided by syncfusion is not working
            //using (Stream fileStream = File.Open(diagramFileName, FileMode.OpenOrCreate, FileAccess.ReadWrite))
            //{
            //    try
            //    {
            //        d.Upgrade(fileStream);
            //        d.Load(fileStream);
            //    }
            //    catch (Exception e)
            //    {
            //        MessageBox.Show(e.ToString());
            //    }
            //}

            //SfDiagram diagram = new SfDiagram();
            //OpenFileDialog openfile = new OpenFileDialog();
            //openfile.ShowDialog();
            //using (Stream fileStream = openfile.OpenFile())
            //{
            //    diagram.Upgrade(fileStream);
            //    diagram.Load(fileStream);
            //}

            //_NodeCollection = (ObservableCollection<NodeViewModel>)d.Nodes;
            //_ConnectorCollection = (ObservableCollection<ConnectorViewModel>)d.Connectors; 
            #endregion

        }

        /// <summary>
        /// this function will create a node from XElement
        /// by iterating through XElement parameters and setting 
        /// NodeViewModel parameters and adding it to the
        /// nodecollection
        /// this function is during load of diagram from xml
        /// document
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        private NodeViewModel CreateNodeXML(XElement n)
        {
            NodeViewModel node = new NodeViewModel();
            XElement nodeAnnot = n.Element("Annotations");
            AnnotationCollection aCollect = new AnnotationCollection();
            foreach (XElement anot in nodeAnnot.Elements())
            {
                aCollect.Add(GetAnnotation(anot));
            }

            // Add points to connect with connector
            PortCollection pCollect = new PortCollection();
            NodePortViewModel PLeft = new NodePortViewModel();
            PLeft.NodeOffsetX = 0;
            PLeft.NodeOffsetY = 0.5;
            NodePortViewModel PRight = new NodePortViewModel();
            PRight.NodeOffsetX = 1;
            PRight.NodeOffsetY = 0.5;
            pCollect.Add(PLeft);
            pCollect.Add(PRight);


            node.Annotations = aCollect;
            node.Ports = pCollect;

            node.UnitHeight = int.Parse(n.Element("Height").Value);
            node.UnitWidth = int.Parse(n.Element("Width").Value);
            node.OffsetX = int.Parse(n.Element("OffsetX").Value);
            node.OffsetY = int.Parse(n.Element("OffsetY").Value);
            node.Key = n.Element("Key").Value;
            node.ID = n.Element("InternalID").Value;

            DiagramMenuItem PropertiesMenuItem = new DiagramMenuItem()
            {
                Content = "Properties",
                Command = new DelegateCommand<NodeViewModel>(NodeProperties),
                CommandParameter = node
            };
            node.Constraints = node.Constraints | NodeConstraints.Menu;
            //AddedNode.Constraints = AddedNode.Constraints & ~NodeConstraints.InheritMenu;
            node.Menu = new DiagramMenu();
            node.Menu.MenuItems = new ObservableCollection<DiagramMenuItem>();
            (node.Menu.MenuItems as ICollection<DiagramMenuItem>).Add(PropertiesMenuItem);
            return node;
        }

        /// <summary>
        /// this function will create a node from XElement
        /// by iterating through XElement parameters and setting 
        /// ConnectorViewModel parameters and adding it to the
        /// connectorcollection
        /// this function is during load of diagram from xml
        /// document
        /// </summary>
        /// <param name="c"></param>
        /// <param name="nodeDictionary"></param>
        /// <returns></returns>
        private ConnectorViewModel CreateConnectorXML(XElement c, Dictionary<string, int> nodeDictionary)
        {
            //NodeViewModel source = _NodeCollection.ElementAt(nodeDictionary[c.Element("SourceId").Value.ToString()]);
            //NodeViewModel target = _NodeCollection.ElementAt(nodeDictionary[c.Element("TargetId").Value.ToString()]);
            ConnectorViewModel conn = new ConnectorViewModel()
            {
                SourceNode = _NodeCollection.ElementAt(nodeDictionary[c.Element("SourceId").Value.ToString()]),
                TargetNode = _NodeCollection.ElementAt(nodeDictionary[c.Element("TargetId").Value.ToString()])
            };
            return conn;
        }

        /// <summary>
        /// this function will create a annotation from XElement
        /// by iterating through XElement parameters and setting 
        /// TextAnnotationViewModel parameters and adding it to the
        /// Annotation Collection
        /// this function is during load of diagram from xml
        /// document
        /// </summary>
        /// <param name="ele"></param>
        /// <returns></returns>
        private TextAnnotationViewModel GetAnnotation(XElement ele)
        {
            TextAnnotationViewModel a = new TextAnnotationViewModel();
            //MessageBox.Show(ele.Element("Text").Value.ToString());
            a.Text = ele.Element("Text").Value.ToString();
            string vAlign = ele.Element("VerticalAlignment").Value;
            if (vAlign == "Top")
            {
                a.VerticalAlignment = VerticalAlignment.Top;
            }
            else if (vAlign == "Bottom")
            {
                a.VerticalAlignment = VerticalAlignment.Bottom;
            }
            else if (vAlign == "Center")
            {
                a.VerticalAlignment = VerticalAlignment.Center;
            }
            a.FontSize = 20;
            a.FontWeight = FontWeights.Light;
            a.ReadOnly = true;
            a.TextWrapping = TextWrapping.NoWrap;
            return a;
        }
        /// <summary>
        /// This function removes all the schema from the xml
        /// file. which makes retrieving data from file
        /// easier by comparing node name and values
        /// this function is edited form of answer from
        /// https://stackoverflow.com/questions/987135/how-to-remove-all-namespaces-from-xml-with-c
        /// </summary>
        /// <param name="xmlDocument"></param>
        /// <returns></returns>
        public XElement RemoveSchema(XElement xmlDocument)
        {
            if (!xmlDocument.HasElements)
            {
                XElement xElement = new XElement(xmlDocument.Name.LocalName);
                xElement.Value = xmlDocument.Value;

                foreach (XAttribute attribute in xmlDocument.Attributes())
                    xElement.Add(new XAttribute(attribute.Name.LocalName, attribute.Value));

                return xElement;
            }
            XElement output = new XElement(xmlDocument.Name.LocalName);
            XElement temp = output;
            temp.RemoveNodes();
            output.Value = temp.Value;
            output.Add(xmlDocument.Elements().Select(el => RemoveSchema(el)));
            return output;
        }

        #endregion

        #region command function
        // part where delegate command function are referenced

        /// <summary>
        /// add a new node a event called from project tree in
        /// side bar
        /// </summary>
        private void AddItem(ReliabilityEntity rel)
        {
            //MessageBox.Show(rel.id.ToString() + "," + rel.Name);

            // Display Node Text
            AnnotationCollection aCollect = new AnnotationCollection();
            TextAnnotationViewModel a = new TextAnnotationViewModel();
            a.Text = rel.Name;
            a.VerticalAlignment = VerticalAlignment.Top;
            a.FontSize = 36;
            a.FontWeight = FontWeights.Bold;
            a.ReadOnly = true;
            aCollect.Add(a);

            //TextAnnotationViewModel aReliability = new TextAnnotationViewModel();
            //aReliability.Text = rel.Reliability;
            //aReliability.VerticalAlignment = VerticalAlignment.Top;
            //aReliability.FontSize = 36;
            //aReliability.FontWeight = FontWeights.Bold;
            //aReliability.ReadOnly = true;
            //aCollect.Add(aReliability);

            // Add points to connect with connector
            PortCollection pCollect = new PortCollection();
            NodePortViewModel PLeft = new NodePortViewModel();
            PLeft.NodeOffsetX = 0;
            PLeft.NodeOffsetY = 0.5;
            NodePortViewModel PRight = new NodePortViewModel();
            PRight.NodeOffsetX = 1;
            PRight.NodeOffsetY = 0.5;
            pCollect.Add(PLeft);
            pCollect.Add(PRight);

            NodeViewModel AddedNode = new NodeViewModel()
            {
                ID = rel.id,
                Key = rel.id,
                OffsetX = 400,
                OffsetY = 500,
                UnitHeight = 60,
                UnitWidth = 120,
                Ports = pCollect,
                Annotations = aCollect
            };
            //AddedNode.ID = rel.id;
            //AddedNode.Key = rel.id;
            //AddedNode.OffsetX = 400;
            //AddedNode.OffsetY = 500;
            //AddedNode.UnitHeight = 60;
            //AddedNode.UnitWidth = 120;
            //AddedNode.Ports = pCollect;
            //AddedNode.Annotations = aCollect;

            // Add context menu to node
            DiagramMenuItem PropertiesMenuItem = new DiagramMenuItem()
            {
                Content = "Properties",
                Command = new DelegateCommand<NodeViewModel>(NodeProperties),
                CommandParameter = AddedNode
            };
            AddedNode.Constraints = AddedNode.Constraints | NodeConstraints.Menu;
            //AddedNode.Constraints = AddedNode.Constraints & ~NodeConstraints.InheritMenu;
            AddedNode.Menu = new DiagramMenu();
            AddedNode.Menu.MenuItems = new ObservableCollection<DiagramMenuItem>();
            (AddedNode.Menu.MenuItems as ICollection<DiagramMenuItem>).Add(PropertiesMenuItem);
            _NodeCollection.Add(AddedNode);

            // Add
        }

        /// <summary>
        /// Add a series or parallel diagram status
        /// </summary>
        /// <param name="obj"></param>
        private void NodeProperties(NodeViewModel obj)
        {
            AnnotationCollection NodeAnnotations = (AnnotationCollection)obj.Annotations;
            if (NodeAnnotations.Count == 2)
            {
                // remove last annotation 
                // this will happen if user wants to 
                // add another configuration of 
                // series or parallel 
                NodeAnnotations.RemoveAt(1);
            }
            TextAnnotationViewModel? old = NodeAnnotations[0] as TextAnnotationViewModel;
            string unitName = old.Text;
            DiagramInputPropery diagramInputPorpertyDlg = new DiagramInputPropery(unitName);
            //MessageBox.Show(unitName);
            diagramInputPorpertyDlg.ShowDialog();
            if (diagramInputPorpertyDlg.result)
            {
                TextAnnotationViewModel a = new TextAnnotationViewModel();
                a.Text = diagramInputPorpertyDlg.ConfigurationString;
                //a.TextDecorations = TextDecorations.Underline;
                a.VerticalAlignment = VerticalAlignment.Bottom;
                a.FontSize = 20;
                a.FontWeight = FontWeights.Light;
                a.ReadOnly = true;
                a.TextWrapping = TextWrapping.NoWrap;

                NodeAnnotations.Add(a);
                obj.Annotations = NodeAnnotations;
            }


            //throw new NotImplementedException();
        }

        #endregion

    }
}
