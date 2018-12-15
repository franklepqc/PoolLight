using PoolLight.Wpf.ViewModels;

namespace PoolLight.Wpf.Services
{
    public class ConvertirTemperature : Interfaces.IConvertirTemperature
    {
        public float Convertir(ModeTempEnum mode, float temperatureEnCelcius)
        {
            if (mode == ModeTempEnum.Celcius)
                return temperatureEnCelcius;
            else
                return temperatureEnCelcius + 273.15f;
        }
    }
}
