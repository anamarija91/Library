using Library.Core.Requests;
using Library.Core.Results;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Library.Core.Services
{
    /// <summary>
    /// Defines interface for rental service
    /// </summary>
    public interface IRentalService
    {
        /// <summary>
        /// Gets all rentals
        /// </summary>
        /// <returns>Returns list of rentals as <see cref="RentalResult"/></returns>
        Task<IEnumerable<RentalResult>> GetAll();

        /// <summary>
        /// Get Rental result by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<RentalResult> GetById(int id);

        /// <summary>
        /// Creates new rent event
        /// </summary>
        /// <param name="createRentalRequest">New Rent request with book copy to rent, user that wants to rent a book, and date of rental </param>
        /// <returns></returns>
        Task<RentalResult> CreateRentalEvent(CreateRentalRequest createRentalRequest);

        /// <summary>
        /// Gets rent values for every book copy, orders result by date of rent
        /// </summary>
        /// <param name="bookCopyIds">List of book copy ids</param>
        /// <returns></returns>
        Task<IEnumerable<RentalResult>> GetRentHistoryForBookCopies(IEnumerable<int> bookCopyIds);

        /// <summary>
        /// Gets top user by overdue times
        /// </summary>
        /// <param name="numberOfTopUsers">number of top users returned</param>
        /// <returns></returns>
        Task<IEnumerable<OverdueResult>> GetTopUsersOverdueTimes(int numberOfTopUsers);

        /// <summary>
        /// Updates rent event with return date for book copy borrowed by user
        /// </summary>
        /// <param name="patchRentalRequest">Rental request defining userId, bookCopyId and returnDate</param>
        /// <returns></returns>
        Task ReturnBookCopy(PatchRentalRequest patchRentalRequest);
    }
}
