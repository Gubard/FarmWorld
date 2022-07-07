using Constants;
using NUnit.Framework;
using UnityEngine;

namespace Tests.EditMode
{
    public class Vector3Tests
    {
        [Test]
        public static void ProjectOnPlane_ZeroPoint_Zero()
        {
            var project = Vector3.ProjectOnPlane(Vector3.zero, PlaneConstants.XZ.normal);
            Assert.AreEqual(Vector3.zero, project);
        }

        [Test]
        public static void ProjectOnPlane_ZeroPoint_One()
        {
            var plane = new Plane(Vector3.forward, Vector3.right, Vector3.up);
            var project = Vector3.ProjectOnPlane(Vector3.back, plane.normal);
            Assert.AreEqual(new Vector3(0.333333343f, 0.333333343f, -0.666666687f), project);
        }
    }
}