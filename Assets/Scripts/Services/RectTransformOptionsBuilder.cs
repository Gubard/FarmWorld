using Models;
using UnityEngine;

namespace Services
{
    public class RectTransformOptionsBuilder
    {
        private Vector3 _localPosition;
        private Vector3 _localScale;
        private Quaternion _localRotation;
        private Vector2 _sizeDelta;
        private Transform _parent;

        public RectTransformOptionsBuilder()
        {
            _localPosition = Vector3.zero;
            _localScale = Vector3.one;
            _localRotation = Quaternion.identity;
            _sizeDelta = Vector2.one;
            _parent = null;
        }

        public RectTransformOptionsBuilder SetLocalPosition(Vector3 localPosition)
        {
            _localPosition = localPosition;

            return this;
        }

        public RectTransformOptionsBuilder SetLocalScale(Vector3 localScale)
        {
            _localScale = localScale;

            return this;
        }

        public RectTransformOptionsBuilder SetLocalRotation(Quaternion localRotation)
        {
            _localRotation = localRotation;

            return this;
        }

        public RectTransformOptionsBuilder SetSizeDelta(Vector2 sizeDelta)
        {
            _sizeDelta = sizeDelta;

            return this;
        }

        public RectTransformOptionsBuilder SetParent(Transform parent)
        {
            _parent = parent;

            return this;
        }

        public RectTransformOptions Build()
        {
            return new RectTransformOptions(_localPosition, _localScale, _localRotation, _sizeDelta, _parent);
        }
    }
}