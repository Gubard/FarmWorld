using UnityEngine;
using Space = Models.Space;

namespace Extensions
{
    public static class SpaceExtension
    {
        public static Vector3 GetMouseProject(this Space space, Camera camera)
        {
            var worldPosition = camera.GetMouseWorldPosition();

            return space.ProjectOnPlane(worldPosition);
        }
    }
}