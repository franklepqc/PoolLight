using System;

namespace PoolLightPie.Services
{
    public sealed class LectureTemperature : Interfaces.IServiceLectureTemperature
    {
        private static Random _hasard;

        public float Lire()
        {
            if (null == _hasard) _hasard = new Random();

            return (float)(21.1111f + (_hasard.NextDouble() * (32.2222f - 21.1111f)));
        }
    }
}
