using Microsoft.Azure.Devices.Client;
using Newtonsoft.Json;
using System.Text;

namespace PoolLightPie.Services
{
    /// <summary>
    /// Classe permettant d'envoyer les données sur le cloud.
    /// </summary>
    public sealed class EnvoiCloud : Interfaces.IEnvoiCloud
    {
        /// <summary>
        /// Chaîne de connexion au Cloud.
        /// </summary>
        private readonly static string _chaineConnexion = string.Empty;
        
        /// <summary>
        /// Client.
        /// </summary>
        private readonly DeviceClient _deviceClient = DeviceClient.CreateFromConnectionString(_chaineConnexion);

        /// <summary>
        /// Envoyer la température.
        /// </summary>
        /// <param name="temperature">Température.</param>
        /// <returns>Tâche.</returns>
        public void Envoyer(double temperature)
        {
            // Créer le message en Json.
            var messageJson = new
            {
                temperature
            };

            // Formatter.
            var messageString = JsonConvert.SerializeObject(messageJson);

            // Message à envoyer.
            var message = new Message(Encoding.ASCII.GetBytes(messageString));

            // Envoie.
            _deviceClient.SendEventAsync(message).Wait();
        }
    }
}
