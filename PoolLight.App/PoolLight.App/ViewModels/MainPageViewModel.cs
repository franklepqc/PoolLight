using System.Windows.Input;
using Windows.UI;
using Windows.UI.Xaml.Media;

namespace PoolLight.App.ViewModels
{
    public class MainPageViewModel : Prism.Mvvm.BindableBase
    {
        private SolidColorBrush _couleurBouton = new SolidColorBrush(Colors.Black);

        public MainPageViewModel()
        {
            CommandeAllumer = new Prism.Commands.DelegateCommand(Basculer);
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

        private void Allumer()
        {
            CouleurBouton = new SolidColorBrush(Colors.Green);
            EstAllume = true;
        }

        private void Eteindre()
        {
            CouleurBouton = new SolidColorBrush(Colors.Black);
            EstAllume = false;
        }

        private void Basculer()
        {
            if (EstAllume)
            {
                Eteindre();
            }
            else
            {
                Allumer();
            }
        }
    }
}
