using Library.Core.Model;
using Library.Core.Repositories;
using Library.Infrastructure.EfModels;
using Library.Infrastructure.SieveProcessors;
using Microsoft.EntityFrameworkCore;
using Sieve.Models;
using Sieve.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Library.Infrastructure.EfRepositories
{
    /// <summary>
    /// Defines repository base class.
    /// </summary>
    /// <typeparam name="TEntity">Entity type</typeparam>
    public abstract class RepositoryBase<TEntity, TPrimaryKey> 
        : IRepository<TEntity, TPrimaryKey> where TEntity : class
    {
        protected LibraryContext Context { get; }
        protected readonly ISieveProcessor processor;

        public RepositoryBase(LibraryContext context, ISieveProcessor processor)
        {
            this.Context = context ?? throw new ArgumentNullException(nameof(context));
            this.processor = processor ?? throw new ArgumentNullException(nameof(processor));
        }

        /// <inheritdoc />
        protected DbSet<TEntity> GetTable()
        {
            return Context.Set<TEntity>();
        }

        /// <inheritdoc />
        protected virtual IQueryable<TEntity> GetTableQueryable()
        {
            return GetTable();
        }

        /// <inheritdoc />
        public virtual async Task<TEntity> Add(TEntity entity)
        {
            _ = await GetTable().AddAsync(entity);

            return entity;
        }

        /// <inheritdoc />
        public virtual void Delete(TEntity entity)
        {
            _ = GetTable().Remove(entity);
        }

        /// <inheritdoc />
        public virtual async Task<int> Count(Expression<Func<TEntity, bool>> filterExpression)
        {
            return await GetTableQueryable().Where(filterExpression)
                                            .AsNoTracking()
                                            .CountAsync();
        }

        /// <inheritdoc />
        public virtual async Task<IEnumerable<TEntity>> Filter(Expression<Func<TEntity, bool>> filterExpression)
        {
            return await GetTableQueryable().Where(filterExpression)
                                            .AsNoTracking()
                                            .ToListAsync();
        }

        /// <inheritdoc />
        public virtual async Task<bool> Exists(Expression<Func<TEntity, bool>> filterExpression)
        {
            return await Count(filterExpression) > 0;
        }

        /// <inheritdoc />
        public virtual async Task<bool> Exists(TPrimaryKey id)
        {
            var check = await GetById(id);
            return !(check is null);
        }

        /// <inheritdoc />
        public virtual async Task<IEnumerable<TEntity>> GetAll()
        {
            return await GetTableQueryable()
                            .AsNoTracking()
                            .ToListAsync();
        }

        /// <inheritdoc />
        public virtual async Task<IEnumerable<TEntity>> GetAll(PagingFilteringModel model)
        {
            var entities = await processor.Process(PagingFilteringModelToSieveModel(model), GetTableQueryable().AsNoTracking());
            return entities;
        }

        /// <inheritdoc />
        public abstract Task<TEntity> GetById(TPrimaryKey id);

        /// <inheritdoc />
        public void Update(TEntity entity)
        {
            _ = GetTable().Update(entity);
        }

        /// <summary>
        /// Turns a PagingFilteringModel instance into an instance of SieveModel
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private SieveModel PagingFilteringModelToSieveModel(PagingFilteringModel model)
        {
            if (model is null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            return new SieveModel
            {
                Filters = model.Filters,
                Sorts = model.Sorts,
                //Page = model.Page,
                //PageSize = model.PageSize
            };
        }
    }
}
