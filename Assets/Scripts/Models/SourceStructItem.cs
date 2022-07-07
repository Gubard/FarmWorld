using Interfaces;

namespace Models
{
    public class SourceStructItem<TValue> where TValue : struct
    {
        private readonly TValue? _value;
        private readonly IBuilder<TValue> _builder;

        public SourceStructItem(TValue? value)
        {
            _value = value;
        }

        public SourceStructItem(IBuilder<TValue> builder)
        {
            _builder = builder;
        }

        public TValue Value => _value ?? _builder.Build();

        public static implicit operator SourceStructItem<TValue>(TValue value)
        {
            return new SourceStructItem<TValue>(value);
        }

        public static implicit operator SourceStructItem<TValue>(BuilderBase<TValue> builder)
        {
            return new SourceStructItem<TValue>(builder);
        }
    }
}