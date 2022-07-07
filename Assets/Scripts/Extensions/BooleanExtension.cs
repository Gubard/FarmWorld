namespace Extensions
{
    public static class BooleanExtension
    {
        public static int ToOrientation(this bool value)
        {
            return value ? 1 : -1;
        }
    }
}