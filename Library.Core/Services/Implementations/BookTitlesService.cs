using Library.Core.Results;
using Library.Core.UnitsOfWork;
using Library.Core.Validators;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Library.Core.Services
{
    /// <summary>
    /// Defines book title service class
    /// </summary>
    public class BookTitlesService
        : ServiceBase, IBookTitlesService
    {
        private readonly IRentalService _rentalService;

        /// <summary>
        /// Initilizes new instance of <see cref="BookTitlesService"/>
        /// </summary>
        /// <param name="unitOfWork">Unit of work.</param>
        public BookTitlesService(IUnitOfWork unitOfWork, IRentalService rentalService) 
            : base(unitOfWork)
        {
            _rentalService = rentalService ?? throw new ArgumentNullException(nameof(rentalService));
        }

        /// <inheritdoc />
        public async Task<IEnumerable<RentalResult>> GetBookTitleRentalDetails(int bookTitleId)
        {
            await ValidatorUtility.GetById(UnitOfWork.BookTitles, bookTitleId);

            var bookCopyIds = await UnitOfWork.BookTitles.GetBookCopyIds(bookTitleId);

            var rentals = await _rentalService.GetRentHistoryForBookCopies(bookCopyIds);

            return rentals;
        }
    }
}
