using PoolLight.Wpf.Models;
using System;

namespace PoolLight.Wpf.Services
{
    public class ConvertirTemperature : Interfaces.IConvertirTemperature
    {
        public float Convertir(ModeTempEnum mode, float temperatureEnCelcius)
        {
            switch (mode)
            {
                case ModeTempEnum.Celcius:
                    return temperatureEnCelcius;

                case ModeTempEnum.Fahrenheit:
                    return (temperatureEnCelcius * 1.8f) + 32f;

                default:
                    throw new NotImplementedException();
            }
        }
    }
}
