using System.Collections.Generic;
using System.Linq;
using Extensions;
using Interfaces;
using Models;
using Unity.Mathematics;
using UnityEngine;

namespace Services
{
    public static class PathfinderGrid2D
    {
        public static PathfinderTaskGrid2D<RectangularPathfinderGrid2DNodeOptions> CreateRectangularPathfinder(
            Grid2DPath<RectangularPathfinderGrid2DNodeOptions> grid,
            int2 start,
            int2 end)
        {
            return new PathfinderTaskGrid2D<RectangularPathfinderGrid2DNodeOptions>(grid,
                RectangularPathfinderDistance2D.Default,
                new RectangularPathfinderNeighbours2D(grid),
                start,
                end);
        }
    }

    public class PathfinderTaskGrid2D<TOptions> : IPathfinderTaskGrid2D<TOptions>
    {
        private readonly IPathfinderDistance2D<TOptions> _distance;
        private readonly IPathfinderNeighbours2D<TOptions> _neighbours;
        private readonly HashSet<PathfinderGrid2DNode<TOptions>> _openList;
        private readonly HashSet<PathfinderGrid2DNode<TOptions>> _closeList;
        private readonly PathfinderGrid2DNode<TOptions> _startNode;
        private readonly PathfinderGrid2DNode<TOptions> _endNode;

        public Grid2DPath<TOptions> Grid { get; }
        public bool IsFinished { get; private set; }
        public bool IsFound => !(Path is null);
        public IEnumerable<PathfinderGrid2DNode<TOptions>> Path { get; private set; }
        private int2 Start { get; }
        private int2 End { get; }
        public IEnumerable<PathfinderGrid2DNode<TOptions>> OpenList => _openList;
        public IEnumerable<PathfinderGrid2DNode<TOptions>> CloseList => _closeList;

        public PathfinderTaskGrid2D(Grid2DPath<TOptions> grid,
            IPathfinderDistance2D<TOptions> distance,
            IPathfinderNeighbours2D<TOptions> neighbours,
            int2 start,
            int2 end)
        {
            _closeList = new HashSet<PathfinderGrid2DNode<TOptions>>();
            _openList = new HashSet<PathfinderGrid2DNode<TOptions>>();
            Start = start;
            End = end;
            Grid = grid.ThrowIfEquals(nameof(grid));
            _distance = distance.ThrowIfEquals(nameof(distance));
            _neighbours = neighbours.ThrowIfEquals(nameof(neighbours));
            _startNode = Grid[Start];
            _endNode = Grid[End];
            _startNode.GCost = 0;
            _startNode.HCost = _distance.Distance(_startNode, _endNode);
            _openList.Add(_startNode);

            if (!Start.Equals(End))
            {
                return;
            }

            IsFinished = true;

            Path = new[]
            {
                _startNode
            };
        }

        public bool MoveNext()
        {
            if (IsFinished)
            {
                return false;
            }

            if (_openList.Count > 0)
            {
                var currentNode = GetLowestFCostNode(_openList);

                if (currentNode == _endNode)
                {
                    Path = CalculatePath(_endNode);
                    IsFinished = true;

                    return false;
                }

                _openList.Remove(currentNode);
                _closeList.Add(currentNode);

                foreach (var neighbour in _neighbours.GetNeighbours(currentNode))
                {
                    if (_closeList.Contains(neighbour))
                    {
                        continue;
                    }

                    var gCost = currentNode.GCost + _distance.Distance(currentNode, neighbour);

                    if (gCost >= neighbour.GCost)
                    {
                        continue;
                    }

                    neighbour.Parent = currentNode;
                    neighbour.GCost = gCost;
                    neighbour.HCost = _distance.Distance(neighbour, _endNode);

                    if (!_openList.Contains(neighbour))
                    {
                        _openList.Add(neighbour);
                    }
                }

                return true;
            }

            IsFinished = true;

            return false;
        }

        private IEnumerable<PathfinderGrid2DNode<TOptions>> CalculatePath(PathfinderGrid2DNode<TOptions> node)
        {
            var path = new List<PathfinderGrid2DNode<TOptions>>
            {
                node
            };

            var currentNode = node;

            while (!(currentNode.Parent is null))
            {
                path.Add(currentNode.Parent);
                currentNode = currentNode.Parent;
            }

            path.Reverse();

            return path;
        }

        private PathfinderGrid2DNode<TOptions> GetLowestFCostNode(HashSet<PathfinderGrid2DNode<TOptions>> nodes)
        {
            var result = nodes.First();

            foreach (var node in nodes)
            {
                if (node.FCost < result.FCost)
                {
                    result = node;
                }
            }

            return result;
        }

        private void Clear()
        {
            _closeList.Clear();
            _openList.Clear();
            IsFinished = false;
            Path = null;

            foreach (var item in Grid)
            {
                item.Value.Clear();
            }
        }
    }
}