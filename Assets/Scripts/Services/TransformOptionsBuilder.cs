using Interfaces;
using Models;
using UnityEngine;

namespace Services
{
    public class TransformOptionsBuilder : BuilderBase<TransformOptions>
    {
        private Vector3 _position;
        private Vector3 _localScale;
        private Quaternion _rotation;
        private Transform _parent;

        public TransformOptionsBuilder()
        {
            _position = Vector3.zero;
            _localScale = Vector3.one;
            _rotation = Quaternion.identity;
            _parent = null;
        }

        public TransformOptionsBuilder SetPosition(Vector3 position)
        {
            _position = position;

            return this;
        }

        public TransformOptionsBuilder SetLocalScale(Vector3 localScale)
        {
            _localScale = localScale;

            return this;
        }

        public TransformOptionsBuilder SetRotation(Quaternion rotation)
        {
            _rotation = rotation;

            return this;
        }

        public TransformOptionsBuilder SetParent(Transform parent)
        {
            _parent = parent;

            return this;
        }

        public override TransformOptions Build()
        {
            return new TransformOptions(_position, _localScale, _rotation, _parent);
        }
    }
}