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
        private readonly static string _chaineConnexion = "HostName=PoolLight.azure-devices.net;DeviceId=Appareil1;SharedAccessKey=XRP0yav/9obOYpn5D+R2ME7Pgfcut4xLQquOu9nhrrM=";
        
        /// <summary>
        /// Client.
        /// </summary>
        private readonly DeviceClient _deviceClient = DeviceClient.CreateFromConnectionString(_chaineConnexion);

        /// <summary>
        /// Envoyer la température.
        /// </summary>
        /// <param name="temperature">Température.</param>
        /// <returns>Tâche.</returns>
        public async void Envoyer(float temperature)
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
            await _deviceClient.SendEventAsync(message);
        }
    }
}
