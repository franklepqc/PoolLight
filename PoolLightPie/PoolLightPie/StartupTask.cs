using PoolLightPie.Services;
using System;
using System.Threading.Tasks;
using Windows.ApplicationModel.Background;

namespace PoolLightPie
{
    public sealed class StartupTask : IBackgroundTask
    {
        /// <summary>
        /// Envoie des métriques pour l'application.
        /// </summary>
        /// <param name="taskInstance">Instance d'exécution.</param>
        public async void Run(IBackgroundTaskInstance taskInstance)
        {
            // Services.
            var serviceEnvoiCloud = new EnvoiCloud();
            var serviceLectureThermometre = new LectureTemperature();
            
            while (true)
            {
                // Récupération de la température.
                var temperature = serviceLectureThermometre.Lire();

                // Logging.
                Console.WriteLine($"Température à {DateTime.Now}: {temperature}");

                // Envoi.
                var differe = taskInstance.GetDeferral();

                // Appel.
                serviceEnvoiCloud.Envoyer(temperature);

                // Finition.
                differe.Complete();

                // Faire dormir le processus pour 15 minutes.
                Task.Delay(Convert.ToInt32(TimeSpan.FromSeconds(15d).TotalMilliseconds)).Wait();
            }
        }
    }
}
