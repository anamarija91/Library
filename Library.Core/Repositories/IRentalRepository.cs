using Library.Core.Model.Entities;
using Library.Core.Results;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Library.Core.Repositories
{
    /// <summary>
    /// Defines interface for book rental repository.
    /// </summary>
    public interface IRentalRepository 
        : IRepository<Rental, int>
    {
        /// <summary>
        /// Checks if book copy is available for rent.
        /// </summary>
        /// <param name="bookCopyId">BookCopy id</param>
        /// <returns>Returns True if available, false if not available.</returns>
        Task<bool> IsBookCopyAvailable(int bookCopyId);

        /// <summary>
        /// Finds total overdue for users, orders result by overdue and filters top N
        /// </summary>
        /// <param name="numberOfTopUsers">N</param>
        /// <returns>Returns ordered list of users with total overdue as <see cref="OverdueResult"/></returns>
        Task<IEnumerable<OverdueResult>> GetTotalOverdues(int numberOfTopUsers);

        /// <summary>
        /// Find rental entity for user and book copy that is not returned 
        /// </summary>
        /// <param name="userId">UserId</param>
        /// <param name="bookCopyId">Book Copy Id</param>
        /// <returns></returns>
        Task<Rental> GetRentalForUserAndNotReturnedBookCopy(int userId, int bookCopyId);
    }
}
