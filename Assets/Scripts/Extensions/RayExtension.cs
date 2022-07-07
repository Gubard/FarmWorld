using Unity.Mathematics;
using UnityEngine;

namespace Extensions
{
    public static class RayExtension
    {
        public static float3 GetFloat3Point(this Ray ray, float distance)
        {
            return (float3)ray.origin + (float3)ray.direction * distance;
        }
    }
}