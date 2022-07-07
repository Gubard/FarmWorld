namespace Models
{
    public class RectangularPathfinderGrid2DNodeOptions
    {
        public const int DefaultMoveDiagonalCost = 14;
        public const int DefaultMoveStraightCost = 10;

        public static readonly RectangularPathfinderGrid2DNodeOptions Default =
            new RectangularPathfinderGrid2DNodeOptions
            {
                MoveStraightCost = DefaultMoveStraightCost,
                MoveDiagonalCost = DefaultMoveDiagonalCost
            };

        public int MoveDiagonalCost { get; set; }
        public int MoveStraightCost { get; set; }
    }
}