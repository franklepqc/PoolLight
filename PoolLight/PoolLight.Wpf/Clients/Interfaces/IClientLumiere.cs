using System.Threading.Tasks;

namespace PoolLight.Wpf.Clients.Interfaces
{
    public interface IClientLumiere
    {
        Task<bool> AllumerAsync();

        Task<bool> EteindreAsync();
    }
}
