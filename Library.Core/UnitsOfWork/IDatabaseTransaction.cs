using System;

namespace Library.Core.UnitsOfWork
{
    /// <summary>
    /// Defines database transaction interface
    /// </summary>
    public interface IDatabaseTransaction : IDisposable
    {
        /// <summary>
        /// Creates transaction object.
        /// </summary>
        void CreateTransaction();

        /// <summary>
        /// Commits current transaction.
        /// </summary>
        void CommitTransaction();

        /// <summary>
        /// Rollbacks commited changed to the state before starting the transaction.
        /// </summary>
        void RollbackTransaction();
    }
}
