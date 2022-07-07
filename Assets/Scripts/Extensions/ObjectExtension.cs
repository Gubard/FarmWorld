using System;
using System.Collections.Generic;

namespace Extensions
{
    public static class ObjectExtension
    {
        public static TObject ThrowIfEquals<TObject>(this TObject x, object y)
        {
            if (x.Equals(y))
            {
                throw new Exception();
            }

            return x;
        }

        public static TObject AddToCollection<TObject>(this TObject item, ICollection<TObject> collection)
        {
            collection.Add(item);

            return item;
        }

        public static TObject To<TObject>(this object item)
        {
            return (TObject)item;
        }
    }
}