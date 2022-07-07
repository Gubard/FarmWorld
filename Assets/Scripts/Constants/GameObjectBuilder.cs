using System.Collections.Generic;
using System.Linq;
using Extensions;
using Models;
using TMPro;
using UnityEngine;

namespace Constants
{
    public static class GameObjectBuilder
    {
        public static GameObject Create(string name = "Created by GameObjectBuilder")
        {
            return new GameObject(name);
        }

        public static (GameObject GameObject, Transform Transform) Create(string name, Vector3 position)
        {
            return Create(name)
                .SetupComponent<Transform>(transform =>
                {
                    transform.position = position;
                });
        }

        public static (GameObject GameObject, MeshFilter MeshFilter) Create(string name, Mesh mesh)
        {
            return Create(name)
                .SetupComponent<MeshFilter>(meshFilter =>
                {
                    meshFilter.mesh = mesh;
                });
        }

        public static (GameObject GameObject, MeshFilter MeshFilter, MeshRenderer MeshRenderer)
            Create(string name, Mesh mesh, Material material)
        {
            var baseObject = Create(name, mesh);

            var result = baseObject.GameObject.SetupComponent<MeshRenderer>(meshRenderer =>
            {
                meshRenderer.material = material;
            });

            return (baseObject.GameObject, baseObject.MeshFilter, result.Component);
        }

        public static (GameObject GameObject, Transform Transform)
            Create(string name, TransformOptions transformOptions)
        {
            return Create(name)
                .SetupComponent<Transform>(transform =>
                {
                    transform.SetOptions(transformOptions);
                });
        }

        public static (GameObject GameObject, RectTransform RectTransform)
            Create(string name, RectTransformOptions rectTransformOptions)
        {
            return Create(name)
                .SetupComponent<RectTransform>(rectTransform =>
                {
                    rectTransform.SetOptions(rectTransformOptions);
                });
        }

        public static (GameObject GameObject, Transform Transform, MeshFilter MeshFilter)
            Create(string name, TransformOptions transformOptions, Mesh mesh)
        {
            var baseObject = Create(name, transformOptions);

            var result = baseObject.GameObject.SetupComponent<MeshFilter>(meshFilter =>
            {
                meshFilter.mesh = mesh;
            });

            return (baseObject.GameObject, baseObject.Transform, result.Component);
        }

        public static (GameObject GameObject, Transform Transform, MeshFilter MeshFilter, MeshRenderer MeshRenderer)
            Create(string name, TransformOptions transformOptions, Mesh mesh, Material material)
        {
            var baseObject = Create(name, transformOptions, mesh);

            var result = baseObject.GameObject.SetupComponent<MeshRenderer>(meshRenderer =>
            {
                meshRenderer.material = material;
            });

            return (baseObject.GameObject, baseObject.Transform, baseObject.MeshFilter, result.Component);
        }

        public static (GameObject GameObject, Transform Transform, MeshRenderer MeshRenderer)
            Create(string name, TransformOptions transformOptions, Material material)
        {
            var baseObject = Create(name, transformOptions);

            var result = baseObject.GameObject.SetupComponent<MeshRenderer>(meshRenderer =>
            {
                meshRenderer.material = material;
            });

            return (baseObject.GameObject, baseObject.Transform, result.Component);
        }

        public static (GameObject GameObject, Transform Transform, MeshFilter MeshFilter)
            Create(string name, Vector3 position, Mesh mesh)
        {
            var baseObject = Create(name, position);

            var result = baseObject.GameObject.SetupComponent<MeshFilter>(meshFilter =>
            {
                meshFilter.mesh = mesh;
            });

            return (baseObject.GameObject, baseObject.Transform, result.Component);
        }

        public static (GameObject GameObject, Transform Transform, MeshFilter MeshFilter, MeshRenderer MeshRenderer)
            Create(string name, Vector3 position, Mesh mesh, Material material)
        {
            var baseObject = Create(name, position, mesh);

            var result = baseObject.GameObject.SetupComponent<MeshRenderer>(meshRenderer =>
            {
                meshRenderer.material = material;
            });

            return (baseObject.GameObject, baseObject.Transform, baseObject.MeshFilter, result.Component);
        }

