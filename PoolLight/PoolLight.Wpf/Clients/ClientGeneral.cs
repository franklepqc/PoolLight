using System;
using System.Threading.Tasks;
using PoolLight.Wpf.Clients.Interfaces;

namespace PoolLight.Wpf.Clients
{
    /// <summary>
    /// Implémentation de l'API pour le globe.
    /// </summary>
    public class ClientGeneral : IClientTemperature, IClientPH, IClientLumiere
    {
        /// <summary>
        /// Allumer le globe.
        /// </summary>
        /// <returns>Tâche qui peut être attendue.</returns>
        public Task<bool> AllumerAsync() =>
            Task.Delay(TimeSpan.FromSeconds(3d))
                .ContinueWith(tachePrecedente => true);

        /// <summary>
        /// À savoir si la lumière est allumée ou éteinte.
        /// </summary>
        /// <returns>Vrai si elle est allumée, faux sinon.</returns>
        public Task<bool> EstAllumeeAsync() =>
            Task.Delay(TimeSpan.FromSeconds(5d))
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
        Task<float> IClientTemperature.Obtenir() =>
            Task.Delay(TimeSpan.FromSeconds(10d))
                .ContinueWith(tachePrecedente => 21.11111111111111f);

        /// <summary>
        /// Obtenir le pH.
        /// </summary>
        /// <returns></returns>
        Task<float> IClientPH.Obtenir() => 
            Task.Delay(TimeSpan.FromSeconds(1d))
                .ContinueWith(tachePrecedente => 7f);
    }
}
