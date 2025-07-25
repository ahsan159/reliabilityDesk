using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;


namespace mainApp.ViewModels
{
    class ToolbarViewModel : BindableBase
    {
        private static IEventAggregator _ea;
        public ToolbarViewModel(IEventAggregator ea)
        {
            openProjectCommand = new DelegateCommand(OpenProject);
            SaveDiagramCommand = new DelegateCommand(SaveProject);
            _ea = ea;
            //_ea.GetEvent<OpenProjectFileEvent>().Publish("openFile");
        }
        public DelegateCommand openProjectCommand { get; }
        public DelegateCommand SaveDiagramCommand { get; set; }
        private static void OpenProject()
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
                _ea.GetEvent<OpenProjectFileEvent>().Publish(openFileDlg.FileName);
            }

        }

        private void SaveProject()
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
                _ea.GetEvent<SaveDiagramFileEvent>().Publish(saveFileDlg.FileName);
            }
        }
    }
}
