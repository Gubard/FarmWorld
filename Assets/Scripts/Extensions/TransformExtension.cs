using Models;
using UnityEngine;

namespace Extensions
{
    public static class TransformExtension
    {
        public static void SetOptions(this Transform transform, TransformOptions transformOptions)
        {
            transform.parent = transformOptions.Parent;
            transform.position = transformOptions.Position;
            transform.rotation = transformOptions.Rotation;
            transform.localScale = transformOptions.LocalScale;
        }
    }
}