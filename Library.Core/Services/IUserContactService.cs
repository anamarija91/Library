using Library.Core.Model.Entities;
using Library.Core.Requests;
using System.Threading.Tasks;

namespace Library.Core.Services
{
    /// <summary>
    /// Defines user contact service interface
    /// </summary>
    public interface IUserContactService
    {
        /// <summary>
        /// Updates user contacts list with email and phone contacts from request
        /// </summary>
        /// <param name="user">User to update</param>
        /// <param name="request">Request data</param>
        /// <returns></returns>
        Task UpdateUserContacts(User user, UserRequest request);
    }
}
