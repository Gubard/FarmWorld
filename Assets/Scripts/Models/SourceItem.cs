using Interfaces;

namespace Models
{
    public class SourceItem<TValue> where TValue : class
    {
        private readonly TValue _value;
        private readonly IBuilder<TValue> _builder;

        public SourceItem(TValue value)
        {
            _value = value;
        }

        public SourceItem(IBuilder<TValue> builder)
        {
            _builder = builder;
        }

        public TValue Value => _value ?? _builder.Build();
        
        public static implicit operator SourceItem<TValue>(TValue value)
        {
            return new SourceItem<TValue>(value);
        }

        public static implicit operator SourceItem<TValue>(BuilderBase<TValue> builder)
        {
            return new SourceItem<TValue>(builder);
        }
    }
}