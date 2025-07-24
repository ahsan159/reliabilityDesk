using mainProjectTree.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mainProjectTree
{
    public class mainProjectTree:IModule
    {
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            //throw new NotImplementedException();
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
            //throw new NotImplementedException();
            var regionManager = containerProvider.Resolve<IRegionManager>();
            regionManager.RegisterViewWithRegion("SidebarRegion", typeof(mainProjectTreeView));
        }

    }
}
