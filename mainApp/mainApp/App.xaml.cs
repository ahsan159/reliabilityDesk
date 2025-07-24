using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Prism.Unity;
using Prism.Ioc;
using mainApp.Views;
using Prism.Regions;
using Prism.Modularity;

namespace mainApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        public App()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Ngo9BigBOggjHTQxAR8/V1NDaF1cWGhIfEx1RHxQdld5ZFRHallYTnNWUj0eQnxTdEBjXn1dcHVWQWReWEZ3WElfZA==");
        }

        protected override Window CreateShell()
        {
            return ContainerLocator.Container.Resolve<PrismShell>();
        }
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        { }
        protected override void OnInitialized()
        {
            base.OnInitialized();
            var regionManger = ContainerLocator.Container.Resolve<RegionManager>();
            regionManger.RegisterViewWithRegion<ToolbarView>("ToolbarRegion");
            regionManger.RegisterViewWithRegion<SidebarView>("SidebarRegion");
            regionManger.RegisterViewWithRegion<ContentView>("MainRegion");            
        }
        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            base.ConfigureModuleCatalog(moduleCatalog);
            //moduleCatalog.AddModule(typeof(mainProjectTree.mainProjectTree));
        }
    }


}