        public static (GameObject GameObject, Transform Transform, MeshFilter MeshFilter, MeshRenderer MeshRenderer)
            Create(string name, Vector3 position, Mesh mesh, Material[] materials)
        {
            var baseObject = Create(name, position, mesh);

            var result = baseObject.GameObject.SetupComponent<MeshRenderer>(meshRenderer =>
            {
                meshRenderer.materials = materials;
            });

            return (baseObject.GameObject, baseObject.Transform, baseObject.MeshFilter, result.Component);
        }

        public static (GameObject GameObject, Transform Transform, MeshFilter MeshFilter, MeshRenderer MeshRenderer)
            Create(string name, Vector3 position, Mesh mesh, IEnumerable<Material> materials)
        {
            var baseObject = Create(name, position, mesh);

            var result = baseObject.GameObject.SetupComponent<MeshRenderer>(meshRenderer =>
            {
                meshRenderer.materials = materials.ToArray();
            });

            return (baseObject.GameObject, baseObject.Transform, baseObject.MeshFilter, result.Component);
        }

        public static (GameObject GameObject, Transform Transform, MeshRenderer MeshRenderer)
            Create(string name, Vector3 position, Material material)
        {
            var baseObecjt = Create(name, position);

            var result = baseObecjt.GameObject.SetupComponent<MeshRenderer>(meshRenderer =>
            {
                meshRenderer.material = material;
            });

            return (baseObecjt.GameObject, baseObecjt.Transform, result.Component);
        }

        public static (GameObject GameObject, Transform Transform, MeshRenderer MeshRenderer)
            Create(string name, Vector3 position, IEnumerable<Material> materials)
        {
            var baseObject = Create(name, position);

            var result = baseObject.GameObject.SetupComponent<MeshRenderer>(meshRenderer =>
            {
                meshRenderer.materials = materials.ToArray();
            });

            return (baseObject.GameObject, baseObject.Transform, result.Component);
        }

        public static (GameObject GameObject, Transform Transform, MeshRenderer MeshRenderer)
            Create(string name, Vector3 position, Material[] materials)
        {
            var baseObject = Create(name, position);

            var result = baseObject.GameObject.SetupComponent<MeshRenderer>(meshRenderer =>
            {
                meshRenderer.materials = materials;
            });

            return (baseObject.GameObject, baseObject.Transform, result.Component);
        }

        public static (GameObject GameObject, Transform Transform, MeshFilter MeshFilter)
            CreateCube(string name, Vector3 position)
        {
            return Create(name, position, MeshConstants.Cube);
        }

        public static (GameObject GameObject, Transform Transform, MeshFilter MeshFilter, MeshRenderer MeshRenderer)
            CreateCube(string name, Vector3 position, Material material)
        {
            return Create(name, position, MeshConstants.Cube, material);
        }

        public static (GameObject GameObject, Transform Transform, MeshFilter MeshFilter, MeshRenderer MeshRenderer)
            CreateCube(string name, Vector3 position, Material[] materials)
        {
            return Create(name, position, MeshConstants.Cube, materials);
        }

        public static (GameObject GameObject, Transform Transform, MeshFilter MeshFilter, MeshRenderer MeshRenderer)
            CreateCube(string name, Vector3 position, IEnumerable<Material> materials)
        {
            return Create(name, position, MeshConstants.Cube, materials);
        }

        public static (GameObject GameObject, MeshFilter MeshFilter) CreateQuad()
        {
            return Create("Quad", MeshConstants.Quad);
        }

        public static (GameObject GameObject, MeshFilter MeshFilter, MeshRenderer MeshRenderer)
            CreateQuad(Material material)
        {
            return Create("Quad", MeshConstants.Quad, material);
        }

        public static (GameObject GameObject, MeshFilter MeshFilter, MeshRenderer MeshRenderer)
            CreateQuad(string name, Material material)
        {
            return Create(name, MeshConstants.Quad, material);
        }

