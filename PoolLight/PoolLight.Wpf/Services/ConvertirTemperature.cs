using PoolLight.Wpf.Models;
using System;

namespace PoolLight.Wpf.Services
{
    /// <summary>
    /// Classe qui permet de faire la conversion de la temperature.
    /// </summary>
    public class ConvertirTemperature : Interfaces.IConvertirTemperature
    {
        /// <summary>
        /// Convertir la température (en celcius), en unité voulue.
        /// </summary>
        /// <param name="mode">Unité voulue.</param>
        /// <param name="temperatureEnCelcius">Température en Celcius.</param>
        /// <returns>Température à la nouvelle unité.</returns>
        public float Convertir(ModeTempEnum mode, float temperatureEnCelcius) =>
            mode switch
            {
                ModeTempEnum.Celcius => temperatureEnCelcius,
                ModeTempEnum.Fahrenheit => (temperatureEnCelcius * 1.8f) + 32f,
                _ => throw new NotImplementedException()
            };
    }
}
