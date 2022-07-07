using Interfaces;
using Models;
using UnityEngine;

namespace Services
{
    public class RectangularPathfinderDistance2D : IPathfinderDistance2D<RectangularPathfinderGrid2DNodeOptions>
    {
        public static readonly RectangularPathfinderDistance2D Default = new RectangularPathfinderDistance2D();

        public float Distance(PathfinderGrid2DNode<RectangularPathfinderGrid2DNodeOptions> start,
            PathfinderGrid2DNode<RectangularPathfinderGrid2DNodeOptions> end)
        {
            var xDistance = Mathf.Abs(start.Position.x - end.Position.x);
            var yDistance = Mathf.Abs(start.Position.y - end.Position.y);
            var remaining =  Mathf.Abs(xDistance - yDistance);
            var min = Mathf.Min(xDistance, remaining);

            return start.Options.MoveDiagonalCost * min + start.Options.MoveStraightCost * remaining;
        }
    }
}