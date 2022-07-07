namespace Models
{
    public readonly struct CameraOptions
    {
        public bool Orthographic { get; }

        public CameraOptions(bool orthographic)
        {
            Orthographic = orthographic;
        }
    }
}