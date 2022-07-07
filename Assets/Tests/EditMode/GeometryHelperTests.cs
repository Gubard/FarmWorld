using Constants;
using NUnit.Framework;
using UnityEngine;

namespace Tests.EditMode
{
    public class GeometryHelperTests
    {
        [Test]
        public void DefaultTask()
        {
            var plane = GeometryHelper.GetPerpendicularStraight(new Vector3(2, 1, -3),
                new Vector3(1, 0, 5),
                new Vector3(1, -1, 1),
                true);

            Assert.AreEqual(0.604707897f, plane.normal.x);
            Assert.AreEqual(0.777481556f, plane.normal.y);
            Assert.AreEqual(0.172773689f, plane.normal.z);
            Assert.AreEqual(-17, plane.distance);
        }
    }
}