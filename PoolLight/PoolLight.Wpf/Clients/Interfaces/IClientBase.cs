using System.Threading.Tasks;

namespace PoolLight.Wpf.Clients.Interfaces
{
    public interface IClientBase<T>
    {
        Task<T> Obtenir();
    }
}
