using Library.Core.Model.Entities;
using Library.Core.Repositories;
using Library.Core.UnitsOfWork;
using Library.Infrastructure.EfModels;
using Library.Infrastructure.EfRepositories;
using Sieve.Services;
using System;
using System.Threading.Tasks;

namespace Library.Infrastructure.EfUnitsOfWork
{  
    /// <summary>
   /// Defines unit of work.
   /// </summary>
    public class UnitOfWork 
        : IUnitOfWork
    {
        private readonly LibraryContext context;
        private readonly ISieveProcessor processor;

        private IUserRepository userRepository;
        private IBookCopyRepository bookCopyRepository;
        private IBookTitleRepository bookTitleRepository;
        private IRentalRepository rentalRepository;

        public UnitOfWork(LibraryContext context, ISieveProcessor processor)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
            this.processor = processor ?? throw new ArgumentNullException(nameof(processor));
        }

        /// <inheritdoc/>
        public IUserRepository Users
        {
            get
            {
                if (userRepository is null)
                {
                    userRepository = new UserRepository(context, processor);
                }

                return userRepository;
            }
        }

        /// <inheritdoc/>
        public IBookCopyRepository BookCopies
        {
            get
            {
                if (bookCopyRepository is null)
                {
                    bookCopyRepository = new BookCopyRepository(context, processor);
                }

                return bookCopyRepository;
            }
        }

        /// <inheritdoc/>
        public IBookTitleRepository BookTitles
        {
            get
            {
                if (bookTitleRepository is null)
                {
                    bookTitleRepository = new BookTitleRepository(context, processor);
                }

                return bookTitleRepository;
            }
        }

        /// <inheritdoc/>
        public IRentalRepository Rentals
        {
            get
            {
                if (rentalRepository is null)
                {
                    rentalRepository = new RentalRepository(context, processor);
                }

                return rentalRepository;
            }
        }

        /// <inheritdoc/>
        public async Task Commit() => _ = await context.SaveChangesAsync();


        /// <inheritdoc/>
        public IRepository<T, TKey> GetRepository<T, TKey>() where T : class
        {
            var type = typeof(T);

            return true switch
            {
                bool _ when type == typeof(BookCopy) => BookCopies as IRepository<T, TKey>,
                bool _ when type == typeof(BookTitle) => BookTitles as IRepository<T, TKey>,
                bool _ when type == typeof(User) => Users as IRepository<T, TKey>,
                bool _ when type == typeof(Rental) => Rentals as IRepository<T, TKey>,
                _ => throw new ArgumentException("The requested type doesn't have an exposed repository"),
            };
        }
    }
}
