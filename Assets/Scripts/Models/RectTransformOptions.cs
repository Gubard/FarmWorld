using UnityEngine;

namespace Models
{
    public readonly struct RectTransformOptions
    {
        public static readonly RectTransformOptions Default =
            new RectTransformOptions(Vector3.zero, Vector3.one, Quaternion.identity, Vector2.one, null);

        public Vector3 LocalPosition { get; }
        public Vector3 LocalScale { get; }
        public Quaternion LocalRotation { get; }
        public Vector2 SizeDelta { get; }
        public Transform Parent { get; }

        public RectTransformOptions(Vector3 localPosition,
            Vector3 localScale,
            Quaternion localRotation,
            Vector2 sizeDelta,
            Transform parent)
        {
            LocalPosition = localPosition;
            LocalScale = localScale;
            LocalRotation = localRotation;
            SizeDelta = sizeDelta;
            Parent = parent;
        }
    }
}