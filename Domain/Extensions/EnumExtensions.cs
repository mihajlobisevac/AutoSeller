using System;

namespace Domain.Extensions
{
    public static class EnumExtensions
    {
        public static T ToEnum<T>(this string position) => (T)Enum.Parse(typeof(T), position, false);
    }
}
