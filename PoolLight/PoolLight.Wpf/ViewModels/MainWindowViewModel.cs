using PoolLight.Wpf.Clients;
using PoolLight.Wpf.Clients.Interfaces;
using PoolLight.Wpf.Services;
using PoolLight.Wpf.Services.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;

namespace PoolLight.Wpf.ViewModels
{
    public class MainWindowViewModel : Prism.Mvvm.BindableBase
    {
        #region Fields

        /// <summary>
        /// Injections.
        /// </summary>
        private readonly IClientApi _clientApi;
        private readonly IConvertirTemperature _convertirTemperature;

        /// <summary>
        /// Champs.
        /// </summary>
        private bool _activiteEnCours = false;
        private bool _lumiereAllumee = false;
        private SolidColorBrush _couleurBouton = new SolidColorBrush(Colors.Black);
        private float _temperatureEnCelcius = 60f;
        private ModeTempEnum _modeTemperature = ModeTempEnum.Celcius;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Constructeur par défaut.
        /// </summary>
        /// <param name="clientApi">Client d'API.</param>
        public MainWindowViewModel(IClientApi clientApi, IConvertirTemperature convertirTemperature)
        {
            CommandeAllumer = new Prism.Commands.DelegateCommand(Basculer, () => !ActiviteEnCours);
            CommandeModeTemperature = new Prism.Commands.DelegateCommand(BasculerTemperature);
            _clientApi = (clientApi ?? new ClientApi());
            _convertirTemperature = (convertirTemperature ?? new ConvertirTemperature());
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
        /// Commande (bouton).
        /// </summary>
        public ICommand CommandeAllumer { get; set; }

        /// <summary>
        /// Commande (bouton mode temp).
        /// </summary>
        public ICommand CommandeModeTemperature { get; set; }

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
        /// Couleur du bouton.
        /// </summary>
        public SolidColorBrush CouleurBouton
        {
            get { return _couleurBouton; }
            set
            {
                _couleurBouton = value;
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

        #endregion Properties

        #region Methods

        /// <summary>
        /// Allume la lumière.
        /// </summary>
        /// <returns>Tâche.</returns>
        private async Task Allumer()
        {
            if (await _clientApi.AllumerAsync())
            {
                CouleurBouton = new SolidColorBrush(Colors.Green);
                _lumiereAllumee = true;
            }
        }

        /// <summary>
        /// Bascule (toggle).
        /// </summary>
        /// <returns>Tâche.</returns>
        private async void Basculer()
        {
            ActiviteEnCours = true;

            if (_lumiereAllumee)
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
            if (await _clientApi.EteindreAsync())
            {
                CouleurBouton = new SolidColorBrush(Colors.Black);
                _lumiereAllumee = false;
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