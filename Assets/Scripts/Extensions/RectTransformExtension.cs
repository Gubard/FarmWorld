using Models;
using UnityEngine;

namespace Extensions
{
    public static class RectTransformExtension
    {
        public static void SetOptions(this RectTransform rectTransform, RectTransformOptions options)
        {
            rectTransform.parent = options.Parent;
            rectTransform.localPosition = options.LocalPosition;
            rectTransform.localScale = options.LocalScale;
            rectTransform.localRotation = options.LocalRotation;
            rectTransform.sizeDelta = options.SizeDelta;
        }
    }
}