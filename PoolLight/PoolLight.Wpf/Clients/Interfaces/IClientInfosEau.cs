using System.Threading.Tasks;

namespace PoolLight.Wpf.Clients.Interfaces
{
    public interface IClientInfosEau
    {
        Task<IInfosEau> Obtenir();
    }
}
