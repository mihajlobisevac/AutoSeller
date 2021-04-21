using System;
using System.Collections.Generic;

namespace Domain.Extensions
{
    public static class EnumExtensions
    {
        public static T ToEnum<T>(this string enumName) => (T)Enum.Parse(typeof(T), enumName, false);

        public static int[] ToEnumValueArray<T>(this List<string> array)
        {
            List<int> enumList = new();

            foreach (string item in array)
            {
                int enumValue = (int)Enum.Parse(typeof(T), item, false);
                enumList.Add(enumValue);
            }

            return enumList.ToArray();
        }
    }
}
