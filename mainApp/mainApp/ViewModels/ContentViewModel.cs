using mainApp.Template;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace mainApp.ViewModels
{
    internal class ContentViewModel:BindableBase
    {
        public DelegateCommand Command { get; set; }

        private ObservableCollection<ReliabilityEntity> _Collection;

        public ContentViewModel()
        {
            _Collection = new ObservableCollection<ReliabilityEntity>();            
        }
       
    }
}
