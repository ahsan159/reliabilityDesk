using mainProjectTree.Models;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace mainProjectTree.ViewModels
{
    class mainProjectTreeViewModel : BindableBase
    {
        private Models.mainProjectTreeModel _treeNodeModel;
        public ICommand selectionCommand { get; private set; }
        public Models.mainProjectTreeModel treeNodeModel
        {
            set
            {
                SetProperty(ref _treeNodeModel, value);
            }
            get
            {
                return _treeNodeModel;
            }
        }
    }
}