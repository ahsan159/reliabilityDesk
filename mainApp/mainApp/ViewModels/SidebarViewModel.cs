using Prism.Events;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Xml;
using System.Xml.Linq;
using mainApp.Template;
using System.Collections.ObjectModel;
using Prism.Commands;
using System.Windows.Markup;
using System.Diagnostics;
using System.IO.Pipes;

namespace mainApp.ViewModels
{
    class SidebarViewModel : BindableBase
    {
        #region parameters
        private string ActivePartListName = "";
        public ReliabilityEntity? selectedEntity = null;
        public DelegateCommand NewPartAdditionCommand { get; private set; }
        public DelegateCommand<ReliabilityEntity> RemoveChildItem { get; private set; }
        public DelegateCommand OpenProperties { get; private set; }
        public DelegateCommand<ReliabilityEntity> TreeViewSelectionChanged { get; private set; }
        public DelegateCommand<ReliabilityEntity> AddToDiagramCommand { get; private set; }
        //public DelegateCommand AddCommand { get; private set; }

        private ObservableCollection<ReliabilityEntity> _projectTreeRel;

        public ObservableCollection<ReliabilityEntity> projectTreeRel
        {
            get
            {
                return _projectTreeRel;
            }
            set
            {
                SetProperty(ref _projectTreeRel, value);
            }
        }

        IEventAggregator _ea;
        #endregion

        #region constructor
        /// <summary>
        /// initialization of SidebarViewModel
        /// This will also take IEvent Aggregator 
        /// that will take for communicating with other
        /// view models
        /// </summary>
        /// <param name="ea"></param>
        public SidebarViewModel(IEventAggregator ea)
        {
            _ea = ea;
            _ea.GetEvent<OpenProjectFileEvent>().Subscribe(openProjectFile);
            _ea.GetEvent<SaveProjectFileEvent>().Subscribe(SaveProjectFile);
            _ea.GetEvent<ReliabilityTreeCalculationEvent>().Subscribe(CalculateReliability);
            _ea.GetEvent<SetActivePartListEvent>().Subscribe(SetActivePartList);
            _projectTreeRel = new ObservableCollection<ReliabilityEntity>();

            TreeViewSelectionChanged = new DelegateCommand<ReliabilityEntity>(SelectionChanged);
            RemoveChildItem = new DelegateCommand<ReliabilityEntity>(RemoveItemFromTree);
            AddToDiagramCommand = new DelegateCommand<ReliabilityEntity>(AddToDiagram);
            NewPartAdditionCommand = new DelegateCommand(NewPartAddition);

            //NewAssembly = new DelegateCommand(AddNewAssembly)
        }

        #endregion

        #region Event Aggregated function from toolbar
        /// <summary>
        /// Open a project file 
        /// name will be provided by toolbar
        /// this same function will open and 
        /// add reliability entities for 
        /// treeview
        /// </summary>
        /// <param name="fileName"></param>
        private void openProjectFile(string fileName)
        {
            //MessageBox.Show("Opening File : " + fileName);
            XDocument doc = XDocument.Load(fileName);
            XElement element = doc.Root;
            var r1 = getProjectTreeRel(element);
            projectTreeRel.Add(r1);
        }
        /// <summary>
        /// save project in both projecttree and diagram form
        /// for diagram saving event is published which will 
        /// be subscribed by content view
        /// </summary>
        /// <param name="FileName"></param>
        public void SaveProjectFile(string FileName)
        {
            XElement element = _projectTreeRel[0].GetXElement();
            XDocument doc = new XDocument(element);
            doc.Save(FileName);
            _ea.GetEvent<SaveDiagramFileEvent>().Publish(FileName + "Diagram.xml");
        }

        /// <summary>
        /// Function for calculation of reliability
        /// this is actual function  for calculating reliability
        /// </summary>
        /// <param name="TimeReliability"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void CalculateReliability(double TimeReliability)
        {
            projectTreeRel[0].CalculateReliability(TimeReliability);
        }
        /// <summary>
        /// set active partlist name
        /// </summary>
        /// <param name="obj"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void SetActivePartList(string obj)
        {
            ActivePartListName = obj;
        }

