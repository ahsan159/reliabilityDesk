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
    internal class ContentViewModel:BindableBase
    {
        IEventAggregator _ea;
        public DelegateCommand ItemAddCommand { get; set; }

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

        public ContentViewModel()
        {

            _ea.GetEvent<SaveDiagramFileEvent>().Subscribe(SaveDiagram);
            _NodeCollection = new ObservableCollection<NodeViewModel>();
            _ConnectorCollection = new ObservableCollection<ConnectorViewModel>();
            ItemAddCommand = new DelegateCommand(AddItem);

            AnnotationCollection a1 = new AnnotationCollection();
            AnnotationEditorViewModel t1 = new AnnotationEditorViewModel();
            t1.ReadOnly = true;
            t1.Content = "Begin";
            a1.Add(t1);

            AnnotationCollection a2 = new AnnotationCollection();
            AnnotationEditorViewModel t2 = new AnnotationEditorViewModel();
            t2.Content = "End";
            t2.ReadOnly = true;
            a2.Add(t2);

            NodeViewModel n1 = new NodeViewModel();
            n1.ID = "Begin";
            n1.OffsetX = 300;
            n1.OffsetY = 400;
            n1.UnitHeight = 80;
            n1.UnitWidth = 160;
            n1.Shape = Syncfusion.UI.Xaml.Diagram.Shapes.Star;
            _NodeCollection.Add(n1);
            n1.Annotations = a1;

            NodeViewModel n2 = new NodeViewModel();
            n2.ID = "End";
            n2.OffsetX = 400;
            n2.OffsetY = 500;
            n2.UnitHeight = 60;
            n2.UnitWidth = 120;
            n2.Shape = Syncfusion.UI.Xaml.Diagram.Shapes.Octagon;
            _NodeCollection.Add(n2);
            n2.Annotations = a2;


            ConnectorViewModel c1 = new ConnectorViewModel();
            c1.SourceNode = n1;
            c1.TargetNode = n2;
            c1.CornerRadius = 0;
            _ConnectorCollection.Add(c1);
        }

        private void AddItem()
        {
            
        }
       
        private void SaveDiagram(string FileName)
        {
            MessageBox.Show(FileName);
            
            //using (Stream str = File.Open(FileName, FileMode.CreateNew))
            //{
                
            //}
        }
    }
}
