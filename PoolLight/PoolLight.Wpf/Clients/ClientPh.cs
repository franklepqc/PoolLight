using Microsoft.Extensions.Options;
using PoolLight.Wpf.Clients.Interfaces;

namespace PoolLight.Wpf.Clients
{
    /// <summary>
    /// Client pour les informations de l'eau.
    /// </summary>
    public class ClientPh : ClientBase, IClientPh
    {
        /// <summary>
        /// Constructeur par défaut.
        /// </summary>
        /// <param name="options">Options de configuration.</param>
        public ClientPh(IOptions<UrlConfig> options)
            : base(() => options.Value.UrlApiPh)
        {
        }
    }
}
