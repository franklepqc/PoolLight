using PoolLight.App.Clients;
using PoolLight.App.Clients.Interfaces;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI;
using Windows.UI.Xaml.Media;

namespace PoolLight.App.ViewModels
{
    public class MainPageViewModel : Prism.Mvvm.BindableBase
    {
        #region Fields

        /// <summary>
        /// Champs.
        /// </summary>
        private bool _activiteEnCours = false;
        private bool _lumiereAllumee = false;
        private IClientApi _clientApi;
        private SolidColorBrush _couleurBouton = new SolidColorBrush(Colors.Black);

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Constructeur par défaut.
        /// </summary>
        /// <param name="clientApi">Client d'API.</param>
        public MainPageViewModel(IClientApi clientApi)
        {
            CommandeAllumer = new Prism.Commands.DelegateCommand(Basculer, () => !ActiviteEnCours);
            _clientApi = (clientApi ?? new ClientApi());
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

        #endregion Methods
    }
}