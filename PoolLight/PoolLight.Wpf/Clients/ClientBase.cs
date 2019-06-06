using PoolLight.Wpf.Clients.Interfaces;
using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;

namespace PoolLight.Wpf.Clients
{
    /// <summary>
    /// Client pour les informations de l'eau.
    /// </summary>
    public abstract class ClientBase<T> : IClientBase<T>
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
        public Task<T> Obtenir() => HttpClientFactory.Create()
            .GetAsync(_url)
            .ContinueWith(reponse => ResultatRequete(reponse));

        /// <summary>
        /// Traiter le résultat de la requête.
        /// </summary>
        /// <param name="responseMessage">Réponse du travail fait par le client Http.</param>
        /// <returns>Instance selon le type demandé.</returns>
        private T ResultatRequete(Task<HttpResponseMessage> responseMessage)
        {
            // Valeur de retour.
            var retour = default(T);

            if (responseMessage.IsCompletedSuccessfully &&  // Tâche précédente est un succès.
                responseMessage.Result.IsSuccessStatusCode) // Code HTTP de retour est 2xx.
            {
                try
                {
                    retour = responseMessage.Result.Content.ReadAsAsync<T>().Result;
                }
                catch (Exception exception)
                {
                    Debug.WriteLine($@"Une erreur est survenue lors de la conversion de l'objet reçu par le service web : {exception.Message}");
                }
            }

            return retour;
        }
    }
}
