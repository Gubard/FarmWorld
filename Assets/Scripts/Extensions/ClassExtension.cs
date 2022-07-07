using Interfaces;
using Models;

namespace Extensions
{
    public static class ClassExtension
    {
        public static SourceItem<TClass> ToSourceItem<TClass>(this TClass @class) where TClass : class
        {
            return @class;
        }

        public static SourceItem<TClass> BuilderToSourceItem<TClass>(this IBuilder<TClass> @class) where TClass : class
        {
            return new SourceItem<TClass>(@class);
        }
    }
}