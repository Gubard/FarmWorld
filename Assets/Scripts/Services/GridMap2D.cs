using System.Collections;
using System.Collections.Generic;
using Interfaces;
using Unity.Mathematics;

namespace Services
{
    public class GridMap2D<TItem> : IEnumerable<KeyValuePair<int2, TItem>>
    {
        private readonly Dictionary<int2, TItem> _map;

        public TItem this[int2 position]
        {
            get => _map[position];
            set => _map[position] = value;
        }

        public ICellService CellService { get; }

        public GridMap2D(ICellService cellService)
        {
            CellService = cellService;
            _map = new Dictionary<int2, TItem>();
        }

        public bool IsEmpty(int2 position)
        {
            return !_map.ContainsKey(position);
        }

        public bool TryGet(int2 position, out TItem item)
        {
            if (IsEmpty(position))
            {
                item = default;

                return false;
            }

            item = _map[position];

            return true;
        }

        public void Add(int2 position, TItem item)
        {
            _map.Add(position, item);
        }

        public void Remove(int2 position)
        {
            _map.Remove(position);
        }

        public IEnumerator<KeyValuePair<int2, TItem>> GetEnumerator()
        {
            return _map.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}