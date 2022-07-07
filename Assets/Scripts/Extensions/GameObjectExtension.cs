using System;
using Models;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Extensions
{
    public static class GameObjectExtension
    {
        public static void Destroy(this GameObject gameObject)
        {
            Object.Destroy(gameObject);
        }

        public static (GameObject ParentGameObject,
            GameObject ChildGameObject,
            Transform ParentTransform,
            Transform ChildTransform) AddChild(this GameObject parent, GameObject child)
        {
            (GameObject GameObject, Transform Component) childObject = default;

            var parentObject = parent.SetupComponent<Transform>(parentTransform =>
            {
                childObject = child.SetupComponent<Transform>(childTransform =>
                {
                    childTransform.parent = parentTransform;
                });
            });

            return (parentObject.GameObject, childObject.GameObject, parentObject.Component, childObject.Component);
        }

        public static (GameObject GameObject, TComponent Component) SetupComponent<TComponent>(
            this GameObject gameObject,
            Action<TComponent> setup) where TComponent : Component
        {
            if (!gameObject.TryGetComponent(out TComponent component))
            {
                component = gameObject.AddComponent<TComponent>();
            }

            setup.Invoke(component);

            return (gameObject, component);
        }

        public static (GameObject GameObject, TComponent Component) SetupComponent<TComponent>(
            this GameObject gameObject) where TComponent : Component
        {
            if (!gameObject.TryGetComponent(out TComponent component))
            {
                component = gameObject.AddComponent<TComponent>();
            }

            return (gameObject, component);
        }

        public static GameObject DestroyComponent<TComponent>(this GameObject gameObject) where TComponent : Component
        {
            if (gameObject.TryGetComponent(out TComponent component))
            {
                Object.Destroy(component);
            }

            return gameObject;
        }
    }
}