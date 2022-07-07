namespace Models
{
    public class Options<TValue, TOption>
    {
        public TValue Value { get; }
        public TOption Option { get; }

        public Options(TValue value, TOption option)
        {
            Value = value;
            Option = option;
        }
    }
}