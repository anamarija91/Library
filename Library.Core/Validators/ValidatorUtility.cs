using Library.Core.Exceptions;
using Library.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;

namespace Library.Core.Validators
{
    public static class ValidatorUtility
    {
        /// <summary>
        /// Gets the entity <typeparamref name="TEntity"/> by id.
        ///used for getting entities in methods without AbstractValidator validation 
        /// </summary>
        /// <exception cref="EntityNotFoundException">When the entity with given id doesn't exist.</exception>
        /// <typeparam name="TEntity">Entity type</typeparam>
        /// <typeparam name="TPrimaryKey">Primary key type</typeparam>
        /// <param name="repository">Entity repository</param>
        /// <param name="id">Entity id</param>
        /// <returns>Entity with given id</returns>
        public async static Task<TEntity> GetById<TEntity, TPrimaryKey>(IRepository<TEntity, TPrimaryKey> repository, TPrimaryKey id)
            where TEntity : class
        {
            if (repository is null)
            {
                throw new ArgumentNullException(nameof(repository));
            }

            var entity = await repository.GetById(id);

            CheckIfEntityFound(entity, id);

            return entity;
        }

        /// <summary>
        /// Checks if entity is found, based on default value.
        /// If Entity is not found throws <see cref="EntityNotFoundException"/>
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity">Entity</param>
        /// <param name="value">Entity value</param>
        public static void CheckIfEntityFound<TEntity>(TEntity entity, object value)
        {
            if (EqualityComparer<TEntity>.Default.Equals(entity, default))
            {
                throw new EntityNotFoundException(typeof(TEntity).Name, value);
            }
        }

        /// <summary>
        /// Checks if date is valid for given format
        /// </summary>
        /// <param name="dateValue"></param>
        /// <returns></returns>
        public static bool IsDateValid(string dateValue, string dateFormat)
        {
            return DateTime.TryParseExact(dateValue, dateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out var _);
        }
    }
}
