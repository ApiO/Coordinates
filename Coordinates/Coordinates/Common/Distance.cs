using System;

namespace Coordinates.Common
{
    //FROM: https://stackoverflow.com/questions/27928/calculate-distance-between-two-latitude-longitude-points-haversine-formula

    /// <summary>
    /// Distance calculation common tools
    /// </summary>
    public static class Distance
    {
        private const double EARTH_RADIUS = 6371;

        /// <summary>
        /// Converts degree to radian
        /// </summary>
        /// <param name="degree"></param>
        /// <returns></returns>
        public static double ToRadian(double degree)
        {
            return Math.PI * degree / 180.0;
        }

        /// <summary>
        /// Calculate the distance in km between two WGS84 places in degree.
        /// </summary>
        /// <param name="lon1">Point 1 longitude</param>
        /// <param name="lat1">Point 1 latitude</param>
        /// <param name="lon2">Point 2 longitude</param>
        /// <param name="lat2">Point 2 latitude</param>
        /// <returns></returns>
        /// <remarks>WGS84 refers to greenwich</remarks>
        public static double BetweenWGS84Degree(double lon1, double lat1, double lon2, double lat2)
        {
            var dlon = ToRadian(lon2 - lon1);
            var dlat = ToRadian(lat2 - lat1);

            var a = Math.Sin(dlat / 2) * Math.Sin(dlat / 2) + Math.Cos(ToRadian(lat1)) * Math.Cos(ToRadian(lat2)) * (Math.Sin(dlon / 2) * Math.Sin(dlon / 2));
            var angle = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            return angle * EARTH_RADIUS;
        }
    }
}
