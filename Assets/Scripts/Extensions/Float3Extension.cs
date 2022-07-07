using Models;
using Unity.Mathematics;

namespace Extensions
{
    public static class Float3Extension
    {
        public static Normal3D ToNormal3D(this float3 value)
        {
            return new Normal3D(value);
        }

        public static float3 ProjectOnPlane(this float3 value, Normal3D planeNormal)
        {
            var num1 = math.dot(planeNormal, planeNormal);

            if (num1 < math.EPSILON)
            {
                return value;
            }

            var num2 = math.dot(value, planeNormal);
            var x = value.x - planeNormal.X * num2 / num1;
            var y = value.y - planeNormal.Y * num2 / num1;
            var z = value.z - planeNormal.Z * num2 / num1;

            return new float3(x, y, z);
        }
    }
}