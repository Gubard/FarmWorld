using System;

namespace Models
{
    public readonly struct Matrix2x2
    {
        public float M00 { get; }
        public float M10 { get; }
        public float M01 { get; }
        public float M11 { get; }
        public float Determinant { get; }

        public Matrix2x2(float m00, float m10, float m01, float m11)
        {
            M00 = m00;
            M10 = m10;
            M01 = m01;
            M11 = m11;
            Determinant = (float)(M00 * (double)M11 - M01 * (double)M10);
        }

        public override string ToString()
        {
            return $"{M00} {M01}{Environment.NewLine}{M10} {M11}";
        }
    }
}