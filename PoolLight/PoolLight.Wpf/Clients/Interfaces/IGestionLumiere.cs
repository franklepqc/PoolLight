using System.Threading.Tasks;

namespace PoolLight.Wpf.Clients.Interfaces
{
    public interface IGestionLumiere
    {
        Task<bool> AllumerAsync();

        Task<bool> EteindreAsync();
    }
}
