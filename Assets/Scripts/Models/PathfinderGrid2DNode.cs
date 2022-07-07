using Unity.Mathematics;

namespace Models
{
    public class PathfinderGrid2DNode<TOptions>
    {
        public int2 Position { get; }
        public TOptions Options { get; }
        public float GCost { get; set; } = float.MaxValue;
        public float HCost { get; set; }
        public float FCost => GCost + HCost;
        public bool IsWall { get; }
        public PathfinderGrid2DNode<TOptions> Parent { get; set; }

        public PathfinderGrid2DNode(int2 position, TOptions options, bool isWall)
        {
            Position = position;
            Options = options;
            IsWall = isWall;
        }

        public void Clear()
        {
            GCost = int.MaxValue;
            HCost = 0;
            Parent = null;
        }
    }
}