        public static (GameObject GameObject, Transform Transform, MeshFilter MeshFilter, MeshRenderer MeshRenderer)
            CreateQuad(string name, TransformOptions transformOptions, Material material)
        {
            return Create(name, transformOptions, MeshConstants.Quad, material);
        }

        public static (GameObject GameObject, Transform Transform, MeshFilter MeshFilter)
            CreateCube(TransformOptions transformOptions)
        {
            return Create("Cube", transformOptions, MeshConstants.Cube);
        }

        public static (GameObject GameObject, Transform Transform, MeshFilter MeshFilter, MeshRenderer MeshRenderer)
            CreateCube(TransformOptions transformOptions, Material material)
        {
            return Create("Cube", transformOptions, MeshConstants.Cube, material);
        }

        public static (GameObject GameObject, Transform Transform, MeshFilter MeshFilter, MeshRenderer MeshRenderer)
            CreateCube(string name, TransformOptions transformOptions, Material material)
        {
            return Create(name, transformOptions, MeshConstants.Cube, material);
        }

        public static (GameObject GameObject, Transform Transform, MeshFilter MeshFilter) CreateCube(Vector3 position)
        {
            return Create("Cube", position, MeshConstants.Cube);
        }

        public static (GameObject GameObject, Transform Transform, MeshFilter MeshFilter, MeshRenderer MeshRenderer)
            CreateCube(Vector3 position, Material material)
        {
            return Create("Cube", position, MeshConstants.Cube, material);
        }

        public static (GameObject GameObject, Transform Transform, MeshFilter MeshFilter, MeshRenderer MeshRenderer)
            CreateCube(Vector3 position, Material[] materials)
        {
            return Create("Cube", position, MeshConstants.Cube, materials);
        }

        public static (GameObject GameObject, Transform Transform, MeshFilter MeshFilter, MeshRenderer MeshRenderer)
            CreateCube(Vector3 position, IEnumerable<Material> materials)
        {
            return Create("Cube", position, MeshConstants.Cube, materials);
        }

        public static (GameObject GameObject, TextMeshPro TextMeshPro) CreateTextMeshPro(string name, string text)
        {
            return Create(name)
                .SetupComponent<TextMeshPro>(textMeshPro =>
                {
                    textMeshPro.text = text;
                });
        }

        public static (GameObject GameObject, Transform Transform, TextMeshPro TextMeshPro)
            CreateTextMeshPro(TransformOptions transformOptions, string text)
        {
            var baseObject = Create("TextMeshPro", transformOptions);

            var result = baseObject.GameObject.SetupComponent<TextMeshPro>(textMeshPro =>
            {
                textMeshPro.text = text;
            });

            return (baseObject.GameObject, baseObject.Transform, result.Component);
        }

        public static (GameObject GameObject, Transform Transform, TextMeshPro TextMeshPro)
            CreateTextMeshPro(string name, TransformOptions transformOptions, string text)
        {
            var baseObject = Create(name, transformOptions);

            var result = baseObject.GameObject.SetupComponent<TextMeshPro>(textMeshPro =>
            {
                textMeshPro.text = text;
            });

            return (baseObject.GameObject, baseObject.Transform, result.Component);
        }

        public static (GameObject GameObject, RectTransform RectTransform, TextMeshPro TextMeshPro)
            CreateTextMeshPro(string name,
                RectTransformOptions rectTransformOptions,
                TextMeshProOptions textMeshProOptions)
        {
            var baseObject = Create(name, rectTransformOptions);

            var result = baseObject.GameObject.SetupComponent<TextMeshPro>(textMeshPro =>
            {
                textMeshPro.SetOptions(textMeshProOptions);
            });

            return (baseObject.GameObject, baseObject.RectTransform, result.Component);
        }

        public static (GameObject GameObject, Transform Transform, Camera Camera)
            CreateCamera(string name, TransformOptions transformOptions, CameraOptions cameraOptions)
        {
            var baseObject = Create(name, transformOptions);

            var result = baseObject.GameObject.SetupComponent<Camera>(camera =>
            {
                camera.SetOptions(cameraOptions);
            });

            return (baseObject.GameObject, baseObject.Transform, result.Component);

            ;
        }
    }
}