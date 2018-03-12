using Dapper;
using DapperRepository.Models;
using System;
using System.Linq;
using System.Reflection;
using System.Text;

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

        /// <summary>
        /// Returns a pagination info object with the TotalPages, TotalElement
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filter"></param>
        /// <param name="totalElements"></param>
        /// <param name="paginationParams"></param>
        /// <returns></returns>
        public static PaginationInfo GetPagination<T>(this T filter, int totalElements, PaginationParams paginationParams) where T : class
        {
            return new PaginationInfo
            {
                TotalElements = totalElements,
                PageNumber = paginationParams.PageNumber,
                PageSize = paginationParams.PageSize,
                TotalPages = GetTotalPages(totalElements, paginationParams.PageSize)
            };
        }

        private static int GetTotalPages(int totalElements, int pageSize)
        {
            if (pageSize == 0) return 0;
            double totalPages = totalElements / (double)pageSize;
            return (int)Math.Ceiling(totalPages);
        }

        public static StringBuilder AddOrderByToSqlString(this StringBuilder sql, PaginationParams paginationParams)
        {
            //Return if no orderby present
            if (!paginationParams.ValidateOrderBy()) return sql;

            //Get the order by list
            var notNullElements = paginationParams.OrderBy.Where(c => !string.IsNullOrWhiteSpace(c.ColumnName.Trim())).Select(d => d.ToString());

            //Append new line with oder
            sql.AppendLine($"order by {string.Join(", ", notNullElements)}");

            return sql;
        }

        private static bool ValidateOrderBy(this PaginationParams paginationParams)
        {
            if (paginationParams.OrderBy == null) return false;

            //if does not contain any order by, return
            if (!paginationParams.OrderBy.Any()) return false;

            //check for null elements
            return paginationParams.OrderBy.Any(c => !string.IsNullOrWhiteSpace(c.ColumnName.Trim()));
        }

        public static StringBuilder AddPagingToSqlString(this StringBuilder sql, PaginationParams paginationParams)
        {
            //Return if no orderby present
            if (!paginationParams.ValidateOrderBy()) return sql;

            //return because paging is not possible if page size || page number is less than or equals to 0
            if (paginationParams.PageSize <= 0 || paginationParams.PageNumber <= 0) return sql;

            //Calculate offset
            int offset = GetOffset(paginationParams.PageSize, paginationParams.PageNumber);

            //return because and offset less than 0 is not possible
            if (offset < 0) return sql;

            sql.AppendLine($"offset {offset} rows");
            sql.AppendLine($"fetch next {paginationParams.PageSize} rows only");

            return sql;
        }

        private static int GetOffset(int pageSize, int pageNumber)
        {
            return (pageNumber - 1) * pageSize;
        }
    }
}
