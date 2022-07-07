using Constants;
using Models;
using UnityEngine;

namespace Extensions
{
    public static class PlaneExtension
    {
        public static bool RaycastMouse(this Plane plane, Camera camera, out Vector3 position)
        {
            var ray = camera.GetMouseRay();

            if (plane.Raycast(ray, out var enter))
            {
                position = ray.GetPoint(enter);

                return true;
            }

            position = default;

            return false;
        }

        public static Plane GetPerpendicularStraight(this Plane plane, Straight straight, bool orientation)
        {
            return GeometryHelper.GetPerpendicularStraight(straight.PointZero,
                straight.PointOne,
                plane.normal,
                orientation);
        }
    }
}