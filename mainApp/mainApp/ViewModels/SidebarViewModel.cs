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

namespace mainApp.ViewModels
{
    class SidebarViewModel : BindableBase
    {
        #region parameters
        public ReliabilityEntity? selectedEntity = null;
        public DelegateCommand NewAssembly { get; private set; }
        public DelegateCommand NewPart { get; private set; }
        public DelegateCommand<ReliabilityEntity> RemoveChildItem { get; private set; }
        public DelegateCommand OpenProperties { get; private set; }
        public DelegateCommand<ReliabilityEntity> TreeViewSelectionChanged { get; private set; }
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
            _projectTreeRel = new ObservableCollection<ReliabilityEntity>();            

            TreeViewSelectionChanged = new DelegateCommand<ReliabilityEntity>(SelectionChanged);
            RemoveChildItem = new DelegateCommand<ReliabilityEntity>(RemoveItemFromTree);
            NewAssembly = new DelegateCommand(saveProjectFile);
            //NewAssembly = new DelegateCommand(AddNewAssembly)
        }
        #endregion

        #region Delegate command functions
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

        private void saveProjectFile()
        {
            savefileXelement();
        }

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
        public void RemoveItemFromTree(ReliabilityEntity rel)
        {
            if (selectedEntity.id == _projectTreeRel[0].id)
            {
                MessageBox.Show("Cannot Delete project" + projectTreeRel[0].id);
            }
            projectTreeRel[0].RemoveChild(selectedEntity);
            //MessageBox.Show("Deteting" + selectedEntity.Name + "," + selectedEntity.EntityType + "," + selectedEntity.MTBF);
        }

        public void savefileXelement()
        {
            XElement element = _projectTreeRel[0].GetXElement();
            XDocument doc = new XDocument(element);
            doc.Save("mynefile.xml");
        }

        #endregion

        private TreeViewItem getProjectTree(XElement element)
        {
            var tree = new TreeViewItem();
            if (element.HasAttributes)
            {
                foreach(XAttribute attrib in element.Attributes())
                {
                    if(attrib.Name=="Name")
                    {
                        tree.Header = element.Name+","+attrib.Value;
                    }    
                    else if(attrib.Name=="MTBF")
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
            var tree = new ReliabilityEntity();
            if (element.HasAttributes)
            {
                foreach (XAttribute attrib in element.Attributes())
                {
                    if (attrib.Name == "Name")
                    {
                        //tree.Header = element.Name + "," + attrib.Value;
                        tree.setBase(attrib.Value, element.Name.ToString());
                    }
                    else if (attrib.Name == "MTBF")
                    {
                        tree.setMTBF(attrib.Value);
                    }
                }
            }
            if (element.HasElements)
            {
                foreach (XElement child in element.Elements())
                {
                    tree.AddChild(getProjectTreeRel(child));
                }
            }
            return tree;
        }

    }
}
