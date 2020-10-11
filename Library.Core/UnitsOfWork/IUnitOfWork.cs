using Library.Core.Repositories;
using System.Threading.Tasks;

namespace Library.Core.UnitsOfWork
{
    /// <summary>
    /// Defines interface for UnitOfWork.
    /// </summary>
    public interface IUnitOfWork
    {
        /// <summary>
        /// Used to commit changes to the underlaying persistence layer
        /// </summary>
        /// <returns></returns>
        Task Commit();

        /// <summary>
        /// Gets a Repository depending on it's type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <returns></returns>
        IRepository<T, TKey> GetRepository<T, TKey>() where T : class;

        /// <summary>
        /// Used to get users repository.
        /// </summary>
        IUserRepository Users { get; }

        /// <summary>
        /// Used to get bookcopies repository.
        /// </summary>
        IBookCopyRepository BookCopies { get; }

        /// <summary>
        /// Used to get book titles repository.
        /// </summary>
        IBookTitleRepository BookTitles { get; }

        /// <summary>
        /// Uset to get book rentals.
        /// </summary>
        IRentalRepository Rentals { get; }
    }
}
