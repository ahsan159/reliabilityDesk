using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace mainProjectTree.Models
{
    class mainProjectTreeModel:BindableBase
    {
        private TreeView _treeNode;
        public TreeView treeNode
        {
            set
            {
                SetProperty(ref _treeNode, value);
            }
            get
            {
                return _treeNode;
            }
        }
    }
}
