using System.Collections.Generic;
using Models;
using Services;

namespace Interfaces
{
    public interface IPathfinderTaskGrid2D<TOptions>
    {
        bool IsFinished { get; }
        bool IsFound { get; }
        Grid2DPath<TOptions> Grid { get; }
        IEnumerable<PathfinderGrid2DNode<TOptions>> Path { get; }

        bool MoveNext();
    }
}