using PoolLight.Wpf.Services.Interfaces;

namespace PoolLight.Wpf.Models
{
    /// <summary>
    /// Valeur de la température avec le changement de valeurs.
    /// </summary>
    public class TimestampedValueTemperature : TimestampedValue<float?>
    {
        /// <summary>
        /// Valeur de départ.
        /// </summary>
        private ModeTempEnum _modeTemperature = ModeTempEnum.Fahrenheit;

        /// <summary>
        /// Convertisseur de température.
        /// </summary>
        private readonly IConvertirTemperature _convertirTemperature;

        /// <summary>
        /// Constructeur.
        /// </summary>
        /// <param name="convertirTemperature">Injection du service de conversion.</param>
        public TimestampedValueTemperature(IConvertirTemperature convertirTemperature)
        {
            _convertirTemperature = convertirTemperature;
        }

        /// <summary>
        /// Surcharge pour l'affichage de la donnée ajustée.
        /// </summary>
        public override float? Data
        {
            get => base.Data;
            set => SetProperty(ref _data, value, () => RaisePropertyChanged(nameof(AjustedData)));
        }

        /// <summary>
        /// Valeur ajustée.
        /// </summary>
        public float? AjustedData
        {
            get => ObtenirValeurAjustee(Data);
        }

        /// <summary>
        /// Mode de la température.
        /// </summary>
        public ModeTempEnum ModeTemperature
        {
            get => _modeTemperature;
            set => SetProperty(ref _modeTemperature, value, () => RaisePropertyChanged(nameof(AjustedData)));
        }

        /// <summary>
        /// Éteint la lumière.
        /// </summary>
        /// <returns>Tâche.</returns>
        internal void BasculerTemperature()
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
    }

    /// <summary>
    /// Mode de la température.
    /// </summary>
    public enum ModeTempEnum
    {
        Celcius,

        Fahrenheit
    }

}
