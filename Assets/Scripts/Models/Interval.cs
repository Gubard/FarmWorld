namespace Models
{
    public readonly struct Interval<TValue>
    {
        public TValue Min { get; }
        public TValue Max { get; }

        public Interval(TValue min, TValue max)
        {
            Min = min;
            Max = max;
        }
    }
}