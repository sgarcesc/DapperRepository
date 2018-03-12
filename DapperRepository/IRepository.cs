using DapperRepository.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DapperRepository
{
    public interface IRepository<T> where T : class
    {
        /// <summary>
        /// Returns all of the entites from table "Ts".
        /// </summary>
        /// <returns></returns>
        IEnumerable<T> GetAll();

        /// <summary>
        /// Returns all of the entites from table "Ts". asynchronously
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<T>> GetAllAsync();

        /// <summary>
        /// Returns a single entity by a single id from table "Ts". Id must be marked with [Key] attribute.
        /// </summary>
        /// <param name="id">Primary key</param>
        /// <returns></returns>
        T GetById(string id);

        /// <summary>
        /// Returns a single entity by a single id from table "Ts". Id must be marked with [Key] attribute.
        /// </summary>
        /// <param name="id">Primary key</param>
        /// <returns></returns>
        T GetById(int id);

        /// <summary>
        /// Returns a single entity by a single id from table "Ts". Id must be marked with [Key] attribute.
        /// </summary>
        /// <param name="id">Primary key</param>
        /// <returns></returns>
        T GetById(Guid id);

        /// <summary>
        /// Returns a single entity by a single id from table "Ts" asynchronously. Id must be marked with [Key] attribute.
        /// </summary>
        /// <param name="id">Primary key</param>
        /// <returns></returns>
        Task<T> GetByIdAsync(string id);

        /// <summary>
        /// Returns a single entity by a single id from table "Ts" asynchronously. Id must be marked with [Key] attribute.
        /// </summary>
        /// <param name="id">Primary key</param>
        /// <returns></returns>
        Task<T> GetByIdAsync(int id);

        /// <summary>
        /// Returns a single entity by a single id from table "Ts" asynchronously. Id must be marked with [Key] attribute.
        /// </summary>
        /// <param name="id">Primary key</param>
        Task<T> GetByIdAsync(Guid id);

        /// <summary>
        /// Inserts an entity into table "Ts" and returns identity id.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        long Insert(T entity);

        /// <summary>
        /// Inserts an entity into table "Ts" asynchronously and returns identity id.
        /// </summary>
        /// <param name="entity">Entity to insert</param>
        /// <returns></returns>
        Task<int> InsertAsync(T entity);

        /// <summary>
        /// Inserts a list of entities into table "Ts" and returns identity id.
        /// </summary>
        /// <param name="list">Entities to insert</param>
        /// <returns>Number of inserted rows</returns>
        long Insert(IEnumerable<T> list);

        /// <summary>
        /// Inserts a list of entities into table "Ts" asynchronously and returns identity id.
        /// </summary>
        /// <param name="list"></param>
        /// <returns>Number of inserted rows</returns>
        Task<int> InsertAsync(IEnumerable<T> list);

        /// <summary>
        /// Updates entity in table "Ts"
        /// </summary>
        /// <param name="entity">Entity to be updated</param>
        /// <returns>true if updated, false if not found or not modified (tracked entities)</returns>
        bool Update(T entity);

        /// <summary>
        /// Updates entity in table "Ts" asynchronously
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<bool> UpdateAsync(T entity);

        /// <summary>
        /// Updates a list of entities in table "Ts".
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        bool Update(IEnumerable<T> list);

        /// <summary>
        /// Updates a list of entities in table "Ts" asynchronously
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        Task<bool> UpdateAsync(IEnumerable<T> list);

        /// <summary>
        /// Executes a query, returning the data typed as T.
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        IEnumerable<T> Query(string sql, object param = null);

        /// <summary>
        ///  Execute a query, returning the data typed as T. asynchronously 
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        Task<IEnumerable<T>> QueryAsync(string sql, object param = null);

        /// <summary>
        /// Executes a query, given a filter T, a direct column-name===member-name mapping is assumed (case insensitive)
        /// </summary>
        /// <typeparam name="TFilter"></typeparam>
        /// <param name="filter"></param>
        /// <returns></returns>
        IEnumerable<T> QueryDynamic<TFilter>(TFilter filter) where TFilter : class;

        /// <summary>
        /// Executes a query, given a filter T, a direct column-name===member-name mapping is assumed (case insensitive), asynchronously
        /// </summary>
        /// <typeparam name="TFilter"></typeparam>
        /// <param name="filter"></param>
        /// <returns></returns>
        Task<IEnumerable<T>> QueryDynamicAsync<TFilter>(TFilter filter) where TFilter : class;

        /// <summary>
        /// Executes an offset query, given a filter T and pagination parameters, a direct column-name===member-name mapping is assumed (case insensitive)
        /// </summary>
        /// <typeparam name="TFilter"></typeparam>
        /// <param name="filter"></param>
        /// <param name="paginationParams"></param>
        /// <returns></returns>
        (PaginationInfo pagination, IEnumerable<T> elements) QueryDynamicPaged<TFilter>(TFilter filter, PaginationParams paginationParams) where TFilter : class;

        /// <summary>
        /// Executes an offset query, given a filter T and pagination parameters, a direct column-name===member-name mapping is assumed (case insensitive), asynchronously
        /// </summary>
        /// <typeparam name="TFilter"></typeparam>
        /// <param name="filter"></param>
        /// <param name="paginationParams"></param>
        /// <returns></returns>
        Task<(PaginationInfo pagination, IEnumerable<T> elements)> QueryDynamicPagedAsync<TFilter>(TFilter filter, PaginationParams paginationParams) where TFilter : class;

        /// <summary>
        /// Executes a count query, given a filter T
        /// </summary>
        /// <typeparam name="TFilter"></typeparam>
        /// <param name="filter"></param>
        /// <returns></returns>
        int QueryDynamicCount<TFilter>(TFilter filter) where TFilter : class;

        /// <summary>
        /// Executes a count query, given a filter T, asynchronously
        /// </summary>
        /// <typeparam name="TFilter"></typeparam>
        /// <param name="filter"></param>
        /// <returns></returns>
        Task<int> QueryDynamicCountAsync<TFilter>(TFilter filter) where TFilter : class;
    }
}
