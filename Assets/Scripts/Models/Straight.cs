using Constants;
using Extensions;
using Unity.Mathematics;

namespace Models
{
    public readonly struct Straight
    {
        public static readonly Straight X = new(float3.zero, math.right());
        public static readonly Straight Y = new(float3.zero, math.up());
        public static readonly Straight Z = new(float3.zero, math.forward());

        public StraightOptions Options { get; }
        public Normal3D Normal { get; }
        public float3 PointZero { get; }
        public float3 PointOne { get; }

        public Straight(float3 vector1, float3 vector2)
        {
            Options = new StraightOptions(vector1, vector2);
            PointZero = Options.GetPoint(float3.zero);
            PointOne = Options.GetPoint(Float3Constants.One);
            Normal = math.normalize(PointOne - PointZero).ToNormal3D();
        }

        public float3 GetPoint(float3 point)
        {
            return Options.GetPoint(point);
        }

        public float Distance(float3 point)
        {
            var vector = point - Options.M0;
            var cross = math.cross(vector, Options.DirectionVectorDirect);

            return math.length(cross) / Options.DirectionVectorDirect.Length;
        }
    }
}