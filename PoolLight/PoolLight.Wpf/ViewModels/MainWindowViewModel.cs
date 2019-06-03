using PoolLight.Wpf.Clients.Interfaces;
using PoolLight.Wpf.Services.Interfaces;
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
        private readonly IConvertirTemperature _convertirTemperature;

        /// <summary>
        /// Champs.
        /// </summary>
        private float? _temperatureEnCelcius = new float?();
        private System.DateTime? _dateRecuperation;
        private ModeTempEnum _modeTemperature = ModeTempEnum.Fahrenheit;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Constructeur par défaut.
        /// </summary>
        public MainWindowViewModel(IClientInfosEau clientInfosEau, IConvertirTemperature convertirTemperature)
        {
            // Commandes.
            CommandeModeTemperature = new Prism.Commands.DelegateCommand(BasculerTemperature, () => Temperature.HasValue);
            CommandeRafraichir = new Prism.Commands.DelegateCommand(Rafraichir);

            // Injection.
            _clientInfosEau = clientInfosEau;
            _convertirTemperature = convertirTemperature;

            Rafraichir();
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Commande (bouton mode temp).
        /// </summary>
        public ICommand CommandeModeTemperature { get; set; }

        /// <summary>
        /// Commande.
        /// </summary>
        public ICommand CommandeRafraichir { get; set; }

        /// <summary>
        /// Température.
        /// </summary>
        public float? Temperature
        {
            get
            {
                if (_temperatureEnCelcius.HasValue)
                {
                    return _convertirTemperature.Convertir(ModeTemperature, _temperatureEnCelcius.Value);
                }

                return default(float?);
            }
            set
            {
                _temperatureEnCelcius = value;
                RaisePropertyChanged();
                (CommandeModeTemperature as Prism.Commands.DelegateCommand).RaiseCanExecuteChanged();
            }
        }

        /// <summary>
        /// Mode de temperature.
        /// </summary>
        public ModeTempEnum ModeTemperature
        {
            get => _modeTemperature;
            set
            {
                _modeTemperature = value;
                RaisePropertyChanged();
                RaisePropertyChanged(nameof(Temperature));
                RaisePropertyChanged(nameof(ModeTemperatureStr));
            }
        }

        /// <summary>
        /// Mode de temperature.
        /// </summary>
        public string ModeTemperatureStr
        {
            get => (ModeTemperature == ModeTempEnum.Celcius ? "°C" : (ModeTemperature == ModeTempEnum.Fahrenheit ? "°F" : "K"));
        }

        /// <summary>
        /// Obtenir les dernières informations concernant les enregistrements.
        /// </summary>
        public string InfosDates
        {
            get => (_dateRecuperation.HasValue ? 
                $"Dernière mise à jour : {_dateRecuperation.Value.ToLocalTime()}" : 
                string.Empty);
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Éteint la lumière.
        /// </summary>
        /// <returns>Tâche.</returns>
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
                Temperature = infos.Temperature;
                //pH = infos.PH;

                _dateRecuperation = System.DateTime.Now;
                RaisePropertyChanged(nameof(InfosDates));
            }
        }

        #endregion Methods
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