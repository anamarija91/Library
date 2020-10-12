using Library.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Library.Core.Repositories
{
    /// <summary>
    /// Defines repository interface
    /// </summary>
    /// <typeparam name="TEntity">Entity type</typeparam>
    /// <typeparam name="TPrimaryKey">Entity primary key</typeparam>
    public interface IRepository<TEntity, TPrimaryKey>
        where TEntity : class
    {
        /// <summary>
        /// Gets all entities for the specified table including a preset condition.
        /// </summary>
        /// <returns>All table entities</returns>
        Task<IEnumerable<TEntity>> GetAll();

        /// <summary>
        /// Gets all entities for the specified table including a sieve query.
        /// </summary>
        /// <returns>All table entities</returns>
        Task<IEnumerable<TEntity>> GetAll(PagingFilteringModel model);

        /// <summary>
        /// Filters entities according to <paramref name="filterExpression"/>
        /// </summary>
        /// <param name="filterExpression"></param>
        /// <returns></returns>
        Task<IEnumerable<TEntity>> Filter(Expression<Func<TEntity, bool>> filterExpression);

        /// <summary>
        /// Counts all entities for the specified table with a preset condition and also conditional filter.
        /// </summary>
        /// <param name="filterExpression">Filter expression</param>
        /// <returns>Number of entities that meet the condition</returns>
        Task<int> Count(Expression<Func<TEntity, bool>> filterExpression);

        /// <summary>
        /// Checks if entities for the specified table with a preset condition and also conditional filter exist.
        /// </summary>
        /// <param name="filterExpression">Filter expression</param>
        /// <returns>True if entities that meet the condition exist, else false</returns>
        Task<bool> Exists(Expression<Func<TEntity, bool>> filterExpression);

        /// <summary>
        /// Checks if entity exists in table.
        /// </summary>
        /// <param name="id">Entity primary key</param>
        /// <returns>True if entity exists, else false</returns>
        Task<bool> Exists(TPrimaryKey id);

        /// <summary>
        /// Get the entity with the specified id.
        /// </summary>
        /// <param name="id">Unique identifier</param>
        /// <returns>Entity</returns>
        Task<TEntity> GetById(TPrimaryKey id);

        /// <summary>
        /// Adds an additional entity to the table.
        /// Makes sure it's state is set to active
        /// </summary>
        /// <param name="entity">Entity</param>
        /// <returns></returns>
        Task<TEntity> Add(TEntity entity);

        /// <summary>
        /// Updates an already existing entity.
        /// </summary>
        /// <param name="entity">Updated entity</param>
        void Update(TEntity entity);

        /// <summary>
        /// Removes an existing entity.
        /// </summary>
        /// <param name="entity">Entity for deletion</param>
        void Delete(TEntity entity);
    }
}
