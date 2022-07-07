using Extensions;
using Interfaces;
using Unity.Mathematics;
using UnityEngine;
using Space = Models.Space;

namespace Services
{
    public class RectangularCellService : ICellService
    {
        public Space Space { get; }
        public float2 CellSize { get; }

        public RectangularCellService(Space space, float2 cellSize)
        {
            Space = space;
            CellSize = cellSize;
        }

        public int2 GetCellPositionByPlanePoint(float3 planePoint)
        {
            var xOrientation = Space.XOrientation.GetSide(planePoint).ToOrientation();
            var yOrientation = Space.YOrientation.GetSide(planePoint).ToOrientation();
            var distanceYAxis = Space.YAxis.Distance(planePoint);
            var distanceXAxis = Space.XAxis.Distance(planePoint);
            var x = distanceYAxis * xOrientation / CellSize.x;
            var y = distanceXAxis * yOrientation / CellSize.y;

            if (x.IsNegative())
            {
                x--;
            }

            if (y.IsNegative())
            {
                y--;
            }

            return new int2((int)x, (int)y);
        }

        public float3 GetCellCenter(int2 position)
        {
            var xCenter = position.x * CellSize.x + CellSize.x / 2;
            var yCenter = position.y * CellSize.y + CellSize.y / 2;
            var center = Space.ProjectOnPlane(new float3(xCenter, 0, yCenter) + Space.Options.Center);

            return center;
        }

        public float3 GetCellStartPoint(int2 position)
        {
            var xCenter = position.x * -CellSize.x;
            var yCenter = position.y * CellSize.y;
            var center = Space.ProjectOnPlane(new float3(xCenter, 0, yCenter) + Space.Options.Center);

            return center;
        }
    }
}