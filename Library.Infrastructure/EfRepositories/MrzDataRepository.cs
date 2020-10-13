using Library.Core.Model.Entities;
using Library.Core.Repositories;
using Library.Infrastructure.EfModels;
using Microsoft.EntityFrameworkCore;
using Sieve.Services;
using System.Threading.Tasks;

namespace Library.Infrastructure.EfRepositories
{
    /// <summary>
    /// Defines MRZdata repository class.
    /// </summary>
    public class MrzDataRepository
        : RepositoryBase<Mrzdata, int>, IMrzDataRepository
    {
        /// <summary>
        /// Initializes new instance of <see cref="MrzDataRepository"/> class.
        /// </summary>
        /// <param name="context">Library context</param>
        /// <param name="processor">Sieve processor</param>
        public MrzDataRepository(LibraryContext context, ISieveProcessor processor)
            : base(context, processor)
        {
        }

        /// <inheritdoc cref="IRepository{TEntity, TPrimaryKey}.GetById(TPrimaryKey)"/>
        public async override Task<Mrzdata> GetById(int id)
        {
            return await GetTableQueryable().FirstOrDefaultAsync(e => e.Id == id);
        }
    }
}
