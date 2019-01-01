using System.Threading.Tasks;

namespace PoolLight.Wpf.Clients.Interfaces
{
    public interface IClientLumiere
    {
        Task<bool> EstAllumeeAsync();

        Task<bool> AllumerAsync();

        Task<bool> EteindreAsync();
    }
}
