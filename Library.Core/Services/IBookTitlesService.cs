using Library.Core.Results;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Library.Core.Services
{
    /// <summary>
    /// Defines interface for book title service
    /// </summary>
    public interface IBookTitlesService
    {
        /// <summary>
        /// Gets all rent details for book title. 
        /// </summary>
        /// <param name="bookTitleId">Book Title id</param>
        /// <returns></returns>
        Task<IEnumerable<RentalResult>> GetBookTitleRentalDetails(int bookTitleId);
    }
}
