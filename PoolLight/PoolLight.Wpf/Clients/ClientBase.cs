using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace PoolLight.Wpf.Clients
{
    /// <summary>
    /// Client pour les informations de l'eau.
    /// </summary>
    public abstract class ClientBase
    {
        /// <summary>
        /// Url.
        /// </summary>
        private readonly string _url;

        /// <summary>
        /// Constructeur par défaut.
        /// </summary>
        /// <param name="propriete">Propriété contenant l'url.</param>
        protected ClientBase(Func<string> propriete)
        {
            _url = propriete.Invoke();
        }

        /// <summary>
        /// Obtention de la valeur selon l'url.
        /// </summary>
        /// <returns>Valeur.</returns>
        public Task<float> Obtenir() => HttpClientFactory.Create()
            .GetAsync(_url)
            .ContinueWith(reponse => (reponse.IsCompletedSuccessfully ? reponse.Result.Content.ReadAsAsync<float>() : Task.FromResult<float>(default)))
            .ContinueWith(reponse => reponse.Result.Result);
    }
}
