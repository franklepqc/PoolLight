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
        /// Temps d'attente.
        /// </summary>
        private static double TEMPS_ATTENTE = 3d;

        /// <summary>
        /// Allumer le globe.
        /// </summary>
        /// <returns>Tâche qui peut être attendue.</returns>
        public Task<bool> AllumerAsync() =>
            Task.Delay(TimeSpan.FromSeconds(TEMPS_ATTENTE))
            .ContinueWith((tachePrecedente) => true);

        /// <summary>
        /// Éteindre le globe.
        /// </summary>
        /// <returns>Tâche qui peut être attendue.</returns>
        public Task<bool> EteindreAsync() =>
            Task.Delay(TimeSpan.FromSeconds(TEMPS_ATTENTE))
            .ContinueWith((tachePrecedente) => true);
    }
}
