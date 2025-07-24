using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Xml;

namespace mainApp.Models
{
    public class SidebarModel:BindableBase
    {
        private TreeView _treeView;
        public TreeView treeView
        {
            set { SetProperty(ref _treeView, value); }
            get { return _treeView; }
        }
        public SidebarModel()
        {
            _treeView = new TreeView();
        }
    }
}
