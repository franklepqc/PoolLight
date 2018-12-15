using PoolLight.Wpf.ViewModels;

namespace PoolLight.Wpf.Services.Interfaces
{
    public interface IConvertirTemperature
    {
        float Convertir(ModeTempEnum mode, float temperatureEnCelcius);
    }
}
