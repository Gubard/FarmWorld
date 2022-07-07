using Extensions;
using Models;
using UnityEngine;

namespace Constants
{
    public static class GeometryHelper
    {
        public static Plane GetPerpendicularStraight(Vector3 m1, Vector3 m2, Vector3 planeNormal, bool orientation)
        {
            var vector = m2 - m1;
            var xMatrix = new Matrix2x2(vector.y, vector.z, planeNormal.y, planeNormal.z);
            var yMatrix = new Matrix2x2(vector.x, vector.z, planeNormal.x, planeNormal.z);
            var zMatrix = new Matrix2x2(vector.x, vector.y, planeNormal.x, planeNormal.y);
            var yCount = (double)m2.y * yMatrix.Determinant;
            var xCount = (double)m2.x * xMatrix.Determinant;
            var zCount = (double)m2.z * zMatrix.Determinant;
            var distance = yCount - xCount - zCount;
            var orientationValue = orientation.ToOrientation();
            var normal = new Vector3(xMatrix.Determinant, -yMatrix.Determinant, zMatrix.Determinant);

            return new Plane(normal * orientationValue, (float)(distance * orientationValue));
        }
    }
}