using Microsoft.Extensions.Options;
using PoolLight.Wpf.Clients.Interfaces;
using System.Threading.Tasks;

namespace PoolLight.Wpf.Clients
{
    /// <summary>
    /// Client pour les informations de l'eau.
    /// </summary>
    public class ClientTemperature : ClientBase, IClientTemperature
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
        /// Obtention de la température de l'eau.
        /// </summary>
        /// <returns>Température.</returns>
        public override Task<float> Obtenir() => 
            Obtenir(_url);
    }
}
