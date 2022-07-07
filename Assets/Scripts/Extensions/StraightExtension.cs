using Constants;
using Models;
using UnityEngine;

namespace Extensions
{
    public static class StraightExtension
    {
        public static Plane GetPerpendicularStraight(this Straight straight, Vector3 planeNormal, bool orientation)
        {
            return GeometryHelper.GetPerpendicularStraight(straight.PointZero,
                straight.PointOne,
                planeNormal,
                orientation);
        }
    }
}