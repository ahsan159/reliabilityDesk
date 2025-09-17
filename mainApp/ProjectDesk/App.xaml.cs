using Prism.Ioc;
using Prism.Unity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace ProjectDesk
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
            return ContainerLocator.Container.Resolve<DeskView>();
            //throw new NotImplementedException();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            //throw new NotImplementedException();
        }
    }
}
