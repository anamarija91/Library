using FluentValidation;
using Library.Core.Repositories;
using Library.Core.UnitsOfWork;
using Library.Core.Utils;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace Library.Core.Validators
{

    /// <summary>
    /// Defines abstract validator class.
    /// </summary>
    /// <typeparam name="TRequest">Request type.</typeparam>
    /// <typeparam name="TEntity">Model type.</typeparam>
    /// <typeparam name="TPrimaryKey">Primary key type.</typeparam>
    public abstract class AbstractValidator<TRequest, TEntity, TPrimaryKey> 
        : AbstractValidator<TRequest> where TEntity : class
    {
        private readonly IRepository<TEntity, TPrimaryKey> _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _accessor;
        public const string ID_ROUTE = "id";
        private const string EQUALS_METHOD = "Equals";

        /// <summary>
        /// Sets properties: repository for entity type <typeparamref name="TEntity"/> and <see cref="IHttpContextAccessor"/>.
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="accessor"></param>
        public AbstractValidator(IUnitOfWork unitOfWork, IHttpContextAccessor accessor)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _repository = unitOfWork.GetRepository<TEntity, TPrimaryKey>() ?? throw new ArgumentNullException(nameof(unitOfWork));
            _accessor = accessor ?? throw new ArgumentNullException(nameof(accessor));
        }

        /// <summary>
        /// Gets <typeparamref name="TEntity"/> repository.
        /// </summary>
        /// <returns><typeparamref name="TEntity"/> <typeparamref name="TPrimaryKey"/> repository</returns>
        public IRepository<TEntity, TPrimaryKey> GetRepository() => _repository;

        /// <summary>
        /// Gets <typeparamref name="TEntity"/> repository.
        /// </summary>
        /// <returns><typeparamref name="TEntity"/> <typeparamref name="TPrimaryKey"/> repository</returns>
        public IUnitOfWork GetUnitOfWork() => _unitOfWork;

        /// <summary>
        /// Checks if the <paramref name="requestValue"/> already exists for entity <typeparamref name="TEntity"/>
        /// and property <paramref name="propertyName"/>
        /// </summary>
        /// <typeparam name="T">Request value type that matches entity property type</typeparam>
        /// <param name="requestValue">Value for the property from request body</param>
        /// <param name="propertyName">Entity property name</param>
        /// <returns>True if requestValue already exists for entity.property name, false otherwise</returns>
        public async Task<bool> Exists<T>(T requestValue, string propertyName)
        {
            var predicate = ReflectionUtility.GetMethodPredicate<TEntity, T>(requestValue, propertyName, EQUALS_METHOD);

            return await _repository.Exists(predicate);
        }

        /// <summary>
        /// Checks if the <paramref name="updateValue"/> is different from property value of the entity whose id is
        /// defined in the request route.
        /// </summary>
        /// <typeparam name="T"><paramref name="updateValue"/> type</typeparam>
        /// <param name="updateValue">Update value from the request</param>
        /// <param name="propertyName">Property name of <typeparamref name="TEntity"/> that matches updateValue semantics</param>
        /// <returns>True if <paramref name="updateValue"/> is different from current property value of the entity, false otherwise</returns>
        public async Task<bool> IsUpdate<T>(T updateValue, string propertyName)
        {
            var id = HttpContextUtility.GetIdFromRoute<TPrimaryKey>(_accessor, ID_ROUTE);
            var method = ReflectionUtility.GetMethod(typeof(T), EQUALS_METHOD);

            var existingEntity = await _repository.GetById(id);
            var existingEntityValue = typeof(TEntity).GetProperty(propertyName).GetValue(existingEntity);

            return !(bool)method.Invoke(updateValue, new object[] { existingEntityValue });
        }

        /// <summary>
        /// Checks if <paramref name="updateValue"/> really is different from property value of the entity whose id is
        /// defined in the request route and if the <paramref name="updateValue"/> doesn't exist in the database.
        /// </summary>
        /// <typeparam name="T"><paramref name="updateValue"/> type</typeparam>
        /// <param name="updateValue">Update value from the request</param>
        /// <param name="propertyName">Property name of <typeparamref name="TEntity"/> that matches updateValue semantics</param>
        /// <returns>True if <paramref name="updateValue"/> is different from current property value of the entity and
        /// the <paramref name="updateValue"/> doesn't exist in the database yet (is unique).
        /// </returns>
        public async Task<bool> IsUpdateUnique<T>(T updateValue, string propertyName)
        {
            var isUpdate = await IsUpdate(updateValue, propertyName);
            if (isUpdate)
            {
                var updateValueExists = await Exists(updateValue, propertyName);
                return !updateValueExists;
            }
            else
            {
                return true;
            }
        }
    }
}
