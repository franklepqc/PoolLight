using System.Threading.Tasks;

namespace PoolLight.Wpf.Services.Interfaces
{
    /// <summary>
    /// Interface pour la récupération de la température.
    /// </summary>
    public interface IRecuperationTemperature
    {
        Task<float> Obtenir();
    }
}
