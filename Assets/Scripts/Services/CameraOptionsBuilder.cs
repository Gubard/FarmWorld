using Interfaces;
using Models;

namespace Services
{
    public class CameraOptionsBuilder : BuilderBase<CameraOptions>
    {
        private bool _orthographic;

        public CameraOptionsBuilder()
        {
            _orthographic = false;
        }

        public CameraOptionsBuilder SetOrthographic(bool orthographic)
        {
            _orthographic = orthographic;

            return this;
        }

        public override CameraOptions Build()
        {
            return new CameraOptions(_orthographic);
        }
    }
}