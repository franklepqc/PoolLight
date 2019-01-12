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
        private readonly IGestionLumiere _clientLumi;
        private readonly IClientInfosEau _clientInfosEau;
        private readonly IConvertirTemperature _convertirTemperature;

        /// <summary>
        /// Champs.
        /// </summary>
        private bool _activiteEnCours = false, 
                     _recupTempCompletee = true, 
                     _recupPhCompletee = true;
        private bool _lumiereAllumee = false;
        private float? _temperatureEnCelcius;
        private float? _pH;
        private System.DateTime? _dateRecuperation;
        private System.DateTime? _dateDernierEnregistrement;
        private ModeTempEnum _modeTemperature = ModeTempEnum.Fahrenheit;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Constructeur par défaut.
        /// </summary>
        public MainWindowViewModel(IGestionLumiere clientLumi, IClientInfosEau clientInfosEau, IConvertirTemperature convertirTemperature)
        {
            // Commandes.
            CommandeAllumer = new Prism.Commands.DelegateCommand(Basculer, () => !ActiviteEnCours);
            CommandeModeTemperature = new Prism.Commands.DelegateCommand(BasculerTemperature);
            CommandeRafraichir = new Prism.Commands.DelegateCommand(Rafraichir, () => !ActiviteEnCours && RecuperationTemperatureCompletee && RecuperationPhCompletee);

            // Injection.
            _clientLumi = clientLumi;
            _clientInfosEau = clientInfosEau;
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

                // Rafraichir l'exécution des commandes.
                (CommandeAllumer as Prism.Commands.DelegateCommand).RaiseCanExecuteChanged();
                (CommandeRafraichir as Prism.Commands.DelegateCommand).RaiseCanExecuteChanged();
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
                
                // Rafraichir l'exécution des commandes.
                (CommandeRafraichir as Prism.Commands.DelegateCommand).RaiseCanExecuteChanged();
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

                // Rafraichir l'exécution des commandes.
                (CommandeRafraichir as Prism.Commands.DelegateCommand).RaiseCanExecuteChanged();
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
            }
        }

        /// <summary>
        /// pH.
        /// </summary>
        public float? pH
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

        /// <summary>
        /// Obtenir les dernières informations concernant les enregistrements.
        /// </summary>
        public string InfosDates
        {
            get => (_dateDernierEnregistrement.HasValue && _dateRecuperation.HasValue ? 
                $"Dernière demande {_dateRecuperation.Value:d}{System.Environment.NewLine}Dernière mise à jour {_dateDernierEnregistrement.Value:d}" : 
                default(string));
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
            RecupererEstAllumee();
            RecupererInfosEau();
        }

        /// <summary>
        /// Récupération si la lumière est allumée.
        /// </summary>
        private void RecupererEstAllumee()
        {
            ActiviteEnCours = true;
            //LumiereAllumee = await _clientLumi.EstAllumeeAsync();
            ActiviteEnCours = false;
        }

        /// <summary>
        /// Récupération de la température et du pH.
        /// </summary>
        private async void RecupererInfosEau()
        {
            _dateRecuperation = System.DateTime.Now;

            var infos = await _clientInfosEau.Obtenir();

            if (infos != null)
            {
                Temperature = infos.Temperature;
                pH = infos.PH;
                _dateDernierEnregistrement = infos.DateDerniereMAJ;

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

        Fahrenheit,

        Kelvin
    }
}