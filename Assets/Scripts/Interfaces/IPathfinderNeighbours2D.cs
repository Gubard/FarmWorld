using System.Collections.Generic;
using Models;

namespace Interfaces
{
    public interface IPathfinderNeighbours2D<TOptions>
    {
        IEnumerable<PathfinderGrid2DNode<TOptions>> GetNeighbours(PathfinderGrid2DNode<TOptions> node);
    }
}