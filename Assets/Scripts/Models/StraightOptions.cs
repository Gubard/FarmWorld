using Unity.Mathematics;

namespace Models
{
    public readonly struct StraightOptions
    {
        public Vector3D DirectionVectorDirect { get; }
        public float3 M0 { get; }

        public StraightOptions(float3 vector1, float3 vector2)
        {
            DirectionVectorDirect = new Vector3D(vector2.x - vector1.x, vector2.y - vector1.y, vector2.z - vector1.z);
            M0 = vector1;
        }

        private float GetYByX(float x)
        {
            return (x - M0.x) / DirectionVectorDirect.X * DirectionVectorDirect.Y + M0.y;
        }

        private float GetYByZ(float z)
        {
            return (z - M0.z) / DirectionVectorDirect.Z * DirectionVectorDirect.Y + M0.y;
        }

        private float GetZByX(float x)
        {
            return (x - M0.x) / DirectionVectorDirect.X * DirectionVectorDirect.Z + M0.z;
        }

        private float GetZByY(float y)
        {
            return (y - M0.y) / DirectionVectorDirect.Y * DirectionVectorDirect.Z + M0.z;
        }

        public float3 GetPoint(float3 point)
        {
            var resultX = DirectionVectorDirect.X == 0 ? M0.x : point.x;
            float resultY;
            var resultZ = 0f;

            if (DirectionVectorDirect.Y == 0)
            {
                resultY = M0.y;
            }
            else
            {
                if (DirectionVectorDirect.X != 0)
                {
                    resultY = GetYByX(point.x);
                }
                else if (DirectionVectorDirect.Z != 0)
                {
                    resultY = GetYByZ(resultZ);
                }
                else
                {
                    resultY = point.y;
                }
            }

            if (DirectionVectorDirect.Z == 0)
            {
                resultZ = M0.z;
            }
            else
            {
                if (DirectionVectorDirect.X != 0)
                {
                    resultZ = GetZByX(point.x);
                }
                else if (DirectionVectorDirect.Y != 0)
                {
                    resultZ = GetZByY(resultY);
                }
                else
                {
                    resultZ = point.z;
                }
            }

            return new float3(resultX, resultY, resultZ);
        }
    }
}