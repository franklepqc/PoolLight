using Microsoft.Extensions.Options;
using PoolLight.Wpf.Clients.Interfaces;
using System.Threading.Tasks;

namespace PoolLight.Wpf.Clients
{
    /// <summary>
    /// Client pour les informations de l'eau.
    /// </summary>
    public class ClientPh : ClientBase, IClientPh
    {
        /// <summary>
        /// Url pour l'API.
        /// </summary>
        private readonly string _url;

        /// <summary>
        /// Constructeur par défaut.
        /// </summary>
        /// <param name="options">Options de configuration.</param>
        public ClientPh(IOptions<UrlConfig> options)
        {
            _url = options.Value.UrlApiPh;
        }

        /// <summary>
        /// Obtention du pH de l'eau.
        /// </summary>
        /// <returns>pH.</returns>
        public override Task<float> Obtenir() =>
            Obtenir(_url);
    }
}
