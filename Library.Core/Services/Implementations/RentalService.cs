using Library.Core.Model.Entities;
using Library.Core.Requests;
using Library.Core.Results;
using Library.Core.UnitsOfWork;
using Library.Core.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Library.Core.Services
{
    /// <summary>
    /// Defines rental service class
    /// </summary>
    public class RentalService
        : ServiceBase, IRentalService
    {
        /// <summary>
        /// Initilizes new instance of <see cref="RentalService"/>
        /// </summary>
        /// <param name="unitOfWork">Unit of work.</param>
        public RentalService(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {

        }

        /// <inheritdoc />
        public async Task<IEnumerable<RentalResult>> GetAll()
        {
            var rentals = await UnitOfWork.Rentals.GetAll();

            return rentals.Select(r => new RentalResult(r)).ToList();
        }

        /// <inheritdoc />
        public async Task<RentalResult> GetById(int id)
        {
            var rental = await UnitOfWork.Rentals.GetById(id);

            return new RentalResult(rental);
        }

        /// <inheritdoc />
        public async Task<RentalResult> CreateRentalEvent(CreateRentalRequest createRentalRequest)
        {
            if (createRentalRequest is null)
            {
                throw new ArgumentNullException(nameof(createRentalRequest));
            }

            var entity = new Rental
            {
                BookCopyId = createRentalRequest.BookCopyId,
                UserId = createRentalRequest.UserId,
                DateRented = Helpers.GetDateFromString(createRentalRequest.DateRented, ProjectConstants.DateFormat)
            };

            entity.DateDue = entity.DateRented.AddDays(ProjectConstants.ReturnPolicyDays);

            await UnitOfWork.Rentals.Add(entity);
            await UnitOfWork.Commit();

            return new RentalResult(entity);
        }

        /// <inheritdoc />
        public async Task ReturnBookCopy(PatchRentalRequest patchRentalRequest)
        {
            if (patchRentalRequest is null)
            {
                throw new ArgumentNullException(nameof(patchRentalRequest));
            }

            var entity = await UnitOfWork.Rentals.GetRentalForUserAndNotReturnedBookCopy(patchRentalRequest.UserId, patchRentalRequest.BookCopyId);

            entity.DateReturned = Helpers.GetDateFromString(patchRentalRequest.DateReturned, ProjectConstants.DateFormat);

            UnitOfWork.Rentals.Update(entity);
            await UnitOfWork.Commit();
        }


        /// <inheritdoc />
        public async Task<IEnumerable<RentalResult>> GetRentHistoryForBookCopies(IEnumerable<int> bookCopyIds)
        {
            var entites = await UnitOfWork.Rentals.Filter(r => bookCopyIds.Contains(r.BookCopyId));

            return entites.Select(r => new RentalResult(r)).OrderBy(r => r.DateRented);
        }

        /// <inheritdoc />
        public async Task<IEnumerable<OverdueResult>> GetTopUsersOverdueTimes(int numberOfTopUsers)
        {
            return await UnitOfWork.Rentals.GetTotalOverdues(numberOfTopUsers);
        }
    }
}
