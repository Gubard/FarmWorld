using Unity.Mathematics;

namespace Models
{
    public readonly struct Vector3D
    {
        private readonly float3 _value;
        public readonly float Length;
        public readonly Normal3D Normal;

        public float X => _value.x;
        public float Y => _value.y;
        public float Z => _value.z;

        public Vector3D(float3 value)
        {
            _value = value;
            Normal = new Normal3D(value);
            Length = math.length(value);
        }

        public Vector3D(float x, float y, float z) : this(new float3(x, y, z))
        {
        }

        public static implicit operator float3(Vector3D vector)
        {
            return vector._value;
        }
    }
}