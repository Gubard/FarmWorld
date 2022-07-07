using UnityEngine;

namespace Models
{
    public readonly struct Orientation
    {
        public Vector2Int Start { get; }
        public Vector2Int End { get; }
        public bool IsBottom { get; }
        public bool IsTop { get; }
        public bool IsLeft { get; }
        public bool IsRight { get; }
        public bool IsTopRight { get; }
        public bool IsTopLeft { get; }
        public bool IsBottomRight { get; }
        public bool IsBottomLeft { get; }

        public Orientation(Vector2Int start, Vector2Int end)
        {
            Start = start;
            End = end;
            IsTop = start.y < end.y;
            IsBottom = start.y > end.y;
            IsLeft = start.x > end.x;
            IsRight = start.x < end.x;
            IsTopRight = IsTop && IsRight;
            IsTopLeft = IsTop && IsLeft;
            IsBottomRight = IsBottom && IsRight;
            IsBottomLeft = IsBottom && IsLeft;
        }
    }
}