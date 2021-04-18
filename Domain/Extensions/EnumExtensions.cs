using System;
using System.Collections.Generic;

namespace Domain.Extensions
{
    public static class EnumExtensions
    {
        public static T ToEnum<T>(this string enumName) => (T)Enum.Parse(typeof(T), enumName, false);

        public static T[] ToEnumArray<T>(this string[] array)
        {
            List<T> enumList = new();

            foreach (var item in array)
            {
                T enumType = (T)Enum.Parse(typeof(T), item, false);
                enumList.Add(enumType);
            }

            return enumList.ToArray();
        }
    }
}
