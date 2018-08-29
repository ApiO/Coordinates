using System;
using Coordinates.Lambert.Enums;

namespace Coordinates.Lambert
{
    /// <summary>
    /// Lambert zone helper
    /// </summary>
    public class LambertZone
    {
        private static readonly double[] LAMBERT_N = { 0.7604059656, 0.7289686274, 0.6959127966, 0.6712679322, 0.7289686274, 0.7256077650 };
        private static readonly double[] LAMBERT_C = { 11603796.98, 11745793.39, 11947992.52, 12136281.99, 11745793.39, 11754255.426 };
        private static readonly double[] LAMBERT_XS = { 600000.0, 600000.0, 600000.0, 234.358, 600000.0, 700000.0 };
        private static readonly double[] LAMBERT_YS = { 5657616.674, 6199695.768, 6791905.085, 7239161.542, 8199695.768, 12655612.050 };

        public const double M_PI_2 = Math.PI / 2.0;
        public const double DEFAULT_EPS = 1e-10;
        public const double E_CLARK_IGN = 0.08248325676;
        public const double E_WGS84 = 0.08181919106;

        public const double A_CLARK_IGN = 6378249.2;
        public const double A_WGS84 = 6378137.0;
        public const double LON_MERID_PARIS = 0;
        public const double LON_MERID_GREENWICH = 0.04079234433;
        public const double LON_MERID_IERS = 3.0 * Math.PI / 180.0;

        private readonly LambertZoneType _lambertZone;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="zone"></param>
        public LambertZone(LambertZoneType zone)
        {
            _lambertZone = zone;
        }

        /// <summary>
        /// Extracts n component
        /// </summary>
        /// <returns></returns>
        public double n()
        {
            return LAMBERT_N[(int)_lambertZone];
        }

        /// <summary>
        /// Extracts s component
        /// </summary>
        /// <returns></returns>
        public double c()
        {
            return LAMBERT_C[(int)_lambertZone];
        }

        /// <summary>
        /// Extracts xs component
        /// </summary>
        /// <returns></returns>
        public double xs()
        {
            return LAMBERT_XS[(int)_lambertZone];
        }

        /// <summary>
        /// Extracts ys component
        /// </summary>
        /// <returns></returns>
        public double ys()
        {
            return LAMBERT_YS[(int)_lambertZone];
        }
    }
}