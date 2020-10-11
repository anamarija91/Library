using Library.Core.UnitsOfWork;
using System;

namespace Library.Core.Services
{
    /// <summary>
    /// Defines service base class.
    /// </summary>
    public abstract class ServiceBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceBase"/> class.
        /// </summary>
        /// <param name="unitOfWork">Unit of work.</param>
        public ServiceBase(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        /// <summary>
        /// Gets or sets Unit of work.
        /// </summary>
        protected IUnitOfWork UnitOfWork { get; set; }
    }
}
