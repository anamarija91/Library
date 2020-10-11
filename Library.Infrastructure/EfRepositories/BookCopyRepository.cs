using Library.Core.Model.Entities;
using Library.Core.Repositories;
using Library.Infrastructure.EfModels;
using Microsoft.EntityFrameworkCore;
using Sieve.Services;
using System.Threading.Tasks;

namespace Library.Infrastructure.EfRepositories
{
    /// <summary>
    /// Defines BookCopy repository class
    /// </summary>
    public class BookCopyRepository 
        : RepositoryBase<BookCopy, int>, IBookCopyRepository
    {
        /// <summary>
        /// Initializes new instance of <see cref="BookCopyRepository"/> class.
        /// </summary>
        /// <param name="context">Library context</param>
        /// <param name="processor">Sieve processor</param>
        public BookCopyRepository(LibraryContext context, ISieveProcessor processor) 
            : base(context, processor)
        {
        }

        /// <inheritdoc />
        public async override Task<BookCopy> GetById(int id)
        {
            return await GetTableQueryable().FirstOrDefaultAsync(e => e.Id == id);
        }
    }
}
