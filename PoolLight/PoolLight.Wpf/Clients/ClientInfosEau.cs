using PoolLight.Wpf.Clients.Interfaces;
using PoolLight.Wpf.Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace PoolLight.Wpf.Clients
{
    /// <summary>
    /// Client pour les informations de l'eau.
    /// </summary>
    public class ClientInfosEau : IClientInfosEau
    {
        /// <summary>
        /// Champs.
        /// </summary>
        private readonly IRecuperationTemperature _recuperationTemperature;
        private readonly IRecuperationPh _recuperationPh;

        /// <summary>
        /// Constructeur par injection.
        /// </summary>
        /// <param name="recuperationTemperature">Injection du service de récupération de la température.</param>
        /// <param name="recuperationPh">Injection du service de récupération du pH.</param>
        public ClientInfosEau(IRecuperationTemperature recuperationTemperature, IRecuperationPh recuperationPh)
        {
            _recuperationTemperature = recuperationTemperature;
            _recuperationPh = recuperationPh;
        }

        /// <summary>
        /// Obtention des informations de l'eau.
        /// </summary>
        /// <returns>Infos.</returns>
        public async Task<IInfosEau> Obtenir() =>
            new InfosEau
            {
                Temperature = await _recuperationTemperature.Obtenir(),
                PH = await _recuperationPh.Obtenir(),
                DateDerniereMAJ = DateTime.Now
            };
    }
}
