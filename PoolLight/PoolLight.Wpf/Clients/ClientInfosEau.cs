using Microsoft.Azure.EventHubs;
using Newtonsoft.Json;
using PoolLight.Wpf.Clients.Interfaces;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoolLight.Wpf.Clients
{
    /// <summary>
    /// Client pour les informations de l'eau.
    /// </summary>
    public class ClientInfosEau : IClientInfosEau
    {
        /// <summary>
        /// Chaine de connexion pour le client IoTHub.
        /// </summary>
        private static readonly string CHAINE_CONNEXION = @"Endpoint=sb://ihsuprodyqres009dednamespace.servicebus.windows.net/;SharedAccessKeyName=iothubowner;SharedAccessKey=49MD9svPNWp07nvrAny0m2AtuTKsOqQPk76IOSRyepM=;EntityPath=iothub-ehub-poollight-1115586-a043f4f0aa";

        /// <summary>
        /// Client pour IotHub.
        /// </summary>
        private readonly EventHubClient _hubClient = EventHubClient.CreateFromConnectionString(CHAINE_CONNEXION);

        /// <summary>
        /// Obtention des informations de l'eau.
        /// </summary>
        /// <returns>Infos.</returns>
        public async Task<IInfosEau> Obtenir()
        {
            // Valeur de retour.
            var retour = default(IInfosEau);

            // Configuration du récepteur.
            var receiver = _hubClient.CreateReceiver("$Default", "0", EventPosition.FromEnqueuedTime(DateTime.Now));

            // Récupération des messages.
            var messages = await receiver.ReceiveAsync(100);

            // S'il y en a, prendre le dernier (plus à jour).
            if (messages.Any())
            {
                var message = messages.Last();

                // Conversion.
                string data = Encoding.UTF8.GetString(message.Body.Array);
                dynamic json = JsonConvert.DeserializeObject(data);

                // Assignation des nouvelles valeurs.
                retour = new InfosEau
                {
                    Temperature = json.temperature,
                    PH = json.pH,
                    DateDerniereMAJ = (DateTime)message.SystemProperties["iothub-enqueuedtime"]
                };
            }

            // Fermeture du récepteur.
            await receiver.CloseAsync();

            return retour;
        }
    }
}
