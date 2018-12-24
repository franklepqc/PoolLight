using System.Threading.Tasks;

namespace PoolLight.Wpf.Clients.Interfaces
{
    public interface IClientPH
    {
        Task<float> Obtenir();
    }
}
