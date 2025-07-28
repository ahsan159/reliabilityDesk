using Prism.Mvvm;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Collections.ObjectModel;
using System.IO;

namespace ReliabilityReportPrinting.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private string _XmlFilePath;
        public string XmlFilePath
        {
            get { return _XmlFilePath; }
            set { SetProperty(ref _XmlFilePath, value); }
        }

        private Stream _DisplayDocument;
        public Stream DisplayDocumnet
        {
            get { return _DisplayDocument; }
            set { SetProperty(ref _DisplayDocument, value); }
        }

        public MainWindowViewModel()
        {
            string[] str = Environment.GetCommandLineArgs();
            //MessageBox.Show(str.Count().ToString());
            //foreach(string s in str)
            //{
            //    MessageBox.Show(s);
            //}
            _XmlFilePath = str[1];
            _DisplayDocument = new FileStream(_XmlFilePath, FileMode.Open);

        }
    }
}