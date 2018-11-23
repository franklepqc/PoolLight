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
        private SolidColorBrush _couleurBouton = new SolidColorBrush(Colors.Black);
        private IClientApi _clientApi;

        public MainPageViewModel()
        {
            CommandeAllumer = new Prism.Commands.DelegateCommand(Basculer);
            //_clientApi = (clientApi ?? new ClientApi());
            _clientApi = new ClientApi();
        }

        public SolidColorBrush CouleurBouton
        {
            get { return _couleurBouton; }
            set
            {
                _couleurBouton = value;
                RaisePropertyChanged();
            }
        }

        public bool EstAllume { get; private set; }

        public ICommand CommandeAllumer { get; set; }

        private async Task Allumer()
        {
            if (await _clientApi.AllumerAsync())
            {
                CouleurBouton = new SolidColorBrush(Colors.Green);
                EstAllume = true;
            }
        }

        private async Task Eteindre()
        {
            if (await _clientApi.EteindreAsync())
            {
                CouleurBouton = new SolidColorBrush(Colors.Black);
                EstAllume = false;
            }
        }

        private async void Basculer()
        {
            if (EstAllume)
            {
                await Eteindre();
            }
            else
            {
                await Allumer();
            }
        }
    }
}
