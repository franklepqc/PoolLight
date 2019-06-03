using System.Configuration;
using System.Windows;
using Microsoft.Extensions.Options;
using PoolLight.Wpf.Clients;
using PoolLight.Wpf.Clients.Interfaces;
using PoolLight.Wpf.Services;
using PoolLight.Wpf.Services.Interfaces;
using PoolLight.Wpf.Views;
using Prism.Ioc;
using Prism.Unity;

namespace PoolLight.Wpf
{
    /// <summary>
    /// Logique d'interaction pour App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IClientTemperature, ClientTemperature>();
            containerRegistry.RegisterSingleton<IClientPh, ClientPh>();
            containerRegistry.Register<IConvertirTemperature, ConvertirTemperature>();

            containerRegistry.RegisterInstance(Options.Create(new UrlConfig
            {
                UrlApiTemperature = ConfigurationManager.AppSettings["UrlApiTemperature"],
                UrlApiPh = ConfigurationManager.AppSettings["UrlApiPh"]
            }));
        }

        protected override Window CreateShell() => Container.Resolve<MainWindow>();
    }
}
