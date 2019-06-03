using Microsoft.Extensions.Options;
using PoolLight.Wpf.Clients.Interfaces;
using System.Net.Http;
using System.Threading.Tasks;

namespace PoolLight.Wpf.Clients
{
    /// <summary>
    /// Client pour les informations de l'eau.
    /// </summary>
    public class ClientTemperature : IClientTemperature
    {
        /// <summary>
        /// Url pour l'API.
        /// </summary>
        private readonly string _url;

        /// <summary>
        /// Constructeur par défaut.
        /// </summary>
        /// <param name="options">Options de configuration.</param>
        public ClientTemperature(IOptions<UrlConfig> options)
        {
            _url = options.Value.UrlApiTemperature;
        }

        /// <summary>
        /// Obtention des informations de l'eau.
        /// </summary>
        /// <returns>Infos.</returns>
        public Task<float> Obtenir() => (new HttpClient())
            .GetAsync(_url)
            .ContinueWith(reponse => (reponse.IsCompletedSuccessfully ? reponse.Result.Content.ReadAsAsync<float>() : Task.FromResult<float>(default)))
            .ContinueWith(reponse => reponse.Result.Result);
    }
}
