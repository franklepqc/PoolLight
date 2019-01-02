using PoolLight.Wpf.Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace PoolLight.Wpf.Services
{
    public class RecupInfosIoTHub : IRecuperationTemperature, IRecuperationPh
    {
        /// <summary>
        /// Obtenir les informations pour la température.
        /// </summary>
        /// <returns></returns>
        Task<float> IRecuperationTemperature.Obtenir() =>
            Task.Delay(TimeSpan.FromSeconds(3d))
                .ContinueWith(tachePrecedente => 21.11111111111111f);

        /// <summary>
        /// Obtenir les informations pour le pH.
        /// </summary>
        /// <returns></returns>
        Task<float> IRecuperationPh.Obtenir() =>
            Task.Delay(TimeSpan.FromSeconds(5d))
                .ContinueWith(tachePrecedente => 7f);
    }
}
