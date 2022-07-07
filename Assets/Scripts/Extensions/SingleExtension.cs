using UnityEngine;

namespace Extensions
{
    public static class SingleExtension
    {
        public static bool IsNegative(this float value)
        {
            return value < 0;
        }

        public static bool Equals(this float x, float y, float epsilon)
        {
            return Mathf.Abs(x - y) < epsilon;
        }

        public static Vector2 ToVector2(this float value)
        {
            return new Vector2(value, value);
        }

        public static Vector3 ToVector3(this float value)
        {
            return new Vector3(value, value, value);
        }
        
        public static Vector3 ToVector3Y(this float value)
        {
            return new Vector3(0, value, 0);
        }
    }
}