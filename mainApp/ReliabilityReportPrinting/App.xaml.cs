using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Prism.Regions;
using Prism.Modularity;
using Prism.Unity;
using Prism.Ioc;
using Prism.Regions;
using Prism.Modularity;

namespace ReliabilityReportPrinting
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    /// 
    public partial class App : PrismApplication
    {
        public App()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Ngo9BigBOggjHTQxAR8/V1NDaF1cWGhIfEx1RHxQdld5ZFRHallYTnNWUj0eQnxTdEBjXn1dcHVWQWReWEZ3WElfZA==");
        }
        protected override Window CreateShell()
        {
            return ContainerLocator.Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            //throw new NotImplementedException();
        }
    }
}
