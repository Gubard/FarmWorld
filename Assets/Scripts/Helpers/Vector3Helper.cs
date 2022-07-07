using Models;
using Unity.Mathematics;
using UnityEngine;

namespace Helpers
{
    public static class Vector3Helper
    {
        public static float3 RotateOnNormal(float3 point, Straight normal, Degree degree)
        {
            var project = math.project(point - normal.PointZero, normal.Normal);
            var rotationCenter = normal.PointZero + project;
            var rotationAxis = normal.Normal;
            var relativePosition = point - rotationCenter;
            var rotatedAngle = Quaternion.AngleAxis(degree.Value, (float3)rotationAxis);
            var rotatedPosition = (float3)(rotatedAngle * relativePosition);
            var result = rotationCenter + rotatedPosition;

            return result;
        }

        public static Vector3 FromSingle(float value)
        {
            return new Vector3(value, value, value);
        }

        public static Vector3 FromSingleXZ(float value)
        {
            return new Vector3(value, 0, value);
        }
    }
}