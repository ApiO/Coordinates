using System;

namespace Coordinates.Lambert
{
    /// <summary>
    /// Defines a 3D point in space
    /// </summary>
    public class Point
    {
        /// <summary>
        /// X coordinates
        /// </summary>
        public double X { get; set; }
        /// <summary>
        /// Y coordinates
        /// </summary>
        public double Y { get; set; }
        /// <summary>
        /// Z coordinates
        /// </summary>
        public double Z { get; set; }

        private const double RADIAN_TODEGREE = 180.0 / Math.PI;

        /// <summary>
        /// Ctor
        /// </summary>
        public Point(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        /// <summary>
        /// Apply a translation to current point
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        public void Translate(double x, double y, double z)
        {
            X += x;
            Y += y;
            Z += z;
        }

        /// <summary>
        /// Scale current point
        /// </summary>
        /// <param name="scale"></param>
        private void Scale(double scale)
        {
            X *= scale;
            Y *= scale;
            Z *= scale;
        }

        /// <summary>
        /// Convert to degree
        /// </summary>
        public void ToDegree()
        {
            Scale(RADIAN_TODEGREE);
        }
    }
}
