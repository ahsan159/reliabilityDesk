using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Xsl;

namespace mainApp.ViewModels
{
    class ToolbarViewModel : BindableBase
    {
        #region member functions
        private string ActiveFileName = "";
        private string ActivePartListName = "";
        private static IEventAggregator _ea;
        public DelegateCommand openProjectCommand { get; }
        public DelegateCommand SaveDiagramCommand { get; set; }

        public DelegateCommand SolveProjectTreeCommand { get; set; }
        public DelegateCommand SaveAsDiagramCommand { get; set; }
        public DelegateCommand PrintProjectCommand { get; set; }
        public DelegateCommand SetActivePartListCommand { get; set; }
        public DelegateCommand ViewActivePartListCommand { get; set; }
        public DelegateCommand RefreshTreeCommand { get; set; }
        public DelegateCommand SolveDiagramCommand { get; set; }
        #endregion

        #region consturctor 
        public ToolbarViewModel(IEventAggregator ea)
        {
            openProjectCommand = new DelegateCommand(OpenProject);
            SaveDiagramCommand = new DelegateCommand(SaveProject);
            SolveProjectTreeCommand = new DelegateCommand(SolveProjectTree);
            SaveAsDiagramCommand = new DelegateCommand(SaveAsProject);
            PrintProjectCommand = new DelegateCommand(PrintProject);
            SetActivePartListCommand = new DelegateCommand(setActivePartList);
            ViewActivePartListCommand = new DelegateCommand(ViewActivePartList);
            RefreshTreeCommand = new DelegateCommand(RefreshTree);
            SolveDiagramCommand = new DelegateCommand(SolveDiagram);
            _ea = ea;
            ActiveFileName = "projectID3.xml";
            if (File.Exists(ActiveFileName))
            {
                _ea.GetEvent<OpenProjectFileEvent>().Publish(ActiveFileName);
            }
            //_ea.GetEvent<OpenProjectFileEvent>().Publish("openFile");
        }

        #endregion

        #region command implementations


        private void SolveDiagram()
        {
            _ea.GetEvent<SolveDiagramEvent>().Publish(87600);

            //throw new NotImplementedException();
        }

        private void RefreshTree()
        {
            _ea.GetEvent<RefreshTreeEvent>().Publish(ActiveFileName);
        }

        /// <summary>
        /// This function will publish a reliability calculation event
        /// </summary>
        private void SolveProjectTree()
        {
            // open window asking for the calculation point for project 
            // value added for testing purposes only
            _ea.GetEvent<ReliabilityTreeCalculationEvent>().Publish(87600);
        }

        private void OpenProject()
        {
            //MessageBox.Show("This is Open project");
            Microsoft.Win32.OpenFileDialog openFileDlg = new Microsoft.Win32.OpenFileDialog();
            openFileDlg.Filter = "Project File (*.prj)|*.prj|XML File (*.xml)|*.xml|All Files (*.*)|*.*";
            openFileDlg.FilterIndex = 2;
            openFileDlg.RestoreDirectory = true;
            openFileDlg.Multiselect = false;
            openFileDlg.InitialDirectory = System.IO.Directory.GetCurrentDirectory();
            bool result = (bool)openFileDlg.ShowDialog();
            if (result)
            {
                ActiveFileName = openFileDlg.FileName;
                _ea.GetEvent<OpenProjectFileEvent>().Publish(openFileDlg.FileName);
                //_ea.GetEvent<OpenProjectFileEvent>().Publish(ActiveFileName);
            }
        }

        private void SaveAsProject()
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
                _ea.GetEvent<SaveProjectFileEvent>().Publish(saveFileDlg.FileName);
            }
        }

        private void SaveProject()
        {
            //Microsoft.Win32.SaveFileDialog saveFileDlg = new Microsoft.Win32.SaveFileDialog();
            //saveFileDlg.Filter = "Project File (*.prj)|*.prj|XML File (*.xml)|*.xml|All Files (*.*)|*.*";
            //saveFileDlg.FilterIndex = 2;
            //saveFileDlg.RestoreDirectory = true;
            ////saveFileDlg.Multiselect = false;
            //saveFileDlg.InitialDirectory = System.IO.Directory.GetCurrentDirectory();
            //bool result = (bool)saveFileDlg.ShowDialog();
            //if (result)
            //{
            //    _ea.GetEvent<SaveProjectFileEvent>().Publish(ActiveFileName);
            //}
            _ea.GetEvent<SaveProjectFileEvent>().Publish(ActiveFileName);
        }

        /// <summary>
        /// function to call for opening report viewer program
        /// </summary>
        private void PrintProject()
        {
            string TransformationFile = "C:\\Users\\muhammadahsan\\source\\repos\\reliabilityDesk\\mainApp\\mainApp\\XSLTransformation\\ReportTransformation.xslt";
            //string outputFile = "output.html";
            string outputFile = "C:\\Users\\muhammadahsan\\source\\repos\\reliabilityDesk\\mainApp\\mainApp\\bin\\Debug\\net6.0-windows\\output.html";
            string xmlFile = ActiveFileName;
            XslCompiledTransform xslt = new XslCompiledTransform();
            xslt.Load(TransformationFile);
            xslt.Transform(ActiveFileName, outputFile);
            ProcessStartInfo StartInfo = new ProcessStartInfo();
            StartInfo.FileName = "C:\\Users\\muhammadahsan\\source\\repos\\reliabilityDesk\\mainApp\\ReliabilityReportPrinting\\bin\\Debug\\net6.0-windows\\ReliabilityReportPrinting.exe";
            StartInfo.ArgumentList.Add(outputFile);
            StartInfo.CreateNoWindow = true;
            Process proc = new Process();
            proc.StartInfo = StartInfo;
            proc.Start();

        }

        private void setActivePartList()
        {

            Microsoft.Win32.OpenFileDialog openFileDlg = new Microsoft.Win32.OpenFileDialog();
            openFileDlg.Filter = "Project File (*.prj)|*.prj|XML File (*.xml)|*.xml|All Files (*.*)|*.*";
            openFileDlg.FilterIndex = 2;
            openFileDlg.RestoreDirectory = true;
            openFileDlg.Multiselect = false;
            openFileDlg.InitialDirectory = System.IO.Directory.GetCurrentDirectory();
            bool result = (bool)openFileDlg.ShowDialog();
            if (result)
            {
                ActivePartListName = openFileDlg.FileName;
                _ea.GetEvent<SetActivePartListEvent>().Publish(ActivePartListName);

            }
        }

        private void ViewActivePartList()
        {
            Process proc = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = "C:\\Users\\muhammadahsan\\source\\repos\\reliabilityDesk\\mainApp\\PartListSelector\\bin\\Debug\\net6.0-windows\\PartListSelector.exe";
            startInfo.ArgumentList.Add(ActivePartListName);
            proc.StartInfo = startInfo;
            proc.Start();
        }

        #endregion
    }
}
