using System;
using System.Threading.Tasks;

namespace PoolLight.App.Clients
{
    public class ClientApi : Interfaces.IClientApi
    {
        private static double TEMPS_ATTENTE = 3d;

        public Task<bool> AllumerAsync() =>
            Task.Delay(TimeSpan.FromSeconds(TEMPS_ATTENTE))
            .ContinueWith((tachePrecedente) => true);

        public Task<bool> EteindreAsync() =>
            Task.Delay(TimeSpan.FromSeconds(TEMPS_ATTENTE))
            .ContinueWith((tachePrecedente) => true);

    }
}
