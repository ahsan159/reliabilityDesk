using PartListSelector.Template;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Xml.Linq;
using System.IO.Pipes;
using System.Diagnostics;
using PartListSelector.Views;

namespace PartListSelector.ViewModels
{
    internal class MainWindowViewModel : BindableBase
    {

        #region class member
        private string ActiveFileName = "projectID3.xml";
        private ObservableCollection<Part> _collection;

        private Part _selectedItem;
        public Part SelectedItem
        {
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
        public DelegateCommand SaveFileCommand { get; set; }
        public DelegateCommand SaveFileAsCommand { get; set; }
        public DelegateCommand AddNewPartCommand { get; set; }
        public DelegateCommand DeletePartCommand { get; set; }
        public DelegateCommand SelectPartCommand { get; set; }
        //public DelegateCommand TestCommand { set; get; }

        #endregion

        #region constructor
        /// <summary>
        /// consturctor will also check for any arguments received 
        /// during the call otherwise partlist will be required to
        /// open manually
        /// </summary>
        public MainWindowViewModel()
        {
            _collection = new ObservableCollection<Part>();
            OpenFileCommand = new DelegateCommand(OpenFile);
            SaveFileCommand = new DelegateCommand(SaveFile);
            SaveFileAsCommand = new DelegateCommand(SaveAsFile);
            AddNewPartCommand = new DelegateCommand(AddNewPart);
            DeletePartCommand = new DelegateCommand(DeletePart);
            SelectPartCommand = new DelegateCommand(SelectPartToSend);

            if (Environment.GetCommandLineArgs().Count() > 1)
            {
                string[] args = Environment.GetCommandLineArgs();
                string bufFileName = args[1];
                if (File.Exists(bufFileName))
                {
                    ActiveFileName = bufFileName;
                }
                else
                {
                    OpenFile();
                }
            }
            //TestCommand = new DelegateCommand(TestFunction);
        }
        /// <summary>
        /// consturctor will also check for any arguments received 
        /// during the call otherwise partlist will be required to
        /// open manually
        /// </summary>
        public MainWindowViewModel(object var)
        {
            _collection = new ObservableCollection<Part>();
            OpenFileCommand = new DelegateCommand(OpenFile);
            SaveFileCommand = new DelegateCommand(SaveFile);
            SaveFileAsCommand = new DelegateCommand(SaveAsFile);
            AddNewPartCommand = new DelegateCommand(AddNewPart);
            DeletePartCommand = new DelegateCommand(DeletePart);
            SelectPartCommand = new DelegateCommand(SelectPartToSend);

            if (Environment.GetCommandLineArgs().Count() > 1)
            {
                string[] args = Environment.GetCommandLineArgs();
                string bufFileName = args[1];
                if (File.Exists(bufFileName))
                {
                    ActiveFileName = bufFileName;
                    _collection.Clear();
                    XDocument doc = XDocument.Load(ActiveFileName);
                    XElement element = (XElement)doc.FirstNode;
                    //MessageBox.Show(element.Name.ToString());
                    foreach (XElement x in element.Elements())
                    {
                        var p = new Part(x);
                        _collection.Add(p);
                    }
                }
                else
                {
                    OpenFile();
                }
            }
        }

        #endregion

        #region delegate command implementations
        private void SelectPartToSend()
        {
            XElement element = SelectedItem.toXElement();
            //element.ToString();
            //MessageBox.Show(element.ToString());

            //NamedPipeClientStream client = new NamedPipeClientStream(".", "partTransferStream", PipeDirection.InOut, PipeOptions.Asynchronous);
            //client.Connect();

            NamedPipeServerStream server = new NamedPipeServerStream("partTransferStream", PipeDirection.InOut);
            server.WaitForConnection();
            byte[] bytes = Encoding.UTF8.GetBytes(element.ToString());
            server.Write(bytes, 0, bytes.Length);
            server.WaitForPipeDrain();
            server.Disconnect();
            server.Dispose();
            MessageBox.Show("Sent Complete");
            Application.Current.Shutdown();
            //throw new NotImplementedException();
        }
        private void DeletePart()
        {
            throw new NotImplementedException();
        }

        private void AddNewPart()
        {
            NewPartDialog dialog = new NewPartDialog();
            dialog.ShowDialog();
            Debugger.Break();
        }

        private void SaveAsFile()
        {
            Microsoft.Win32.SaveFileDialog saveFileDlg = new Microsoft.Win32.SaveFileDialog();
            saveFileDlg.Filter = "Project File (*.prj)|*.prj|XML File (*.xml)|*.xml|All Files (*.*)|*.*";
            saveFileDlg.FilterIndex = 2;
            saveFileDlg.RestoreDirectory = true;
            //saveFileDlg.Multiselect = false;
            saveFileDlg.InitialDirectory = System.IO.Directory.GetCurrentDirectory();
            bool result = (bool)saveFileDlg.ShowDialog();
            if (result)
            {
                ActiveFileName = saveFileDlg.FileName;
                SaveFile();
            }
            //throw new NotImplementedException();
        }

        private void SaveFile()
        {
            XElement partList = new XElement("PartList");
            foreach (Part p in _collection)
            {
                partList.Add(p.toXElement());
            }
            XDocument doc = new XDocument(partList);
            doc.Save(ActiveFileName);
            //throw new NotImplementedException();
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
                _collection.Clear();
                XDocument doc = XDocument.Load(openFileDlg.FileName);
                ActiveFileName = openFileDlg.FileName;
                XElement element = (XElement)doc.FirstNode;
                //MessageBox.Show(element.Name.ToString());
                foreach (XElement x in element.Elements())
                {
                    var p = new Part(x);
                    _collection.Add(p);
                }

            }
        }
        #endregion
    }
}
