using Library.Core.Model.Entities;
using Library.Core.Repositories;
using Library.Infrastructure.EfModels;
using Microsoft.EntityFrameworkCore;
using Sieve.Services;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Infrastructure.EfRepositories
{
    /// <summary>
    /// Defines repository for user.
    /// </summary>
    public class UserRepository : RepositoryBase<User, int>, IUserRepository
    {
        /// <summary>
        /// Initialize new instance of the <see cref="UserRepository"/> class.
        /// </summary>
        /// <param name="context">Libary context.</param>
        /// <param name="processor">Sieve processor.</param>
        public UserRepository(LibraryContext context, ISieveProcessor processor) : base(context, processor)
        {
        }

        /// <inhertitdoc />
        protected override IQueryable<User> GetTableQueryable()
        {
            return base.GetTableQueryable();
        }

        /// <inheritdoc cref="IRepository{TEntity, TPrimaryKey}.GetById(TPrimaryKey)"/>
        public override async Task<User> GetById(int id)
        {
            return await GetTableQueryable().FirstOrDefaultAsync(e => e.Id == id);
        }
    }
}
