using Coordinates.Lambert;
using Coordinates.Lambert.Enums;
using Xunit;

namespace Coordinates.Tests
{
    public class LambertTests
    {
        [Fact]
        public void ConvertToWGS84DegLambert93Succeed()
        {
            // ARRANGE

            var source = new Point(668832.5384, 6950138.7285, 0);
            var expect = new Point(2.5686536326051743, 49.649610985851474, 0);

            // ACT

            var result = Lambert.Lambert.ConvertToWGS84Deg(source.X, source.Y, LambertZoneType.Lambert93);

            // ASSERT

            Assert.Equal(expect.X, result.X);
            Assert.Equal(expect.Y, result.Y);
            Assert.Equal(expect.Z, result.Z);
        }

        [Fact]
        public void ConvertToWGS84Lambert93Succeed()
        {
            // ARRANGE

            var source = new Point(668832.5384, 6950138.7285, 0);
            var expect = new Point(0.044831463232273064, 0.866549184037456, 0);

            // ACT

            var result = Lambert.Lambert.ConvertToWGS84(source.X, source.Y, LambertZoneType.Lambert93);

            // ASSERT

            Assert.Equal(expect.X, result.X);
            Assert.Equal(expect.Y, result.Y);
            Assert.Equal(expect.Z, result.Z);
        }

        [Fact]
        public void ConvertToWGS84LambertIvSucceed()
        {
            // ARRANGE

            var source = new Point(668832.5384, 6950138.7285, 0);
            var expect = new Point(-1.3676823532281193, 1.5407374270761811, 81.892038438469172);

            // ACT

            var result = Lambert.Lambert.ConvertToWGS84(source.X, source.Y, LambertZoneType.LambertIV);

            // ASSERT

            Assert.Equal(expect.X, result.X);
            Assert.Equal(expect.Y, result.Y);
            Assert.Equal(expect.Z, result.Z);
        }
    }
}
