using Library.Core.Model.Entities;
using Library.Core.Repositories;
using Library.Infrastructure.EfModels;
using Microsoft.EntityFrameworkCore;
using Sieve.Services;
using System.Threading.Tasks;

namespace Library.Infrastructure.EfRepositories
{
    /// <summary>
    /// Defines user contact repository class.
    /// </summary>
    public class UserContactRepository
        : RepositoryBase<UserContact, int>, IUserContactRepository
    {
        /// <summary>
        /// Initialize new instance of <see cref="UserContactRepository"/> class
        /// </summary>
        /// <param name="context">Library context</param>
        /// <param name="processor">Sieve processor</param>
        public UserContactRepository(LibraryContext context, ISieveProcessor processor) : base(context, processor)
        {

        }

        /// <inheritdoc />
        public async override Task<UserContact> GetById(int id)
        {
            return await GetTableQueryable().FirstOrDefaultAsync(e => e.Id == id);
        }
    }
}
