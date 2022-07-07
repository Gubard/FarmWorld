using Constants;
using Models;
using NUnit.Framework;
using Unity.Mathematics;

namespace Tests.EditMode
{
    public class StraightOptionsTests
    {
        [Test]
        public void T()
        {
            var straightOptions = new StraightOptions(float3.zero, Float3Constants.One);
            Assert.AreEqual(1, straightOptions.DirectionVectorDirect.X);
            Assert.AreEqual(1, straightOptions.DirectionVectorDirect.Y);
            Assert.AreEqual(1, straightOptions.DirectionVectorDirect.Z);
            Assert.AreEqual(0, straightOptions.M0.x);
            Assert.AreEqual(0, straightOptions.M0.y);
            Assert.AreEqual(0, straightOptions.M0.z);
        }
    }
}