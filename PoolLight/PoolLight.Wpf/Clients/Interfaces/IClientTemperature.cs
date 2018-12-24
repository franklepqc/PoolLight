using System.Threading.Tasks;

namespace PoolLight.Wpf.Clients.Interfaces
{
    public interface IClientTemperature
    {
        Task<float> Obtenir();
    }
}
