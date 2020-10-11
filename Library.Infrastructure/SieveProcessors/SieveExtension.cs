using Library.Core.Exceptions;
using Microsoft.EntityFrameworkCore;
using Sieve.Exceptions;
using Sieve.Models;
using Sieve.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Infrastructure.SieveProcessors
{
    public static class SieveExtensions
    {
        /// <summary>
        /// Applies filtering, sorting and paging to an IQueryable.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="processor"></param>
        /// <param name="model">The model that defines the filtering, sorting and paging parameters</param>
        /// <param name="query">Anything that implements IQueryable</param>
        /// <returns></returns>
        public static async Task<IList<T>> Process<T>(this ISieveProcessor processor, SieveModel model, IQueryable<T> query) where T : class
        {
            try
            {
                if (model == null)
                    throw new ValidationFailedException("Model is required.");
                if (model.Page != null && model.Page < 1)
                    throw new ValidationFailedException("Page must be higher than zero.");
                if (model.PageSize != null && model.PageSize < 1)
                    throw new ValidationFailedException("Page size must be higher than zero.");

                return await processor.Apply(model, query.AsNoTracking()).ToListAsync();
            }
            catch (SieveException sex)
            {
                throw new ValidationFailedException(sex.Message);
            }
        }
    }
}
