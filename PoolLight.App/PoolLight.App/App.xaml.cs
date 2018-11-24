using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using PoolLight.App.Clients;
using PoolLight.App.Clients.Interfaces;
using Windows.ApplicationModel.Activation;

namespace PoolLight.App
{
    /// <summary>
    /// Fournit un comportement spécifique à l'application afin de compléter la classe Application par défaut.
    /// </summary>
    sealed partial class App : Prism.Unity.Windows.PrismUnityApplication
    {
        /// <summary>
        /// Initialise l'objet d'application de singleton.  Il s'agit de la première ligne du code créé
        /// à être exécutée. Elle correspond donc à l'équivalent logique de main() ou WinMain().
        /// </summary>
        public App()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Sur chargement de l'application.
        /// </summary>
        /// <param name="args">Arguments.</param>
        /// <returns>Tâche.</returns>
        protected override Task OnLaunchApplicationAsync(LaunchActivatedEventArgs args)
        {
            NavigationService.Navigate("Main", null);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Sur initialisation.
        /// </summary>
        /// <param name="args">Arguments.</param>
        /// <returns>Tâche.</returns>
        protected override Task OnInitializeAsync(IActivatedEventArgs args)
        {
            // Injection.
            Container.RegisterType<IClientApi, ClientApi>();

            return base.OnInitializeAsync(args);
        }
    }
}
