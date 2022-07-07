using System.Collections.Generic;
using Interfaces;
using Models;
using Unity.Mathematics;
using UnityEngine;

namespace Services
{
    public class RectangularPathfinderNeighbours2D : IPathfinderNeighbours2D<RectangularPathfinderGrid2DNodeOptions>
    {
        private readonly Grid2DPath<RectangularPathfinderGrid2DNodeOptions> _grid;

        public RectangularPathfinderNeighbours2D(Grid2DPath<RectangularPathfinderGrid2DNodeOptions> grid)
        {
            _grid = grid;
        }

        public IEnumerable<PathfinderGrid2DNode<RectangularPathfinderGrid2DNodeOptions>> GetNeighbours(
            PathfinderGrid2DNode<RectangularPathfinderGrid2DNodeOptions> node)
        {
            var result = new HashSet<PathfinderGrid2DNode<RectangularPathfinderGrid2DNodeOptions>>();
            AddTopIfExistsAndNotWall(node, result);
            AddBottomIfExistsAndNotWall(node, result);
            AddLeftIfExistsAndNotWall(node, result);
            AddRightIfExistsAndNotWall(node, result);
            AddTopRightIfExistsAndNotWall(node, result);
            AddTopLeftIfExistsAndNotWall(node, result);
            AddBottomRightIfExistsAndNotWall(node, result);
            AddBottomLeftIfExistsAndNotWall(node, result);

            return result;
        }

        private void AddIfExistsAndNotWall(int2 position,
            HashSet<PathfinderGrid2DNode<RectangularPathfinderGrid2DNodeOptions>> list)
        {
            if (_grid.IsEmpty(position))
            {
                return;
            }

            var node = _grid[position];

            if (node.IsWall)
            {
                return;
            }

            list.Add(node);
        }

        private void AddTopIfExistsAndNotWall(PathfinderGrid2DNode<RectangularPathfinderGrid2DNodeOptions> node,
            HashSet<PathfinderGrid2DNode<RectangularPathfinderGrid2DNodeOptions>> list)
        {
            var position = new int2(node.Position.x, node.Position.y + 1);
            AddIfExistsAndNotWall(position, list);
        }

        private void AddBottomIfExistsAndNotWall(PathfinderGrid2DNode<RectangularPathfinderGrid2DNodeOptions> node,
            HashSet<PathfinderGrid2DNode<RectangularPathfinderGrid2DNodeOptions>> list)
        {
            var position = new int2(node.Position.x, node.Position.y - 1);
            AddIfExistsAndNotWall(position, list);
        }

        private void AddLeftIfExistsAndNotWall(PathfinderGrid2DNode<RectangularPathfinderGrid2DNodeOptions> node,
            HashSet<PathfinderGrid2DNode<RectangularPathfinderGrid2DNodeOptions>> list)
        {
            var position = new int2(node.Position.x - 1, node.Position.y);
            AddIfExistsAndNotWall(position, list);
        }

        private void AddRightIfExistsAndNotWall(PathfinderGrid2DNode<RectangularPathfinderGrid2DNodeOptions> node,
            HashSet<PathfinderGrid2DNode<RectangularPathfinderGrid2DNodeOptions>> list)
        {
            var position = new int2(node.Position.x + 1, node.Position.y);
            AddIfExistsAndNotWall(position, list);
        }

        private void AddTopRightIfExistsAndNotWall(PathfinderGrid2DNode<RectangularPathfinderGrid2DNodeOptions> node,
            HashSet<PathfinderGrid2DNode<RectangularPathfinderGrid2DNodeOptions>> list)
        {
            var position = new int2(node.Position.x + 1, node.Position.y + 1);
            AddIfExistsAndNotWall(position, list);
        }

        private void AddTopLeftIfExistsAndNotWall(PathfinderGrid2DNode<RectangularPathfinderGrid2DNodeOptions> node,
            HashSet<PathfinderGrid2DNode<RectangularPathfinderGrid2DNodeOptions>> list)
        {
            var position = new int2(node.Position.x - 1, node.Position.y + 1);
            AddIfExistsAndNotWall(position, list);
        }

        private void AddBottomRightIfExistsAndNotWall(PathfinderGrid2DNode<RectangularPathfinderGrid2DNodeOptions> node,
            HashSet<PathfinderGrid2DNode<RectangularPathfinderGrid2DNodeOptions>> list)
        {
            var position = new int2(node.Position.x + 1, node.Position.y - 1);
            AddIfExistsAndNotWall(position, list);
        }

        private void AddBottomLeftIfExistsAndNotWall(PathfinderGrid2DNode<RectangularPathfinderGrid2DNodeOptions> node,
            HashSet<PathfinderGrid2DNode<RectangularPathfinderGrid2DNodeOptions>> list)
        {
            var position = new int2(node.Position.x - 1, node.Position.y - 1);
            AddIfExistsAndNotWall(position, list);
        }
    }
}