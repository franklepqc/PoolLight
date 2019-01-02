using System.Threading.Tasks;

namespace PoolLight.Wpf.Services.Interfaces
{
    /// <summary>
    /// Interface pour la récupération du pH.
    /// </summary>
    public interface IRecuperationPh
    {
        Task<float> Obtenir();
    }
}
