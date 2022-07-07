using Models;
using Services;
using Unity.Mathematics;
using UnityEngine;

namespace Extensions
{
    public static class RectangularCellServiceExtension
    {
        public static bool GetCellPositionByMouse(this RectangularCellService service,
            Camera camera,
            out int2 result)
        {
            if (service.Space.Options.Plane.RaycastMouse(camera, out var position))
            {
                result = service.GetCellPositionByPlanePoint(position);

                return true;
            }

            result = default;

            return false;
        }
        
        public static CellParameters GetCellParametersByCellPosition(this RectangularCellService service,
            int2 cellPosition)
        {
            var xCenter = cellPosition.x * service.CellSize.x + service.CellSize.x / 2;
            var yCenter = cellPosition.y * service.CellSize.y + service.CellSize.y / 2;
            var center = service.Space.ProjectOnPlane(new float3(xCenter, 0, yCenter) + service.Space.Options.Center);
            var top = new float3(xCenter, 0, yCenter + service.CellSize.y / 2);
            var left = new float3(xCenter - service.CellSize.x / 2, 0, yCenter);
            var right = new float3(xCenter + service.CellSize.x / 2, 0, yCenter);
            var bottom = new float3(xCenter, 0, yCenter - service.CellSize.y / 2);
            var topCentred = service.Space.ProjectOnPlane(top + service.Space.Options.Center);
            var leftCentred = service.Space.ProjectOnPlane(left + service.Space.Options.Center);
            var rightCentred = service.Space.ProjectOnPlane(right + service.Space.Options.Center);
            var bottomCentred = service.Space.ProjectOnPlane(bottom + service.Space.Options.Center);

            return new CellParameters(center, topCentred, leftCentred, rightCentred, bottomCentred);
        }

    }
}