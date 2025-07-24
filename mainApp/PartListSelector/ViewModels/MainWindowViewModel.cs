using PartListSelector.Template;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Xml.Linq;

namespace PartListSelector.ViewModels
{
    internal class MainWindowViewModel : BindableBase
    {
        private ObservableCollection<Part> _collection;

        private Part _selectedItem;

        public Part SelectedItem { 
            get { return _selectedItem; } 
            set { SetProperty(ref _selectedItem, value); }
        }
        public Part SelectedPart { get; set; }

        public int SelectedIndex { get; set; }
        public ObservableCollection<Part> collection
        {
            get
            {
                return _collection;
            }
            set
            {
                SetProperty(ref _collection, value);
            }
        }


        public DelegateCommand OpenFileCommand { set; get; }
        public DelegateCommand TestCommand { set; get; }


        public MainWindowViewModel()
        {
            _collection = new ObservableCollection<Part>();
            OpenFileCommand = new DelegateCommand(OpenFile);
            TestCommand = new DelegateCommand(TestFunction);
        }

        public void TestFunction()
        {
            MessageBox.Show(SelectedIndex.ToString());
            SelectedPart = collection[SelectedIndex];
            SelectedItem = collection[SelectedIndex];
            //MessageBox.Show(SelectedItem.ToString());
        }


        public void OpenFile()
        {
            //MessageBox.Show("Clicked");
            Microsoft.Win32.OpenFileDialog openFileDlg = new Microsoft.Win32.OpenFileDialog();
            openFileDlg.Filter = "Project File (*.prj)|*.prj|XML File (*.xml)|*.xml|All Files (*.*)|*.*";
            openFileDlg.FilterIndex = 2;
            openFileDlg.RestoreDirectory = true;
            openFileDlg.Multiselect = false;
            openFileDlg.InitialDirectory = System.IO.Directory.GetCurrentDirectory();
            bool result = (bool)openFileDlg.ShowDialog();
            if (result)
            {
                XDocument doc = XDocument.Load(openFileDlg.FileName);
                XElement element = (XElement)doc.FirstNode;
                //MessageBox.Show(element.Name.ToString());
                foreach (XElement x in element.Elements())
                {
                    var p = new Part(x);
                    _collection.Add(p);
                }

            }
        }
    }
}
