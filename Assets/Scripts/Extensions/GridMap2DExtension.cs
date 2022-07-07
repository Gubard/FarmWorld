using System;
using System.Collections.Generic;
using Models;
using Services;
using Unity.Mathematics;

namespace Extensions
{
    public static class GridMap2DExtension
    {
        public static Grid2DPath<TOptions> GetGrid2DPath<TOptions, TItem>(this GridMap2D<TItem> map,
            Func<KeyValuePair<int2, TItem>, PathfinderGrid2DNode<TOptions>> itemCreate,
            Func<int2, PathfinderGrid2DNode<TOptions>> emptyCreate)
        {
            var maxY = int.MinValue;
            var maxX = int.MinValue;
            var minY = int.MaxValue;
            var minX = int.MaxValue;
            var path = new Grid2DPath<TOptions>();
            
            foreach (var item in map)
            {
                if (maxX < item.Key.x)
                {
                    maxX = item.Key.x;
                }
                
                if (minX > item.Key.x)
                {
                    minX = item.Key.x;
                }
                
                if (maxY < item.Key.y)
                {
                    maxY = item.Key.y;
                }
                
                if (minY > item.Key.y)
                {
                    minY = item.Key.y;
                }
                
                path[item.Key] = itemCreate.Invoke(item);
            }

            for (var x = minX; x <= maxX; x++)
            {
                for (var y = minY; y <= maxY; y++)
                {
                    var position = new int2(x, y);

                    if (path.IsEmpty(position))
                    {
                        path[position] = emptyCreate.Invoke(position);
                    }
                }
            }

            return path;
        }
    }
}