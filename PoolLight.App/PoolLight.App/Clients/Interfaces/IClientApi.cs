using System.Threading.Tasks;

namespace PoolLight.App.Clients.Interfaces
{
    public interface IClientApi
    {
        Task<bool> AllumerAsync();

        Task<bool> EteindreAsync();
    }
}
