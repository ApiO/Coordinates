using Coordinates.Common;
using Coordinates.Lambert;
using Xunit;

namespace Coordinates.Tests
{
    public class DistanceTests
    {
        [Fact]
        public void ToRadianSucceed()
        {
            // ARRANGE

            const double value = 90;
            const double expect = 1.5707963267948966;

            // ACT

            var result = Distance.ToRadian(value);

            // ASSERT

            Assert.Equal(result, expect);
        }

        [Fact]
        public void BetweenPlacesSucceed()
        {
            // ARRANGE

            var a = new Point(46.1604417994864, -1.1856525744902, 0);
            var b = new Point(46.157745496233, -1.14359719009196, 0);
            const double expect = 4.685942614250779;

            // ACT

            var result = Distance.BetweenWGS84Degree(a.X, a.Y, b.X, b.Y);

            // ASSERT

            Assert.Equal(result, expect);
        }
    }
}
