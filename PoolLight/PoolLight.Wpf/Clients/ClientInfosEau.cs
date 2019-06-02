using Microsoft.Extensions.Options;
using PoolLight.Wpf.Clients.Interfaces;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace PoolLight.Wpf.Clients
{
    /// <summary>
    /// Client pour les informations de l'eau.
    /// </summary>
    public class ClientInfosEau : IClientInfosEau
    {
        /// <summary>
        /// Contenant pour l'injection.
        /// </summary>
        private readonly UrlConfig _urlConfig;

        /// <summary>
        /// Constructeur par défaut.
        /// </summary>
        /// <param name="options">Options de configuration.</param>
        public ClientInfosEau(IOptions<UrlConfig> options)
        {
            _urlConfig = options.Value;
        }

        /// <summary>
        /// Obtention des informations de l'eau.
        /// </summary>
        /// <returns>Infos.</returns>
        public Task<IInfosEau> Obtenir() => (new HttpClient())
            .GetAsync(_urlConfig.UrlApi)
            .ContinueWith(reponse => (reponse.IsCompletedSuccessfully ? reponse.Result.Content.ReadAsAsync<InfosEau>() : Task.FromResult<InfosEau>(default)))
            .ContinueWith<IInfosEau>(reponse => reponse?.Result?.Result);
    }
}
