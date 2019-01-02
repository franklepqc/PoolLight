using System;
using System.Threading.Tasks;
using PoolLight.Wpf.Clients.Interfaces;

namespace PoolLight.Wpf.Clients
{
    /// <summary>
    /// Implémentation de l'API pour le globe.
    /// </summary>
    public class ClientGeneral : IGestionLumiere
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
    }
}
