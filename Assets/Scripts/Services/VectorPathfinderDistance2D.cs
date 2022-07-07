using Interfaces;
using Models;
using Unity.Mathematics;
using UnityEngine;

namespace Services
{
    public class VectorPathfinderDistance2D<TOptions> : IPathfinderDistance2D<TOptions>
    {
        public static readonly VectorPathfinderDistance2D<TOptions> Default = 
            new VectorPathfinderDistance2D<TOptions>();

        public float Distance(PathfinderGrid2DNode<TOptions> start, PathfinderGrid2DNode<TOptions> end)
        {
            return math.distance(start.Position, end.Position);
        }
    }
}