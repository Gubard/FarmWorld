using System.Collections.Generic;
using Extensions;
using UnityEngine;

namespace Services
{
    public class ListGameObject : List<GameObject>
    {
        public ListGameObject(IEnumerable<GameObject> objects) : base(objects)
        {
        }

        public ListGameObject(params GameObject[] objects) : base(objects)
        {
        }

        public void DestroyAll()
        {
            foreach (var item in this)
            {
                item.Destroy();
            }
        }

        public void DestroyAllAndClear()
        {
            DestroyAll();
            Clear();
        }
    }
}