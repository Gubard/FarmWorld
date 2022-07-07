namespace Models
{
    public readonly struct Degree
    {
        public static readonly Degree Zero = new Degree(0);
        public float Value { get; }

        public Degree(float value)
        {
            Value = value;
        }
    }
}