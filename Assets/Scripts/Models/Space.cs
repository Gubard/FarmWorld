using Constants;
using Extensions;
using Unity.Mathematics;
using UnityEngine;

namespace Models
{
    public readonly struct Space
    {
        public static readonly Space XZ = new(Plane3D.XZ, Vector2.zero, Degree.Zero);

        public SpaceOptions Options { get; }
        public Straight XAxis { get; }
        public Straight YAxis { get; }
        public Plane3D XOrientation { get; }
        public Plane3D YOrientation { get; }

        public Space(Plane3D plane, Vector2 centerOffset, Degree rotatePlane)
        {
            Options = new SpaceOptions(plane, centerOffset, rotatePlane);
            var pointRight = Float3Constants.Right + Options.Center;
            var pointForward = Float3Constants.Forward + Options.Center;
            var xAxisPoint = Options.ProjectOnPlane(pointRight);
            var yAxisPoint = Options.ProjectOnPlane(pointForward);
            XAxis = new Straight(Options.WorldCenter, xAxisPoint);
            YAxis = new Straight(Options.WorldCenter, yAxisPoint);
            XOrientation = Options.Plane.GetPerpendicularStraight(YAxis, false);
            YOrientation = Options.Plane.GetPerpendicularStraight(XAxis, true);
        }

        public float3 ProjectOnPlane(float3 vector3)
        {
            return Options.ProjectOnPlane(vector3);
        }
    }
}