using System;

namespace DapperRepository.Extensions
{
    internal static class ObjectExtensions
    {
        /// <summary>
        /// returns true if a given object is default value is a default type, false otherwise
        /// </summary>
        /// <param name="value">object value</param>
        /// <returns>true if a given object is default value is a default type, false otherwise</returns>
        public static bool IsDefaultValue(this object value)
        {
            switch (value)
            {
                case int i:
                    return default(int) == i;
                case DateTime i:
                    return default(DateTime) == i;
                case decimal i:
                    return default(decimal) == i;
                case double i:
                    return default(double) == i;
                case float i:
                    return default(float) == i;
                case long i:
                    return default(long) == i;
                case short i:
                    return default(short) == i;
                case uint i:
                    return default(uint) == i;
                case ulong i:
                    return default(ulong) == i;
                case ushort i:
                    return default(ushort) == i;
                case sbyte i:
                    return default(sbyte) == i;
                case Guid i:
                    return default(Guid) == i;
                default:
                    return false;
            }
        }
    }
}
