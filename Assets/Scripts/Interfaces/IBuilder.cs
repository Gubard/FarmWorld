namespace Interfaces
{
    public interface IBuilder<out TValue>
    {
        TValue Build();
    }
}