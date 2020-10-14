using Library.Core.UnitsOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;

namespace Library.Infrastructure.EfUnitsOfWork
{
    /// <summary>
    /// Defines database transaction
    /// </summary>
    public class DatabaseTransaction : IDatabaseTransaction
    {
        private readonly DbContext dbContext;
        private IDbContextTransaction transaction;
        private bool disposedValue;


        /// <summary>
        /// Initialize new instance of <see cref="DatabaseTransaction"/>
        /// </summary>
        /// <param name="dbContext">Database context</param>
        public DatabaseTransaction(DbContext dbContext)
        {
            this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }


        public IDbContextTransaction Transaction
        {
            get
            {
                if (transaction is null)
                {
                    CreateTransaction();
                }

                return transaction;
            }
        }


        /// <inheritdoc/>
        public void CommitTransaction()
        {
            transaction.Commit();
        }

        /// <inheritdoc/>
        public void CreateTransaction()
        {
            transaction = dbContext.Database.BeginTransaction();
        }

        /// <inheritdoc/>
        public void RollbackTransaction()
        {
            transaction.Rollback();
        }

        /// <inheritdoc/>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    transaction.Dispose();
                }

                disposedValue = true;
            }
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
