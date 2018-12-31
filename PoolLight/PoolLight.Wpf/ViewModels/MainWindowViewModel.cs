using PoolLight.Wpf.Clients.Interfaces;
using PoolLight.Wpf.Services.Interfaces;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PoolLight.Wpf.ViewModels
{
    public class MainWindowViewModel : Prism.Mvvm.BindableBase
    {
        #region Fields

        /// <summary>
        /// Injections.
        /// </summary>
        private readonly IClientLumiere _clientLumi;
        private readonly IClientTemperature _clientTemp;
        private readonly IClientPH _clientPh;
        private readonly IConvertirTemperature _convertirTemperature;

        /// <summary>
        /// Champs.
        /// </summary>
        private bool _activiteEnCours = false, 
                     _recupTempCompletee = false, 
                     _recupPhCompletee = false;
        private bool _lumiereAllumee = false;
        private float _temperatureEnCelcius;
        private float _pH;
        private ModeTempEnum _modeTemperature = ModeTempEnum.Fahrenheit;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Constructeur par défaut.
        /// </summary>
        public MainWindowViewModel(IClientLumiere clientLumi, IClientTemperature clientTemp, IClientPH clientPh, IConvertirTemperature convertirTemperature)
        {
            // Commandes.
            CommandeAllumer = new Prism.Commands.DelegateCommand(Basculer, () => !ActiviteEnCours);
            CommandeModeTemperature = new Prism.Commands.DelegateCommand(BasculerTemperature);
            CommandeRafraichir = new Prism.Commands.DelegateCommand(Rafraichir, () => !ActiviteEnCours);

            // Injection.
            _clientLumi = clientLumi;
            _clientTemp = clientTemp;
            _clientPh = clientPh;
            _convertirTemperature = convertirTemperature;

            Rafraichir();
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Activité en cours.
        /// Savoir si la demande est acheminée.
        /// </summary>
        public bool ActiviteEnCours
        {
            get { return _activiteEnCours; }
            set
            {
                _activiteEnCours = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Activité de la récupération de la température en cours.
        /// Savoir si la demande est acheminée.
        /// </summary>
        public bool RecuperationTemperatureCompletee
        {
            get { return _recupTempCompletee; }
            set
            {
                _recupTempCompletee = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Activité de la récupération du pH en cours.
        /// Savoir si la demande est acheminée.
        /// </summary>
        public bool RecuperationPhCompletee
        {
            get { return _recupPhCompletee; }
            set
            {
                _recupPhCompletee = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Commande (bouton).
        /// </summary>
        public ICommand CommandeAllumer { get; set; }

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
        public float Temperature
        {
            get => _convertirTemperature.Convertir(ModeTemperature, _temperatureEnCelcius);
            set
            {
                _temperatureEnCelcius = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// pH.
        /// </summary>
        public float pH
        {
            get => _pH;
            set
            {
                _pH = value;
                RaisePropertyChanged();
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
        /// Indicateur si la lumière est allumée.
        /// </summary>
        public bool LumiereAllumee
        {
            get => _lumiereAllumee;
            set
            {
                _lumiereAllumee = value;
                RaisePropertyChanged();
            }
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Allume la lumière.
        /// </summary>
        /// <returns>Tâche.</returns>
        private async Task Allumer()
        {
            if (await _clientLumi.AllumerAsync())
            {
                LumiereAllumee = true;
            }
        }

        /// <summary>
        /// Bascule (toggle).
        /// </summary>
        /// <returns>Tâche.</returns>
        private async void Basculer()
        {
            ActiviteEnCours = true;

            if (LumiereAllumee)
            {
                await Eteindre();
            }
            else
            {
                await Allumer();
            }

            ActiviteEnCours = false;
        }

        /// <summary>
        /// Éteint la lumière.
        /// </summary>
        /// <returns>Tâche.</returns>
        private async Task Eteindre()
        {
            if (await _clientLumi.EteindreAsync())
            {
                LumiereAllumee = false;
            }
        }

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
            else if (ModeTemperature == ModeTempEnum.Fahrenheit)
            {
                ModeTemperature = ModeTempEnum.Kelvin;
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
            RecupererTemp();
            RecupererPh();
        }

        /// <summary>
        /// Récupération de la température.
        /// </summary>
        private async void RecupererTemp()
        {
            RecuperationTemperatureCompletee = false;
            Temperature = await _clientTemp.Obtenir();
            RecuperationTemperatureCompletee = true;
        }

        /// <summary>
        /// Récupération du pH.
        /// </summary>
        private async void RecupererPh()
        {
            RecuperationPhCompletee = false;
            pH = await _clientPh.Obtenir();
            RecuperationPhCompletee = true;
        }

        #endregion Methods
    }

    /// <summary>
    /// Mode de la température.
    /// </summary>
    public enum ModeTempEnum
    {
        Celcius,

        Fahrenheit,

        Kelvin
    }
}