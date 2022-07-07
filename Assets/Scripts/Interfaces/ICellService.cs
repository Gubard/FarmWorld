using Unity.Mathematics;

namespace Interfaces
{
    public interface ICellService
    {
        int2 GetCellPositionByPlanePoint(float3 planePoint);
        float3 GetCellCenter(int2 position);
        float3 GetCellStartPoint(int2 position);
    }
}