using Library.Core.Model.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Library.Core.Repositories
{
    /// <summary>
    /// Defines interface for book title repository.
    /// </summary>
    public interface IBookTitleRepository
        : IRepository<BookTitle, int>
    {
        /// <summary>
        /// Finds list of book copys for given book title
        /// </summary>
        /// <param name="bookTitleId">Book Title Id</param>
        /// <returns>Returns list of book copy ids</returns>
        Task<IEnumerable<int>> GetBookCopyIds(int bookTitleId);
    }
}
