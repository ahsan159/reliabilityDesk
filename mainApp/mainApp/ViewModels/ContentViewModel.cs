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
            AnnotationEditorViewModel a = new AnnotationEditorViewModel();
            a.Content = rel.Name;
            a.FontSize = 24;
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

            _NodeCollection.Add(AddedNode);

            // Add
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
