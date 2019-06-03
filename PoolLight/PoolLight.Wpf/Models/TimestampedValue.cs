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
        protected DateTime? _receivedDateTime;

        /// <summary>
        /// Donnée.
        /// </summary>
        protected T _data;

        /// <summary>
        /// Donnée.
        /// </summary>
        public virtual T Data
        {
            get => _data;
            set => SetProperty(ref _data, value);
        }

        /// <summary>
        /// Date / heure réception.
        /// </summary>
        public virtual DateTime? ReceivedDateTime
        {
            get => _receivedDateTime;
            set => SetProperty(ref _receivedDateTime, value);
        }
    }
}
