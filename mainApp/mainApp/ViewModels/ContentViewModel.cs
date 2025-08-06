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

namespace mainApp.ViewModels
{
    internal class ContentViewModel : BindableBase
    {
        #region class members
        IEventAggregator _eaSave;

        IEventAggregator _eaAddNode;

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
            _eaAddNode.GetEvent<AddNewNodeEvent>().Subscribe(AddItem);
            _NodeCollection = new ObservableCollection<NodeViewModel>();
            _ConnectorCollection = new ObservableCollection<ConnectorViewModel>();
            ItemAddCommand = new DelegateCommand<ReliabilityEntity>(AddItem);

            // Create Begin Node
            AnnotationCollection aCollect = new AnnotationCollection();
            AnnotationEditorViewModel a = new AnnotationEditorViewModel();
            a.Content = "Begin";
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
            AnnotationEditorViewModel a1 = new AnnotationEditorViewModel();
            a1.Content = "End";
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
            AddedNode1.ID = "begin";
            AddedNode1.Key = "begin";
            AddedNode1.OffsetX = 400;
            AddedNode1.OffsetY = 500;
            AddedNode1.UnitHeight = 60;
            AddedNode1.UnitWidth = 120;
            AddedNode1.Ports = pCollect1;
            AddedNode1.Annotations = aCollect1;

            _NodeCollection.Add(AddedNode1);
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

            NodeViewModel AddedNode = new NodeViewModel();
            AddedNode.ID = rel.id;
            AddedNode.Key = rel.id;
            AddedNode.OffsetX = 400;
            AddedNode.OffsetY = 500;
            AddedNode.UnitHeight = 60;
            AddedNode.UnitWidth = 120;
            AddedNode.Ports = pCollect;
            AddedNode.Annotations = aCollect;

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

        #endregion

    }
}
