using Constants;
using Extensions;
using Unity.Mathematics;
using UnityEngine;

namespace Models
{
    public readonly struct Plane3D
    {
        public static readonly Plane3D XZ = new(Float3Constants.Up, float3.zero);

        public readonly float Distance;
        public readonly Normal3D Normal;

        public Plane3D(Normal3D normal, float3 inPoint)
        {
            Normal = normal;
            Distance = -math.dot(Normal, inPoint);
        }

        public Plane3D(Normal3D normal, float d)
        {
            Normal = normal;
            Distance = d;
        }

        public Plane3D(float3 inNormal, float3 inPoint)
        {
            Normal = inNormal.ToNormal3D();
            Distance = -math.dot(Normal, inPoint);
        }

        public Plane3D(float3 inNormal, float d)
        {
            Normal = inNormal.ToNormal3D();
            Distance = d;
        }

        public Plane3D(float3 a, float3 b, float3 c)
        {
            Normal = math.cross(b - a, c - a).ToNormal3D();
            Distance = -math.dot(Normal, a);
        }

        public bool GetSide(float3 point)
        {
            return math.dot(Normal, point) + (double)Distance > 0.0;
        }

        public Plane3D GetPerpendicularStraight(Straight straight, bool orientation)
        {
            return GetPerpendicularStraight(straight.PointZero,
                straight.PointOne,
                Normal,
                orientation);
        }

        public bool Raycast(Ray ray, out float enter)
        {
            var a = math.dot(ray.direction, Normal);
            var num = -math.dot(ray.origin, Normal) - Distance;

            if (Mathf.Approximately(a, 0.0f))
            {
                enter = 0.0f;

                return false;
            }

            enter = num / a;

            return enter > 0.0;
        }

        public bool RaycastMouse(Camera camera, out float3 position)
        {
            var ray = camera.GetMouseRay();

            if (Raycast(ray, out var enter))
            {
                position = ray.GetFloat3Point(enter);

                return true;
            }

            position = default;

            return false;
        }

        public static Plane3D GetPerpendicularStraight(float3 m1, float3 m2, Normal3D planeNormal, bool orientation)
        {
            var vector = m2 - m1;
            var xMatrix = new float2x2(vector.y, vector.z, planeNormal.Y, planeNormal.Z);
            var yMatrix = new float2x2(vector.x, vector.z, planeNormal.X, planeNormal.Z);
            var zMatrix = new float2x2(vector.x, vector.y, planeNormal.X, planeNormal.Y);
            var xMatrixDeterminant = math.determinant(xMatrix);
            var yMatrixDeterminant = math.determinant(yMatrix);
            var zMatrixDeterminant = math.determinant(zMatrix);
            var yCount = (double)m2.y * yMatrixDeterminant;
            var xCount = (double)m2.x * xMatrixDeterminant;
            var zCount = (double)m2.z * zMatrixDeterminant;
            var distance = yCount - xCount - zCount;
            var orientationValue = orientation.ToOrientation();
            var normal = new float3(xMatrixDeterminant, -yMatrixDeterminant, zMatrixDeterminant);

            return new Plane3D(normal * orientationValue, (float)(distance * orientationValue));
        }
    }
}