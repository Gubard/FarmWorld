using Unity.Mathematics;

namespace Models
{
    public readonly struct CellParameters
    {
        public float3 CellCenter { get; }
        public float3 TopCenter { get; }
        public float3 LeftCenter { get; }
        public float3 RightCenter { get; }
        public float3 BottomCenter { get; }

        public CellParameters(float3 cellCenter,
            float3 topCenter,
            float3 leftCenter, 
            float3 rightCenter, 
            float3 bottomCenter)
        {
            CellCenter = cellCenter;
            TopCenter = topCenter;
            LeftCenter = leftCenter;
            RightCenter = rightCenter;
            BottomCenter = bottomCenter;
        }
    }
}