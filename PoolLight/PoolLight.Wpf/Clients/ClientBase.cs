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
        /// Obtention d'une valeur avec l'aide de l'url passée en passée.
        /// </summary>
        /// <param name="url">Url de l'appel de l'API.</param>
        /// <returns>Valeur.</returns>
        protected Task<float> Obtenir(string url) => HttpClientFactory.Create()
            .GetAsync(url)
            .ContinueWith(reponse => (reponse.IsCompletedSuccessfully ? reponse.Result.Content.ReadAsAsync<float>() : Task.FromResult<float>(default)))
            .ContinueWith(reponse => reponse.Result.Result);

        /// <summary>
        /// Doit implémenter la méthode.
        /// </summary>
        /// <returns>La valeur.</returns>
        public abstract Task<float> Obtenir();
    }
}
