using Library.Core.Model;
using Library.Core.Model.Entities;
using Library.Core.Requests;
using Library.Core.Results;
using Library.Core.UnitsOfWork;
using Library.Core.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Core.Services
{
    /// <summary>
    /// Defines user service.
    /// </summary>
    public class UserService
        : ServiceBase, IUserService
    {

        /// <summary>
        /// Initilizes new instance of <see cref="UserService"/>
        /// </summary>
        /// <param name="unitOfWork">Unit of work.</param>
        public UserService(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {

        }

        /// <inheritdoc />
        public async Task<UserResult> Create(UserCreateRequest request)
        {
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var userEntity = new User(request);

            _ = await UnitOfWork.Users.Add(userEntity);
            await UnitOfWork.Commit();

            return new UserResult(userEntity);
        }

        /// <inheritdoc />
        public async Task Delete(int userId)
        {
            var user = await ValidatorUtility.GetById(UnitOfWork.Users, userId);

            UnitOfWork.Users.Delete(user);
            await UnitOfWork.Commit();
        }

        /// <inheritdoc />
        public async Task<IEnumerable<UserResult>> GetAll(PagingFilteringModel model)
        {
            var users = await UnitOfWork.Users.GetAll(model);

            return users.Select(u => new UserResult(u));
        }

        /// <inheritdoc />
        public async Task<UserResult> GetById(int userId)
        {
            var user = await ValidatorUtility.GetById(UnitOfWork.Users, userId);

            return new UserResult(user);
        }

        /// <inheritdoc />
        public async Task Update(int userId, UserUpdateRequest request)
        {

            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var user = await ValidatorUtility.GetById(UnitOfWork.Users, userId);

            user.UpdateUserData(request);

            UnitOfWork.Users.Update(user);
            await UnitOfWork.Commit();
        }
    }
}
