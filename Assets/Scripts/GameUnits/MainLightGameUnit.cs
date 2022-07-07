using Constants;
using Extensions;
using Interfaces;
using Models;
using UnityEngine;

namespace GameUnits
{
    public class MainLightGameUnit : IGameUnit
    {
        private readonly string _name;
        private readonly SourceStructItem<TransformOptions> _transformOptions;

        // private const string Name = "Main Light";
        // private static readonly Quaternion Rotation = Quaternion.AngleAxis(-90, Vector3.left);
        // private static readonly Vector3 Position = 10f.ToVector3Y();
        //
        // private static readonly TransformOptions TransformOptions = new TransformOptionsBuilder().SetRotation(Rotation)
        //     .SetPosition(Position)
        //     .Build();
        public MainLightGameUnit(string name, SourceStructItem<TransformOptions> transformOptions)
        {
            _name = name;
            _transformOptions = transformOptions;
        }

        public GameObject Create()
        {
            return GameObjectBuilder.Create(_name, _transformOptions.Value)
                .GameObject
                .SetupComponent<Light>(light =>
                {
                    light.type = LightType.Directional;
                })
                .GameObject;
        }
    }
}