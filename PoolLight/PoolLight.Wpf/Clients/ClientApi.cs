using Microsoft.Azure.Devices.Client;
using System;
using System.Threading.Tasks;
using System.Text;
using Newtonsoft.Json;

namespace PoolLight.Wpf.Clients
{
    /// <summary>
    /// Implémentation de l'API pour le globe.
    /// </summary>
    public class ClientApi : Interfaces.IClientApi
    {
        // Champs.
        const string IOT_HUB_CONN_STRING = "HostName=Piscine.azure-devices.net;DeviceId=Device1;SharedAccessKey=NQrYf3ceEfGeO5qkC6Ds4eH0D0lNiFZkt1TujAMbMhQ=";
        const string IOT_HUB_DEVICE_LOCATION = "canadaeast";
        const string IOT_HUB_DEVICE = "Device1";

        /// <summary>
        /// Allumer le globe.
        /// </summary>
        /// <returns>Tâche qui peut être attendue.</returns>
        public async Task<bool> AllumerAsync()
        {
            var deviceClient = ObtenirClient();
            try
            {
                var msg = new Message(ConstruireMessage("close"));
                await deviceClient.SendEventAsync(msg);
                return true;
            }
            catch
            {
            }

            return false;
        }

        /// <summary>
        /// Éteindre le globe.
        /// </summary>
        /// <returns>Tâche qui peut être attendue.</returns>
        public async Task<bool> EteindreAsync()
        {
            var deviceClient = ObtenirClient();
            try
            {
                var msg = new Message(ConstruireMessage("open"));
                await deviceClient.SendEventAsync(msg);
                return true;
            }
            catch
            {
            }

            return false;
        }

        /// <summary>
        /// Construire le message.
        /// </summary>
        /// <param name="etat">État du statut.</param>
        /// <returns></returns>
        private byte[] ConstruireMessage(string etat) =>
            Encoding.UTF8.GetBytes(ConstruireContenuMessage(etat));

        /// <summary>
        /// Construire le contenu pour le message.
        /// </summary>
        /// <param name="etat"></param>
        /// <returns></returns>
        private string ConstruireContenuMessage(string etat) =>
            JsonConvert.SerializeObject(new
            {
                deviceId = IOT_HUB_DEVICE,
                location = IOT_HUB_DEVICE_LOCATION,
                state = etat,
                localTimestamp = DateTime.Now.ToLocalTime()
            });

        /// <summary>
        /// Obtenir le client.
        /// </summary>
        /// <returns>Un objet de type DeviceClient.</returns>
        private DeviceClient ObtenirClient() => 
            DeviceClient.CreateFromConnectionString(IOT_HUB_CONN_STRING, TransportType.Amqp);

        /// <summary>
        /// Obtenir la température.
        /// </summary>
        /// <returns></returns>
        public Task<float> ObtenirTemperatureAsync() =>
            Task.FromResult(15.56f);

        /// <summary>
        /// Obtenir le pH.
        /// </summary>
        /// <returns></returns>
        public Task<float> ObtenirpH() =>
            Task.FromResult(7f);
    }
}
