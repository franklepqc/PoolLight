using System;

namespace PoolLightPie.Services
{
    public sealed class LecturePh : Interfaces.IServiceLecturePh
    {
        private static Random _hasard;

        public float Lire()
        {
            if (null == _hasard) _hasard = new Random();

            return (float)(1f + (_hasard.NextDouble() * (14f - 1f)));
        }
    }
}
