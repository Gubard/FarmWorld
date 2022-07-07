using Unity.Mathematics;

namespace Constants
{
    public static class Float3Constants
    {
        public static readonly float3 Up = math.up();
        public static readonly float3 Right = math.right();
        public static readonly float3 Forward = math.forward();
        public static readonly float3 One = new(1, 1, 1);
    }
}