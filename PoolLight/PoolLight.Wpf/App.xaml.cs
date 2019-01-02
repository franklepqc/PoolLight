using System.Windows;
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
            containerRegistry.RegisterSingleton<IRecuperationTemperature, RecupInfosIoTHub>();
            containerRegistry.RegisterSingleton<IRecuperationPh, RecupInfosIoTHub>();
            containerRegistry.Register<IGestionLumiere, ClientGeneral>();
            containerRegistry.Register<IClientInfosEau, ClientInfosEau>();
            containerRegistry.Register<IConvertirTemperature, ConvertirTemperature>();
        }

        protected override Window CreateShell() => Container.Resolve<MainWindow>();
    }
}
