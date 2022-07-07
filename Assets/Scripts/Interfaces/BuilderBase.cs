namespace Interfaces
{
    public abstract class BuilderBase<TValue> : IBuilder<TValue>
    {
        public abstract TValue Build();
    }
}