using Unity.Mathematics;

namespace Models
{
    public readonly struct Normal3D
    {
        private readonly float3 _value;

        public float X => _value.x;
        public float Y => _value.y;
        public float Z => _value.z;

        public Normal3D(float3 value)
        {
            _value = math.normalize(value);
        }

        public static implicit operator float3(Normal3D normal)
        {
            return normal._value;
        }
    }
}