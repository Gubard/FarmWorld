using System;
using System.Linq;

namespace Extensions
{
    public static class EnumExtension
    {
        public static bool IsInValues<TEnum>(this TEnum @enum) where TEnum : Enum
        {
            return Enum.GetValues(typeof(TEnum)).OfType<TEnum>().Contains(@enum);
        }

        public static TEnum ThrowIfNotIsInValues<TEnum>(this TEnum @enum) where TEnum : Enum
        {
            if (@enum.IsInValues())
            {
                return @enum;
            }

            throw new Exception();
        }
    }
}