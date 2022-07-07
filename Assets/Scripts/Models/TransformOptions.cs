using UnityEngine;

namespace Models
{
    public readonly struct TransformOptions
    {
        public static readonly TransformOptions Default =
            new TransformOptions(Vector3.zero, Vector3.one, Quaternion.identity, null);

        public Vector3 Position { get; }
        public Vector3 LocalScale { get; }
        public Quaternion Rotation { get; }
        public Transform Parent { get; }

        public TransformOptions(Vector3 position, Vector3 localScale, Quaternion rotation, Transform parent)
        {
            Position = position;
            LocalScale = localScale;
            Rotation = rotation;
            Parent = parent;
        }
    }
}