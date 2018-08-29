using System;

namespace Coordinates.Lambert
{
    //FROM https://github.com/yageek/lambert-cs

    /// <summary>
    /// Lambert conversion tools
    /// </summary>
    public static class Lambert
    {
        /// <summary>
        /// Convert Lambert* point into WGS84 in radian
        /// </summary>
        /// <param name="org">Point to convert</param>
        /// <param name="zone">Lambert type</param>
        /// <returns>Converted point</returns>
        /// <remarks>WGS84 refers to greenwich</remarks>
        public static Point ConvertToWGS84(Point org, Enums.LambertZoneType zone)
        {
            var lzone = new LambertZone(zone);

            if (zone == Enums.LambertZoneType.Lambert93)
            {
                return LambertToGeographic(org, lzone, LambertZone.LON_MERID_IERS, LambertZone.E_WGS84,
                    LambertZone.DEFAULT_EPS);
            }

            var pt1 = LambertToGeographic(org, lzone, LambertZone.LON_MERID_PARIS, LambertZone.E_CLARK_IGN,
                LambertZone.DEFAULT_EPS);

            var pt2 = GeographicToCartesian(pt1.X, pt1.Y, pt1.Z, LambertZone.A_CLARK_IGN,
                LambertZone.E_CLARK_IGN);

            pt2.Translate(-168, -60, 320);

            //WGS84 refers to greenwich
            return CartesianToGeographic(pt2, LambertZone.LON_MERID_GREENWICH, LambertZone.A_WGS84,
                LambertZone.E_WGS84, LambertZone.DEFAULT_EPS);
        }

        /// <summary>
        /// Convert Lambert* coordinates into WGS84 in radian
        /// </summary>
        /// <param name="x">x coordinates or latitude</param>
        /// <param name="y">y  coordinates or longitude</param>
        /// <param name="zone">Lambert type</param>
        /// <returns>Converted point</returns>
        /// <remarks>WGS84 refers to greenwich</remarks>
        public static Point ConvertToWGS84(double x, double y, Enums.LambertZoneType zone)
        {
            var pt = new Point(x, y, 0);
            return ConvertToWGS84(pt, zone);
        }

        /// <summary>
        /// Convert Lambert* point into WGS84 in degree
        /// </summary>
        /// <param name="x">x coordinates or latitude</param>
        /// <param name="y">y  coordinates or longitude</param>
        /// <param name="zone">Lambert type</param>
        /// <returns>Converted point</returns>
        /// <remarks>WGS84 refers to greenwich</remarks>
        public static Point ConvertToWGS84Deg(double x, double y, Enums.LambertZoneType zone)
        {
            var pt = new Point(x, y, 0);

            pt = ConvertToWGS84(pt, zone);
            pt.ToDegree();

            return pt;
        }

        #region privates

        private static double LatitudeFromLatitudeIso(double latISo, double e, double eps)
        {
            var phi0 = 2 * Math.Atan(Math.Exp(latISo)) - LambertZone.M_PI_2;
            var phiI =
                2 * Math.Atan(Math.Pow((1 + e * Math.Sin(phi0)) / (1 - e * Math.Sin(phi0)), e / 2d) *
                              Math.Exp(latISo)) - LambertZone.M_PI_2;
            var delta = Math.Abs(phiI - phi0);

            while (delta > eps)
            {
                phi0 = phiI;
                phiI = 2 * Math.Atan(Math.Pow((1 + e * Math.Sin(phi0)) / (1 - e * Math.Sin(phi0)), e / 2d) *
                                     Math.Exp(latISo)) - LambertZone.M_PI_2;
                delta = Math.Abs(phiI - phi0);
            }

            return phiI;
        }

        private static Point LambertToGeographic(Point org, LambertZone zone, double lonMeridian, double e, double eps)
        {
            var n = zone.n();
            var c = zone.c();
            var xs = zone.xs();
            var ys = zone.ys();

            var x = org.X;
            var y = org.Y;

            var r = Math.Sqrt((x - xs) * (x - xs) + (y - ys) * (y - ys));

            var gamma = Math.Atan((x - xs) / (ys - y));

            var lon = lonMeridian + gamma / n;

            var latIso = -1 / n * Math.Log(Math.Abs(r / c));

            var lat = LatitudeFromLatitudeIso(latIso, e, eps);

            var dest = new Point(lon, lat, 0);
            return dest;
        }

        private static double LambertNormal(double lat, double a, double e)
        {
            return a / Math.Sqrt(1 - e * e * Math.Sin(lat) * Math.Sin(lat));
        }

        private static Point GeographicToCartesian(double lon, double lat, double he, double a, double e)
        {
            var n = LambertNormal(lat, a, e);

            return new Point(0, 0, 0)
            {
                X = (n + he) * Math.Cos(lat) * Math.Cos(lon),
                Y = (n + he) * Math.Cos(lat) * Math.Sin(lon),
                Z = (n * (1 - e * e) + he) * Math.Sin(lat)
            };
        }

        private static Point CartesianToGeographic(Point org, double meridien, double a, double e, double eps)
        {
            double x = org.X, y = org.Y, z = org.Z;

            var lon = meridien + Math.Atan(y / x);

            var module = Math.Sqrt(x * x + y * y);

            var phi0 = Math.Atan(z / (module * (1 - (a * e * e) / Math.Sqrt(x * x + y * y + z * z))));
            var phiI = Math.Atan(z / module / (1 - a * e * e * Math.Cos(phi0) /
                                                  (module * Math.Sqrt(1 - e * e * Math.Sin(phi0) * Math.Sin(phi0)))
                                    ));
            var delta = Math.Abs(phiI - phi0);
            while (delta > eps)
            {
                phi0 = phiI;
                phiI = Math.Atan(z / module / (1 - a * e * e * Math.Cos(phi0) /
                                               (module * Math.Sqrt(1 - e * e * Math.Sin(phi0) * Math.Sin(phi0)))));
                delta = Math.Abs(phiI - phi0);

            }

            var he = module / Math.Cos(phiI) - a / Math.Sqrt(1 - e * e * Math.Sin(phiI) * Math.Sin(phiI));

            var pt = new Point(lon, phiI, he);

            return pt;
        }

        #endregion
    }
}
