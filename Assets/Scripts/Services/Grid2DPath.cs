using System.Collections;
using System.Collections.Generic;
using Models;
using Unity.Mathematics;

namespace Services
{
    public class Grid2DPath<TOptions> : IEnumerable<KeyValuePair<int2, PathfinderGrid2DNode<TOptions>>>
    {
        private readonly Dictionary<int2, PathfinderGrid2DNode<TOptions>> _grid;

        public PathfinderGrid2DNode<TOptions> this[int2 position]
        {
            get => _grid[position];
            set => _grid[position] = value;
        }

        public Grid2DPath()
        {
            _grid = new Dictionary<int2, PathfinderGrid2DNode<TOptions>>();
        }

        public bool IsEmpty(int2 position)
        {
            return !_grid.ContainsKey(position);
        }

        public IEnumerator<KeyValuePair<int2, PathfinderGrid2DNode<TOptions>>> GetEnumerator()
        {
            return _grid.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}