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
            _ea = ea;
            //_ea.GetEvent<OpenProjectFileEvent>().Publish("openFile");
        }
        public DelegateCommand openProjectCommand { get; }
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

    }
}
