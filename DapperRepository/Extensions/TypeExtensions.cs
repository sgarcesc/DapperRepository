using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace DapperRepository.Extensions
{
    internal static class TypeExtensions
    {
        /// <summary>
        /// Gets the Sql String For Table
        /// </summary>
        /// <param name="type"></param>
        /// <param name="parameterNames"></param>
        /// <returns></returns>
        public static StringBuilder GetSqlString(this Type type, IEnumerable<string> parameterNames)
        {
            var builder = new StringBuilder();
            builder.AppendLine($"select * from {type.GetTableName()} ");
            builder.AppendLine("where 1 = 1 ");
            foreach (var name in parameterNames)
            {
                builder.AppendLine($"and [{name}] = @{name} ");
            }
            return builder;

        }

        /// <summary>
        /// Gets the Table Name for a given entity type
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns
        public static string GetTableName(this Type type)
        {
            var tableAttr = type.GetTypeInfo().GetCustomAttributes(false).SingleOrDefault(attr => attr.GetType().Name == "TableAttribute") as dynamic;

            if (tableAttr != null) return tableAttr.Name;

            return type.Name + "s";
        }
    }
}
