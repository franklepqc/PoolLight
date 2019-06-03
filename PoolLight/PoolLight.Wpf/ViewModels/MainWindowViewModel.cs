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
        private readonly IClientTemperature _clientTemperature;
        private readonly IClientPh _clientPh;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Constructeur par défaut.
        /// </summary>
        /// <param name="clientTemperature">Client pour la température.</param>
        /// <param name="clientPh">Client pour le pH.</param>
        public MainWindowViewModel(IClientTemperature clientTemperature, IClientPh clientPh)
        {
            // Commandes.
            CommandeRafraichir = new DelegateCommand(Rafraichir);

            // Injections.
            _clientTemperature = clientTemperature;
            _clientPh = clientPh;

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

        /// <summary>
        /// pH.
        /// </summary>
        public TimestampedValue<float?> Ph { get; } = new TimestampedValue<float?>();

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
        private void RecupererInfosEau()
        {
            ObtenirSelonClient(Temperature, _clientTemperature);
            ObtenirSelonClient(Ph, _clientPh);
        }

        /// <summary>
        /// Récupération de la température et du pH.
        /// </summary>
        private async void ObtenirSelonClient(TimestampedValue<float?> propriete, IClientBase<float> client)
        {
            propriete.Data = (float)Math.Round(await client.Obtenir(), 0, MidpointRounding.AwayFromZero);
            propriete.ReceivedDateTime = DateTime.Now;
        }

        #endregion Methods
    }
}