namespace PoolLight.Wpf.Clients.Interfaces
{
    public interface IInfosEau
    {
        float? Temperature { get; }
        float? PH { get; }
        System.DateTime DateDerniereMAJ { get; }
    }
}
