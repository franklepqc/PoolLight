using Windows.UI;
using Windows.UI.Xaml.Media;

namespace PoolLight.App.ViewModels
{
    public class MainPageViewModel
    {
        public SolidColorBrush CouleurBouton { get; set; }

        public bool EstAllume { get; private set; }

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

        public void Basculer()
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
