using Interfaces;
using Models;

namespace Extensions
{
    public static class StructExtension
    {
        public static SourceStructItem<TStruct> ToSourceItem<TStruct>(this TStruct @struct) where TStruct : struct
        {
            return @struct;
        }

        public static SourceStructItem<TStruct> BuilderToSourceItem<TStruct>(this IBuilder<TStruct> @struct)
            where TStruct : struct
        {
            return new SourceStructItem<TStruct>(@struct);
        }
    }
}