using Library.Core.Model;
using Library.Core.Requests;
using Library.Core.Results;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Library.Core.Services
{
    /// <summary>
    /// Defines interface for user service.
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Gets list of all users.
        /// </summary>
        /// <returns>Returns user list as <see cref="UserResult"/></returns>
        Task<IEnumerable<UserResult>> GetAll(PagingFilteringModel model);

        /// <summary>
        /// Creates new user.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<UserResult> Create(UserCreateRequest request);

        /// <summary>
        /// Creates new user with required fields from MRTD recognizer result.
        /// Also adds parsed mrzData to database.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<UserResult> CreateUserWithMRTD(ImageRequest request);

        /// <summary>
        /// Gets user entity by id.
        /// </summary>
        /// <param name="userId">User id.</param>
        /// <returns>Returns <see cref="UserResult"/></returns>
        Task<UserResult> GetById(int userId);

        /// <summary>
        /// Updates user.
        /// </summary>
        /// <param name="userId">User id</param>
        /// <param name="request"><see cref="UserUpdateRequest"/></param>
        /// <returns></returns>
        Task Update(int userId, UserUpdateRequest request);

        /// <summary>
        /// Deletes user from database.
        /// </summary>
        /// <param name="userId">User id.</param>
        /// <returns></returns>
        Task Delete(int userId);
    }
}
