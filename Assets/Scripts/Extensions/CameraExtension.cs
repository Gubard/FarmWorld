using Models;
using UnityEngine;

namespace Extensions
{
    public static class CameraExtension
    {
        public static Vector3 GetMouseWorldPosition(this Camera camera)
        {
            var mousePosition = Input.mousePosition;

            return camera.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, 10f));
        }
        
        public static Ray GetMouseRay(this Camera camera)
        {
            return camera.ScreenPointToRay(Input.mousePosition);
        }
        
        public static void SetOptions(this Camera camera, CameraOptions options)
        {
            camera.orthographic = options.Orthographic;
        }
    }
}