        #endregion

        #region selection changed method
        /// <summary>
        /// this will fire only on tree view selection changed event
        /// parameter will be passed for newly selected item
        /// </summary>
        /// <param name="rel"></param>
        public void SelectionChanged(ReliabilityEntity rel)
        {
            //MessageBox.Show(rel.Name + "," + rel.EntityType + "," + rel.MTBF);
            selectedEntity = rel;
        }
        #endregion

        #region Delegate command functions


        /// <summary>
        /// this function will add new part to selected
        /// assembly/project
        /// </summary>
        private void NewPartAddition()
        {
            
            // start the part list selector for selection of part
            Process proc = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.ArgumentList.Add(ActivePartListName);
            startInfo.FileName = "C:\\Users\\muhammadahsan\\source\\repos\\reliabilityDesk\\mainApp\\PartListSelector\\bin\\Debug\\net6.0-windows\\PartListSelector.exe";
            proc.StartInfo = startInfo;
            proc.Start();

            // get xml of selected part and convert to Xelement for addition in 
            // selected assembly
            NamedPipeClientStream client = new NamedPipeClientStream(".", "partTransferStream", PipeDirection.InOut, PipeOptions.Asynchronous);
            client.Connect();
            StreamReader reader = new StreamReader(client);
            string str = reader.ReadToEnd();            
            client.Dispose();
            XElement element = XElement.Parse(str);
            ReliabilityEntity rel = new ReliabilityEntity(element);
            selectedEntity.AddChild(rel);

        }
        public void RemoveItemFromTree(ReliabilityEntity rel)
        {
            if (selectedEntity.id == _projectTreeRel[0].id)
            {
                MessageBox.Show("Cannot Delete project" + projectTreeRel[0].id);
            }
            else
            {
                projectTreeRel[0].RemoveChild(selectedEntity);
            }
            //MessageBox.Show("Deteting" + selectedEntity.Name + "," + selectedEntity.EntityType + "," + selectedEntity.MTBF);
        }

        public void AddToDiagram(ReliabilityEntity rel)
        {
            if (selectedEntity.id == _projectTreeRel[0].id)
            {
                MessageBox.Show("cannot add project to diagram");
            }
            else
            {
                _ea.GetEvent<AddNewNodeEvent>().Publish(selectedEntity);
            }
        }

        #endregion

        #region populate Project Tree from Observable collection 
        private TreeViewItem getProjectTree(XElement element)
        {
            var tree = new TreeViewItem();
            if (element.HasAttributes)
            {
                foreach (XAttribute attrib in element.Attributes())
                {
                    if (attrib.Name == "Name")
                    {
                        tree.Header = element.Name + "," + attrib.Value;
                    }
                    else if (attrib.Name == "MTBF")
                    {
                        tree.Tag = attrib.Value;
                    }
                }
            }
            if (element.HasElements)
            {
                foreach (XElement child in element.Elements())
                {
                    tree.Items.Add(getProjectTree(child));
                }
            }
            return tree;
        }

        private ReliabilityEntity getProjectTreeRel(XElement element)
        {
            var tree = new ReliabilityEntity(element);
            //if (element.HasAttributes)
            //{
            //    foreach (XAttribute attrib in element.Attributes())
            //    {
            //        if (attrib.Name == "Name")
            //        {
            //            //tree.Header = element.Name + "," + attrib.Value;
            //            tree.setBase(attrib.Value, element.Name.ToString());
            //        }
            //        else if (attrib.Name == "MTBF")
            //        {
            //            tree.setMTBF(attrib.Value);
            //        }
            //    }
            //}
            //if (element.HasElements)
            //{
            //    foreach (XElement child in element.Elements())
            //    {
            //        tree.AddChild(getProjectTreeRel(child));
            //    }
            //}
            return tree;
        }
        #endregion

    }
}
