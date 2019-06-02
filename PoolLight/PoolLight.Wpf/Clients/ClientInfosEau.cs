using PoolLight.Wpf.Clients.Interfaces;
using System.Net.Http;
using System.Threading.Tasks;

namespace PoolLight.Wpf.Clients
{
    /// <summary>
    /// Client pour les informations de l'eau.
    /// </summary>
    public class ClientInfosEau : IClientInfosEau
    {
        /// <summary>
        /// Obtention des informations de l'eau.
        /// </summary>
        /// <returns>Infos.</returns>
        public Task<IInfosEau> Obtenir() => (new HttpClient())
            .GetAsync("http://minwinpc:5000/api/eau")
            .ContinueWith(reponse => reponse.Result.Content.ReadAsAsync<InfosEau>())
            .ContinueWith<IInfosEau>(reponse => reponse.Result.Result);
    }
}
