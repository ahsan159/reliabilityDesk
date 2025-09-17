using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectDesk.ViewModels
{
    internal class DeskViewModel : BindableBase
    {
        #region members
        private bool _DiagramExpand = false;
        public bool DiagramExpand
        {
            get => _DiagramExpand;
            set => SetProperty(ref _DiagramExpand, value);
        }
        private bool _ProjectExpand = false;
        public bool ProjectExpand
        {
            get => _ProjectExpand;
            set => SetProperty(ref _ProjectExpand, value);
        }

        private bool _ConfigurationExpand = false;
        public bool ConfigurationExpand
        {
            get => _ConfigurationExpand;
            set => SetProperty(ref _ConfigurationExpand, value);
        }

        #endregion

        #region constructor
        /// <summary>
        /// only constructor
        /// </summary>
        public DeskViewModel()
        {
            _DiagramExpand = false;
            _ProjectExpand = false;
            _ConfigurationExpand = false;
        }
        #endregion
    }
}
