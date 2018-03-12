using Dapper;
using Dapper.Contrib.Extensions;
using DapperRepository.Extensions;
using DapperRepository.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace DapperRepository
{
    internal class Repository<T> : IRepository<T> where T : class
    {
        private readonly string _connectionString;
        protected IDbConnection Connection
        {
            get => new SqlConnection(_connectionString);
        }

        public Repository(string connectionString)
        {
            if (string.IsNullOrWhiteSpace(connectionString)) throw new ArgumentNullException(nameof(connectionString));

        }

        public virtual IEnumerable<T> GetAll()
        {
            using (var connection = Connection)
            {
                return connection.GetAll<T>();
            }
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            using (var connection = Connection)
            {
                return await connection.GetAllAsync<T>();
            }
        }

        public virtual T GetById(string id)
        {
            using (var connection = Connection)
            {
                return connection.Get<T>(id);
            }
        }

        public T GetById(int id)
        {
            using (var connection = Connection)
            {
                return connection.Get<T>(id);
            }
        }

        public virtual T GetById(Guid id)
        {
            using (var connection = Connection)
            {
                return connection.Get<T>(id);
            }
        }

        public virtual async Task<T> GetByIdAsync(string id)
        {
            using (var connection = Connection)
            {
                return await connection.GetAsync<T>(id);
            }
        }

        public virtual async Task<T> GetByIdAsync(int id)
        {
            using (var connection = Connection)
            {
                return await connection.GetAsync<T>(id);
            }
        }

        public virtual async Task<T> GetByIdAsync(Guid id)
        {
            using (var connection = Connection)
            {
                return await connection.GetAsync<T>(id);
            }
        }

        public virtual long Insert(T entity)
        {
            using (var connection = Connection)
            {
                return connection.Insert(entity);
            }
        }

        public virtual long Insert(IEnumerable<T> list)
        {
            using (var connection = Connection)
            {
                var transaction = connection.BeginTransaction(IsolationLevel.ReadUncommitted);
                try
                {
                    foreach (var entity in list)
                    {
                        connection.Insert(entity, transaction: transaction);
                    }
                    transaction.Commit();
                    return list.Count();
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    throw e;
                }
            }
        }

        public virtual async Task<int> InsertAsync(T entity)
        {
            using (var connection = Connection)
            {
                return await connection.InsertAsync(entity);
            }
        }
        public virtual async Task<int> InsertAsync(IEnumerable<T> list)
        {
            using (var connection = Connection)
            {
                var transaction = connection.BeginTransaction(IsolationLevel.ReadUncommitted);
                try
                {
                    var tasks = list.Select(c => connection.InsertAsync(c, transaction: transaction));
                    await Task.WhenAll(tasks);
                    transaction.Commit();
                    return list.Count();
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    throw e;
                }
            }
        }
        public virtual bool Update(T entity)
        {
            using (var connection = Connection)
            {
                return connection.Update(entity);
            }
        }
        public virtual bool Update(IEnumerable<T> list)
        {
            using (var connection = Connection)
            {
                var transaction = connection.BeginTransaction(IsolationLevel.ReadUncommitted);
                try
                {
                    foreach (var entity in list)
                    {
                        connection.Update(entity, transaction: transaction);
                    }
                    transaction.Commit();
                    return list.Any();
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    throw e;
                }
            }
        }
        public virtual async Task<bool> UpdateAsync(T entity)
        {
            using (var connection = Connection)
            {
                return await connection.UpdateAsync(entity);
            }
        }
        public virtual async Task<bool> UpdateAsync(IEnumerable<T> list)
        {
            using (var connection = Connection)
            {
                var transaction = connection.BeginTransaction(IsolationLevel.ReadUncommitted);
                try
                {
                    var tasks = list.Select(c => connection.UpdateAsync(c, transaction: transaction));
                    await Task.WhenAll(tasks);
                    transaction.Commit();
                    return list.Any();
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    throw e;
                }
            }
        }
        private bool Delete(T entity)
        {
            using (var connection = Connection)
            {
                return connection.Delete(entity);
            }
        }

        private bool Delete(IEnumerable<T> list)
        {
            using (var connection = Connection)
            {
                var transaction = connection.BeginTransaction(IsolationLevel.ReadUncommitted);
                try
                {
                    foreach (var entity in list)
                    {
                        connection.Delete(list, transaction: transaction);
                    }
                    transaction.Commit();
                    return list.Any();
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    throw e;
                }

            }
        }
        private async Task<bool> DeleteAsync(T entity)
        {
            using (var connection = Connection)
            {
                return await connection.DeleteAsync(entity);
            }
        }
        private async Task<bool> DeleteAsync(IEnumerable<T> list)
        {
            using (var connection = Connection)
            {
                var transaction = connection.BeginTransaction(IsolationLevel.ReadUncommitted);
                try
                {
                    var tasks = list.Select(c => connection.DeleteAsync(list, transaction: transaction));
                    await Task.WhenAll(tasks);
                    transaction.Commit();
                    return list.Any();
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    throw e;

                }
            }
        }
        public virtual IEnumerable<T> Query(string sql, object param = null)
        {
            using (var connection = Connection)
            {
                return connection.Query<T>(sql: sql, param: param);
            }
        }
        public virtual async Task<IEnumerable<T>> QueryAsync(string sql, object param = null)
        {
            using (var connection = Connection)
            {
                return await connection.QueryAsync<T>(sql: sql, param: param);
            }
        }
        public virtual IEnumerable<T> QueryDynamic<TFilter>(TFilter filter) where TFilter : class
        {
            using (var connection = Connection)
            {
                var parameters = filter.GetParameters();
                var sql = typeof(T).GetSqlString(parameters.ParameterNames);
                return connection.Query<T>(sql: sql.ToString(), param: parameters);
            }
        }
        public virtual async Task<IEnumerable<T>> QueryDynamicAsync<TFilter>(TFilter filter) where TFilter : class
        {
            using (var connection = Connection)
            {
                var parameters = filter.GetParameters();
                var sql = typeof(T).GetSqlString(parameters.ParameterNames);
                return await connection.QueryAsync<T>(sql: sql.ToString(), param: parameters);
            }
        }
        public virtual (PaginationInfo pagination, IEnumerable<T> elements) QueryDynamicPaged<TFilter>(TFilter filter, PaginationParams paginationParams) where TFilter : class
        {
            var totalElements = QueryDynamicCount(filter);
            using (var connection = Connection)
            {
                var pagination = filter.GetPagination(totalElements, paginationParams);
                var parameters = filter.GetParameters();
                var sql = typeof(T).GetSqlString(parameters.ParameterNames)
                                   .AddOrderByToSqlString(paginationParams)
                                   .AddPagingToSqlString(paginationParams);
                var elements = connection.Query<T>(sql: sql.ToString(), param: parameters);
                return (pagination, elements);
            }
        }
        public virtual async Task<(PaginationInfo pagination, IEnumerable<T> elements)> QueryDynamicPagedAsync<TFilter>(TFilter filter, PaginationParams paginationParams) where TFilter : class
        {
            var totalElements = await QueryDynamicCountAsync(filter);
            using (var connection = Connection)
            {
                var pagination = filter.GetPagination(totalElements, paginationParams);
                var parameters = filter.GetParameters();
                var sql = typeof(T).GetSqlString(parameters.ParameterNames)
                                   .AddOrderByToSqlString(paginationParams)
                                   .AddPagingToSqlString(paginationParams);
                var elements = await connection.QueryAsync<T>(sql: sql.ToString(), param: parameters);
                return (pagination, elements);
            }
        }
        public virtual int QueryDynamicCount<TFilter>(TFilter filter) where TFilter : class
        {
            using (var connection = Connection)
            {
                var parameters = filter.GetParameters();
                var sql = typeof(T).GetSqlStringCount(parameters.ParameterNames);
                return connection.ExecuteScalar<int>(sql: sql.ToString(), param: parameters);
            }
        }
        public virtual async Task<int> QueryDynamicCountAsync<TFilter>(TFilter filter) where TFilter : class
        {
            using (var connection = Connection)
            {
                var parameters = filter.GetParameters();
                var sql = typeof(T).GetSqlStringCount(parameters.ParameterNames);
                return await connection.ExecuteScalarAsync<int>(sql: sql.ToString(), param: parameters);
            }
        }
    }
}
