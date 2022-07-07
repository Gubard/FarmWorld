using System;

namespace Extensions
{
    public static class Int32Extension
    {
        public static void ThrowIsLess(this int value, int limit)
        {
            if (value < limit)
            {
                throw new Exception($"Value {value} less {limit}.");
            }
        }
    }
}