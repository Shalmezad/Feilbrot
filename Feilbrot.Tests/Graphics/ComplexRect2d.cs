using NUnit.Framework;

using Feilbrot.Graphics;

namespace Feilbrot.Graphics.UnitTests
{
    [TestFixture]
    public class ComplexRect2d_PointAtPercentShould
    {
        [Test]
        public void PointAtPercent_ReturnProperPoint()
        {
            ComplexPoint2d point = new ComplexPoint2d(-4M, -4M);
            ComplexRect2d rect = new ComplexRect2d(point, 8M, 8M);
            ComplexPoint2d result;

            //Halfway should be 0,0
            result = rect.PointAtPercent(0.5M, 0.5M);
            Assert.That(result.r, Is.EqualTo(0.0M).Within(0.00001));
            Assert.That(result.i, Is.EqualTo(0.0M).Within(0.00001));

            // 1/4 should be -2, 3/4 should be 2:
            result = rect.PointAtPercent(0.25M, 0.75M);
            Assert.That(result.r, Is.EqualTo(-2.0M).Within(0.00001));
            Assert.That(result.i, Is.EqualTo(2.0M).Within(0.00001));
        }
    }
}