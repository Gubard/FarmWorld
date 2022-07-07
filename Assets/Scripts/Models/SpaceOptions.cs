using Constants;
using Extensions;
using Helpers;
using Unity.Mathematics;
using UnityEngine;

namespace Models
{
    public readonly struct SpaceOptions
    {
        public Plane3D Plane { get; }
        public float2 CenterOffset { get; }
        public Degree RotatePlane { get; }
        public float3 Center { get; }
        public float3 WorldCenter { get; }
        public Straight RotateNormal { get; }

        public SpaceOptions(Plane3D plane, float2 centerOffset, Degree rotatePlane)
        {
            Plane = plane;
            CenterOffset = centerOffset;
            RotatePlane = rotatePlane;
            Center = new float3(centerOffset.x, 0, centerOffset.y);
            WorldCenter = Center.ProjectOnPlane(Plane.Normal);
            RotateNormal = new Straight(Center, Float3Constants.Up + Center);
        }

        public float3 ProjectOnPlane(float3 vector3)
        {
            var rotateForward = Vector3Helper.RotateOnNormal(vector3, RotateNormal, RotatePlane);
            var result = rotateForward.ProjectOnPlane(Plane.Normal);

            return result;
        }
    }
}