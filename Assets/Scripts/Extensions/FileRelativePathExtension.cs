using Models;
using UnityEditor;
using UnityEngine;

namespace Extensions
{
    public static class FileRelativePathExtension
    {
        public static TObject LoadAssetAtPath<TObject>(this FileRelativePath path) where TObject : Object
        {
            return AssetDatabase.LoadAssetAtPath<TObject>(path.ToString());
        }
    }
}