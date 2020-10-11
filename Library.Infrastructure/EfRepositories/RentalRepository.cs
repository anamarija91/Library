using Library.Core.Model.Entities;
using Library.Core.Repositories;
using Library.Core.Results;
using Library.Infrastructure.EfModels;
using Microsoft.EntityFrameworkCore;
using Sieve.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Infrastructure.EfRepositories
{
    /// <summary>
    /// Defines Rental repository class.
    /// </summary>
    public class RentalRepository 
        : RepositoryBase<Rental, int>, IRentalRepository
    {
        /// <summary>
        /// Initializes new instance of <see cref="RentalRepository"/> class.
        /// </summary>
        /// <param name="context">Library context</param>
        /// <param name="processor">Sieve processor</param>
        public RentalRepository(LibraryContext context, ISieveProcessor processor) 
            : base(context, processor)
        {
        }

        /// <inheritdoc cref="IRepository{TEntity, TPrimaryKey}.GetById(TPrimaryKey)"/>
        public async override Task<Rental> GetById(int id)
        {
            return await GetTableQueryable().FirstOrDefaultAsync(e => e.Id == id);
        }

        /// <inheritdoc />
        public async Task<bool> IsBookCopyAvailable(int bookCopyId)
        {
            return !(await Exists(r => r.BookCopyId == bookCopyId && !r.DateReturned.HasValue)); //TODO: also check if returned date less or equal than date od borrowing book
        }

        /// <inheritdoc />
        public async Task<IEnumerable<OverdueResult>> GetTotalOverdues(int numberOfTopUsers)
        {
            //var rentals = await GetTableQueryable().Select(r => new { r.UserId, OverDue = GetOverdue(r.DateDue, r.DateReturned) })
            //                                        .GroupBy(r => r.UserId)
            //                                        .Select(g => new OverdueResult { UserId = g.Key, TotalOverdue = g.Sum(r => r.OverDue) })
            //                                        .OrderBy(or => or.TotalOverdue)
            //                                        //  .Take(numberOfTopUsers)
            //                                        .AsNoTracking()
            //                                        .ToListAsync(); ;


            var overdueList = await GetTableQueryable().Select(r => new { r.UserId, r.DateDue, r.DateReturned })
                                                       .Where(r => !r.DateReturned.HasValue || r.DateDue < r.DateReturned)
                                                       .AsNoTracking()
                                                       .ToListAsync();

            var totalOverdue = overdueList.GroupBy(r => r.UserId)
                                          .Select(g => new OverdueResult { UserId = g.Key, TotalOverdue = g.Sum(r => GetOverdue(r.DateDue, r.DateReturned)) })
                                          .OrderByDescending(or => or.TotalOverdue)
                                          .Take(numberOfTopUsers)
                                          .ToList();

            return totalOverdue;
        }

        /// <inheritdoc />
        public async Task<Rental> GetRentalForUserAndNotReturnedBookCopy(int userId, int bookCopyId)
        {
            return await GetTableQueryable().Where(r => r.UserId == userId && r.BookCopyId == bookCopyId && !r.DateReturned.HasValue)
                                     .AsNoTracking()
                                     .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Gets overdue in days for date returned and date due.
        /// If book is returned before due there is no overdue
        /// </summary>
        /// <param name="dateDue">Due date to return the book</param>
        /// <param name="dateReturned">Date book is returned</param>
        /// <returns></returns>
        private int GetOverdue(DateTime dateDue, DateTime? dateReturned)
        {
            if (dateReturned.HasValue && dateReturned.Value.Date > dateDue.Date)
            {

                return (dateReturned.Value - dateDue).Days;
            }
            else if (dateDue < DateTime.Now)
            {
                return (DateTime.Now - dateDue).Days;
            }
            else
            {
                return 0;
            }
        }
    }
}
