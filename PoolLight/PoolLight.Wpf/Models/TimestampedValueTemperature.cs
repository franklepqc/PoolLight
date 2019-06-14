using PoolLight.Wpf.Services;
using PoolLight.Wpf.Services.Interfaces;
using Prism.Commands;
using System;

namespace PoolLight.Wpf.Models
{
    /// <summary>
    /// Mode de la température.
    /// </summary>
    public enum ModeTempEnum
    {
        Celcius,

        Fahrenheit
    }

    /// <summary>
    /// Valeur de la température avec le changement de valeurs.
    /// </summary>
    public class TimestampedValueTemperature : TimestampedValue<int?>
    {
        #region Fields

        /// <summary>
        /// Convertisseur de température.
        /// </summary>
        private readonly IConvertirTemperature _convertirTemperature;

        /// <summary>
        /// Valeur de départ.
        /// </summary>
        private ModeTempEnum _modeTemperature = ModeTempEnum.Fahrenheit;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Constructeur.
        /// </summary>
        /// <param name="convertirTemperature">Injection du service de conversion.</param>
        public TimestampedValueTemperature(IConvertirTemperature convertirTemperature = null)
        {
            // Injection.
            _convertirTemperature = (convertirTemperature ?? new ConvertirTemperature());

            // Initialisation de la commande.
            CommandeModeTemperature = new DelegateCommand(() => BasculerTemperature(), () => Data.HasValue);
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Valeur ajustée.
        /// </summary>
        public int? AjustedData
        {
            get => ObtenirValeurAjustee(Data);
        }

        /// <summary>
        /// Commande (bouton mode temp).
        /// </summary>
        public DelegateCommandBase CommandeModeTemperature { get; set; }

        /// <summary>
        /// Mode de la température.
        /// </summary>
        public string ModeTemperature
        {
            get
            {
                switch (_modeTemperature)
                {
                    case ModeTempEnum.Fahrenheit:
                        return "°F";

                    case ModeTempEnum.Celcius:
                        return "°C";

                    default:
                        return string.Empty;
                }
            }
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Sur changement de la propriété "Data".
        /// </summary>
        protected override void OnDataPropertyChanged()
        {
            RaisePropertyChanged(nameof(AjustedData));
            CommandeModeTemperature.RaiseCanExecuteChanged();
        }

        /// <summary>
        /// Bascule le mode de la température (°C, °F...).
        /// </summary>
        private void BasculerTemperature()
        {
            // Basculer.
            if (_modeTemperature == ModeTempEnum.Celcius)
            {
                _modeTemperature = ModeTempEnum.Fahrenheit;
            }
            else
            {
                _modeTemperature = ModeTempEnum.Celcius;
            }

            // Affichage.
            RaisePropertyChanged(nameof(ModeTemperature));
            RaisePropertyChanged(nameof(AjustedData));
        }

        /// <summary>
        /// Obtenir la nouvelle donnée.
        /// </summary>
        /// <param name="nouvelleDonnee">Nouvelle donnée.</param>
        /// <returns>Valeur ajustée.</returns>
        private int? ObtenirValeurAjustee(int? nouvelleDonnee)
        {
            if (!nouvelleDonnee.HasValue) return null;

            return Convert.ToInt32(Math.Round(_convertirTemperature.Convertir(_modeTemperature, nouvelleDonnee.Value), 0, MidpointRounding.AwayFromZero));
        }

        #endregion Methods
    }
}