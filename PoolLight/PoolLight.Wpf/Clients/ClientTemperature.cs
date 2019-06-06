using Microsoft.Extensions.Options;
using PoolLight.Wpf.Clients.Interfaces;

namespace PoolLight.Wpf.Clients
{
    /// <summary>
    /// Client pour les informations de l'eau.
    /// </summary>
    public class ClientTemperature : ClientBase, IClientTemperature
    {
        /// <summary>
        /// Constructeur par défaut.
        /// </summary>
        /// <param name="options">Options de configuration.</param>
        public ClientTemperature(IOptions<UrlConfig> options)
            : base(() => options.Value.UrlApiTemperature)
        {
        }
    }
}
