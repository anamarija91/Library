using Library.Core.Model.Entities;
using Library.Core.Repositories;
using Library.Infrastructure.EfModels;
using Microsoft.EntityFrameworkCore;
using Sieve.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Infrastructure.EfRepositories
{
    /// <summary>
    /// Defines Book title repository class.
    /// </summary>
    public class BookTitleRepository 
        : RepositoryBase<BookTitle, int>, IBookTitleRepository
    {
        /// <summary>
        /// Initializes new instance of <see cref="BookTitleRepository"/> class.
        /// </summary>
        /// <param name="context">Library context</param>
        /// <param name="processor">Sieve processor</param>
        public BookTitleRepository(LibraryContext context, ISieveProcessor processor) 
            : base(context, processor)
        {
        }

        /// <inheritdoc cref="IRepository{TEntity, TPrimaryKey}.GetById(TPrimaryKey)"/>
        public async override Task<BookTitle> GetById(int id)
        {
            return await GetTableQueryable().FirstOrDefaultAsync(e => e.Id == id);
        }

        /// <inheritdoc />
        public async Task<IEnumerable<int>> GetBookCopyIds(int bookTitleId)
        {
            var entity = await GetTableQueryable()
                                    .Where(bc => bc.Id == bookTitleId)
                                    .Include(bt => bt.BookCopy)
                                    .AsNoTracking()
                                    .FirstOrDefaultAsync();

            return entity.BookCopy.Select(bc => bc.Id);
        }
    }
}
