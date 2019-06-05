using Microsoft.Extensions.Options;
using PoolLight.Wpf.Clients;
using PoolLight.Wpf.Clients.Interfaces;
using PoolLight.Wpf.Services;
using PoolLight.Wpf.Services.Interfaces;
using PoolLight.Wpf.Views;
using Prism.Ioc;
using Prism.Unity;
using System.Configuration;
using System.Globalization;
using System.Windows;
using System.Windows.Markup;

namespace PoolLight.Wpf
{
    /// <summary>
    /// Logique d'interaction pour App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        /// <summary>
        /// Sur démarrage.
        /// </summary>
        /// <param name="e">Arguments d'évènement de démarrage.</param>
        protected override void OnStartup(StartupEventArgs e)
        {
            // S'assurer que l'application suit les paramètres
            // locaux des préférences linguistiques.
            FrameworkElement.LanguageProperty.OverrideMetadata(
                typeof(FrameworkElement),
                new FrameworkPropertyMetadata(
                    XmlLanguage.GetLanguage(
                        CultureInfo.CurrentCulture.IetfLanguageTag)));

            base.OnStartup(e);
        }

        /// <summary>
        /// Configuration de l'injection de dépendances.
        /// </summary>
        /// <param name="containerRegistry">Régistraire.</param>
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

        /// <summary>
        /// Création du point d'appui de l'application (élément Window).
        /// </summary>
        /// <returns>Fenêtre de l'application.</returns>
        protected override Window CreateShell() => Container.Resolve<MainWindow>();
    }
}
