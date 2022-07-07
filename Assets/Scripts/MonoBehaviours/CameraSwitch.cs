using UnityEngine;

namespace MonoBehaviours
{
    public class CameraSwitch : MonoBehaviour
    {
        private int _currentIndexCamera;

        [SerializeField]
        private Camera[] _cameras;

        public Camera CurrentCamera => _cameras[_currentIndexCamera];

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                _currentIndexCamera = PreviousIndex();
                TurnOn(_cameras[_currentIndexCamera]);
            }

            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                _currentIndexCamera = NextIndex();
                TurnOn(_cameras[_currentIndexCamera]);
            }
        }

        private void TurnOn(Camera currentCamera)
        {
            DisableCameras();
            Camera.SetupCurrent(currentCamera);
            currentCamera.enabled = true;
        }
        
        private void DisableCameras()
        {
            foreach (var currentCamera in _cameras)
            {
                currentCamera.enabled = false;
            }
        }

        private int NextIndex()
        {
            var result = _currentIndexCamera + 1;

            if (result >= _cameras.Length)
            {
                return 0;
            }

            return result;
        }

        private int PreviousIndex()
        {
            var result = _currentIndexCamera - 1;

            if (result < 0)
            {
                return _cameras.Length - 1;
            }

            return result;
        }
    }
}