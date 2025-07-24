using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Mvvm;

namespace mainApp.ViewModels
{
    public class PrismShellViewModel:BindableBase
    {
        private string _title = "Welcome to MVVM using Prism";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }
    }
}
