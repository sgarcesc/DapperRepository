﻿using System;
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
        /// Updates entity in table "Ts",
        /// </summary>
        /// <param name="entity">Entity to be updated</param>
        /// <returns>true if updated, false if not found or not modified (tracked entities)</returns>
        bool Update(T entity);
        Task<bool> UpdateAsync(T entity);
        bool Update(IEnumerable<T> list);
        Task<bool> UpdateAsync(IEnumerable<T> list);
        IEnumerable<T> Query(string sql, object param = null);
        Task<IEnumerable<T>> QueryAsync(string sql, object param = null);
        IEnumerable<T> QueryDynamic<TFilter>(TFilter filter) where TFilter : class;
        Task<IEnumerable<T>> QueryDynamicAsync<TFilter>(TFilter filter) where TFilter : class;
    }
}
