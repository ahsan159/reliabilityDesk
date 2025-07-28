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
        #region class member functions
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
        #endregion
        
        #region constructor
        /// <summary>
        /// Constructor to initialize class.
        /// Please, note that this class constructor must be public
        /// to set the member variables to initialize correctly.
        /// </summary>
        public MainWindowViewModel()
        {
            // get the command line argument 
            // command line argument will have the path of the project
            // xml file which will be converted into html or pdf format
            // for report viewing
            string[] str = Environment.GetCommandLineArgs();            
            _XmlFilePath = str[1];
            _DisplayDocument = new FileStream(_XmlFilePath, FileMode.Open);

        }
        #endregion
    }
}