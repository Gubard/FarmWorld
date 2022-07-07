using Models;

namespace Interfaces
{
    public interface IPathfinderDistance2D<TOptions>
    {
        float Distance(PathfinderGrid2DNode<TOptions> start, PathfinderGrid2DNode<TOptions> end);
    }
}