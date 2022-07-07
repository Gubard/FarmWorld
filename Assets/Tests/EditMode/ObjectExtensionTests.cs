using Extensions;
using NUnit.Framework;

namespace Tests.EditMode
{
    public class ObjectExtensionTests
    {
        [Test]
        public void ThrowIfEquals_EqualsNotItems_Object()
        {
            var x = 1;
            var y = 2;
            x.ThrowIfEquals(y);
        }

        [Test]
        public void ThrowIfEquals_EqualsItems_Throw()
        {
            var x = 1;
            var y = 1;

            try
            {
                x.ThrowIfEquals(y);
            }
            catch
            {
                Assert.Pass();
            }

            Assert.Fail();
        }
    }
}