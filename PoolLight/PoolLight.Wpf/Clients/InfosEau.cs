using System;

namespace PoolLight.Wpf.Clients
{
    public class InfosEau : Interfaces.IInfosEau
    {
        public float? Temperature { get; set; }

        public float? PH { get; set; }

        public DateTime DateDerniereMAJ { get; set; }
    }
}
