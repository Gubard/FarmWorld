using Constants;
using Interfaces;
using Models;
using UnityEngine;

namespace GameUnits
{
    public class MainCameraGameUnit : IGameUnit
    {
        private readonly string _name;
        private readonly SourceStructItem<TransformOptions> _transformOptions;
        private readonly SourceStructItem<CameraOptions> _cameraOptions;

        public MainCameraGameUnit(string name,
            SourceStructItem<TransformOptions> transformOptions,
            SourceStructItem<CameraOptions> cameraOptions)
        {
            _name = name;
            _transformOptions = transformOptions;
            _cameraOptions = cameraOptions;
        }

        public GameObject Create()
        {
            return GameObjectBuilder.CreateCamera(_name, _transformOptions.Value, _cameraOptions.Value).GameObject;
        }
    }
}