using Models;

namespace Extensions
{
    public static class IntervalExtension
    {
        public static bool Contains(this Interval<int> interval, int value)
        {
            return interval.Min >= value && interval.Max <= value;
        }
    }
}