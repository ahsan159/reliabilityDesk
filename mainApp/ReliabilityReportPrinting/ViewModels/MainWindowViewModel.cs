using Prism.Mvvm;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ReliabilityReportPrinting.ViewModels
{
    internal class MainWindowViewModel : BindableBase
    {
        private string _XmlFilePath;
        public string XmlFilePath
        {
            get { return _XmlFilePath; }
            set { SetProperty(ref _XmlFilePath, value); }
        }

        MainWindowViewModel()
        {
            string[] args  = Environment.GetCommandLineArgs();
            foreach(string arg in args)
            {
                MessageBox.Show(arg);
            }
            _XmlFilePath = "C:/users/ahsan/source/output.html";
        }
    }
}