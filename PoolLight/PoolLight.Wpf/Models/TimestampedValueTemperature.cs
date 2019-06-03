using PoolLight.Wpf.Services;
using PoolLight.Wpf.Services.Interfaces;
using Prism.Commands;

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
    public class TimestampedValueTemperature : TimestampedValue<float?>
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
        public float? AjustedData
        {
            get => ObtenirValeurAjustee(Data);
        }

        /// <summary>
        /// Commande (bouton mode temp).
        /// </summary>
        public DelegateCommandBase CommandeModeTemperature { get; set; }

        /// <summary>
        /// Surcharge pour l'affichage de la donnée ajustée.
        /// </summary>
        public override float? Data
        {
            get => base.Data;
            set => SetProperty(ref _data, value, () =>
            {
                RaisePropertyChanged(nameof(AjustedData));
                CommandeModeTemperature.RaiseCanExecuteChanged();
            });
        }

        /// <summary>
        /// Mode de la température.
        /// </summary>
        public ModeTempEnum ModeTemperature
        {
            get => _modeTemperature;
            set => SetProperty(ref _modeTemperature, value, () => RaisePropertyChanged(nameof(AjustedData)));
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Bascule le mode de la température (°C, °F...).
        /// </summary>
        private void BasculerTemperature()
        {
            if (ModeTemperature == ModeTempEnum.Celcius)
            {
                ModeTemperature = ModeTempEnum.Fahrenheit;
            }
            else
            {
                ModeTemperature = ModeTempEnum.Celcius;
            }
        }

        /// <summary>
        /// Obtenir la nouvelle donnée.
        /// </summary>
        /// <param name="nouvelleDonnee">Nouvelle donnée.</param>
        /// <returns>Valeur ajustée.</returns>
        private float? ObtenirValeurAjustee(float? nouvelleDonnee)
        {
            if (!nouvelleDonnee.HasValue) return null;

            return _convertirTemperature.Convertir(_modeTemperature, nouvelleDonnee.Value);
        }

        #endregion Methods
    }
}