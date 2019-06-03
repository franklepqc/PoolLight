using PoolLight.Wpf.Clients.Interfaces;
using PoolLight.Wpf.Models;
using Prism.Commands;
using System;
using System.Windows.Input;

namespace PoolLight.Wpf.ViewModels
{
    public class MainWindowViewModel : Prism.Mvvm.BindableBase
    {
        #region Fields

        /// <summary>
        /// Injections.
        /// </summary>
        private readonly IClientInfosEau _clientInfosEau;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Constructeur par défaut.
        /// </summary>
        public MainWindowViewModel(IClientInfosEau clientInfosEau)
        {
            // Commandes.
            CommandeRafraichir = new DelegateCommand(Rafraichir);

            // Injection.
            _clientInfosEau = clientInfosEau;

            // Afficher les données.
            Rafraichir();
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Commande.
        /// </summary>
        public ICommand CommandeRafraichir { get; set; }

        /// <summary>
        /// Température.
        /// </summary>
        public TimestampedValueTemperature Temperature { get; } = new TimestampedValueTemperature();

        #endregion Properties

        #region Methods

        /// <summary>
        /// Rafraichir les valeurs.
        /// </summary>
        private void Rafraichir()
        {
            RecupererInfosEau();
        }

        /// <summary>
        /// Récupération de la température et du pH.
        /// </summary>
        private async void RecupererInfosEau()
        {
            var infos = await _clientInfosEau.Obtenir();

            if (infos != null)
            {
                Temperature.Data = (float)Math.Round(infos.Temperature.Value, 0, MidpointRounding.AwayFromZero);
                Temperature.ReceivedDateTime = DateTime.Now;
            }
        }

        #endregion Methods
    }
}