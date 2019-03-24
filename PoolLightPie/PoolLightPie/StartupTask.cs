using PoolLightPie.Services;
using PoolLightPie.Services.Interfaces;
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
        public void Run(IBackgroundTaskInstance taskInstance)
        {
            // Services.
            IEnvoiCloud serviceEnvoiCloud = new EnvoiCloud();
            IServiceLectureTemperature serviceLectureThermometre = new LectureTemperature();
            IServiceLecturePh serviceLecturePh = new LecturePh();

            while (true)
            {
                // Récupération de la température.
                var temperature = serviceLectureThermometre.Lire();
                var pH = serviceLecturePh.Lire();

                // Logging.
                var maintenant = DateTime.Now;
                Console.WriteLine($"Température à {maintenant}: {temperature}");
                Console.WriteLine($"pH à {maintenant}: {pH}");

                // Envoi.
                var differe = taskInstance.GetDeferral();

                // Appel.
                serviceEnvoiCloud.Envoyer(temperature, pH);

                // Finition.
                differe.Complete();

                // Faire dormir le processus pour 15 minutes.
                Task.Delay(Convert.ToInt32(TimeSpan.FromMinutes(15d).TotalMilliseconds)).Wait();
            }
        }
    }
}
