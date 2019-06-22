using Prism.Mvvm;
using System;

namespace PoolLight.Wpf.Models
{
    /// <summary>
    /// Représente une valeur obtenue lors de la dernière mise à jour.
    /// </summary>
    /// <typeparam name="T">Type de la donnée conservée.</typeparam>
    public class TimestampedValue<T> : BindableBase
    {
        /// <summary>
        /// Champ date.
        /// </summary>
        private DateTime? _receivedDateTime;

        /// <summary>
        /// Donnée.
        /// </summary>
        private T _data;

        /// <summary>
        /// Afficher un message pour l'erreur de connectivité.
        /// </summary>
        private bool _afficherMessageConnectivite = false;

        /// <summary>
        /// Donnée.
        /// </summary>
        public T Data
        {
            get => _data;
            set => SetProperty(ref _data, value, OnDataPropertyChanged);
        }

        /// <summary>
        /// Date / heure réception.
        /// </summary>
        public DateTime? ReceivedDateTime
        {
            get => _receivedDateTime;
            set => SetProperty(ref _receivedDateTime, value);
        }

        /// <summary>
        /// Afficher un message ou non pour la connectivité.
        /// </summary>
        public bool AfficherMessageConnectivite
        {
            get => _afficherMessageConnectivite;
            set => SetProperty(ref _afficherMessageConnectivite, value);
        }

        /// <summary>
        /// Sur changement de la propriété.
        /// </summary>
        protected virtual void OnDataPropertyChanged()
        {
            // Rien à faire.
        }
    }
}
