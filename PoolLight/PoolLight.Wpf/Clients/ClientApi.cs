using System;
using System.Threading.Tasks;

namespace PoolLight.Wpf.Clients
{
    /// <summary>
    /// Implémentation de l'API pour le globe.
    /// </summary>
    public class ClientApi : Interfaces.IClientApi
    {
        /// <summary>
        /// Allumer le globe.
        /// </summary>
        /// <returns>Tâche qui peut être attendue.</returns>
        public Task<bool> AllumerAsync() =>
            Task.Delay(TimeSpan.FromSeconds(3d))
                .ContinueWith(tachePrecedente => true);

        /// <summary>
        /// Éteindre le globe.
        /// </summary>
        /// <returns>Tâche qui peut être attendue.</returns>
        public Task<bool> EteindreAsync() =>
            Task.Delay(TimeSpan.FromSeconds(3d))
                .ContinueWith(tachePrecedente => true);

        /// <summary>
        /// Obtenir la température.
        /// </summary>
        /// <returns></returns>
        public Task<float> ObtenirTemperatureAsync() =>
            Task.FromResult(15.56f);

        /// <summary>
        /// Obtenir le pH.
        /// </summary>
        /// <returns></returns>
        public Task<float> ObtenirpH() =>
            Task.FromResult(7f);
    }
}
