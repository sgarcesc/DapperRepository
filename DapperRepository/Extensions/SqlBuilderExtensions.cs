using Dapper;
using System.Reflection;

namespace DapperRepository.Extensions
{
    internal static class SqlBuilderExtensions
    {
        /// <summary>
        /// Gets Parameters for object Dinamic
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static DynamicParameters GetParameters<T>(this T obj) where T : class
        {
            var properties = obj.GetType().GetRuntimeProperties();
            var parameters = new DynamicParameters();
            foreach (var property in properties)
            {
                var value = property.GetValue(obj);
                var name = property.Name;
                var propertyType = property.GetType();

                if (value is null ||
                    value == null ||
                    (propertyType.IsInstanceOfType(typeof(string)) && string.IsNullOrWhiteSpace(value.ToString())) ||
                    value.IsDefaultValue()) continue;

                parameters.Add(name, value);
            }
            return parameters;
        }
    }
}
