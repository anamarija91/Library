using FluentValidation;
using Library.Core.Model.Entities;
using Library.Core.Requests;
using Library.Core.UnitsOfWork;
using Library.Core.Utils;
using Microsoft.AspNetCore.Http;

namespace Library.Core.Validators
{
    /// <summary>
    /// Defines rent rules for new rent event:
    ///  - Required fields: UserId, BookCopyId, DateRented
    ///  - Check if book is available
    ///  - Validate date format
    /// </summary>
    public class CreateRentalValidator : AbstractValidator<CreateRentalRequest, Rental, int>
    {
        public CreateRentalValidator(IUnitOfWork unitOfWork, IHttpContextAccessor accessor)
            : base(unitOfWork, accessor)
        {
            _ = RuleFor(r => r.UserId)
                   .MustAsync(async (userIdValue, cancellation) => await unitOfWork.Users.Exists(userIdValue)).WithMessage("User doesn't exist.");

            _ = RuleFor(r => r.BookCopyId)
                   .Cascade(CascadeMode.Stop)
                   .MustAsync(async (bookIdValue, cancellation) => await unitOfWork.BookCopies.Exists(bookIdValue)).WithMessage("Book copy doesn't exist.")
                   .MustAsync(async (bookIdValue, cancellation) => await unitOfWork.Rentals.IsBookCopyAvailable(bookIdValue)).WithMessage("Book copy is already borrowed.");

            _ = RuleFor(r => r.DateRented)
                    .Cascade(CascadeMode.Stop)
                    .NotEmpty().WithMessage("DateRented is required.")
                    .Must(date => ValidatorUtility.IsDateValid(date, ProjectConstants.DateFormat))
                    .WithMessage("Date is not in valid format. Required date format: " + ProjectConstants.DateFormat + " .");
        }
    }

    /// <summary>
    /// Defines update return date request:
    ///     - Required fields: UserId, BookCopyId, DateRented
    ///     - Check if book is can be returned
    ///     - Validate return date format
    /// </summary>
    public class PatchRentalValidator : AbstractValidator<PatchRentalRequest, Rental, int>
    {
        public PatchRentalValidator(IUnitOfWork unitOfWork, IHttpContextAccessor accessor)
            : base(unitOfWork, accessor)
        {

            _ = RuleFor(r => new { r.UserId, r.BookCopyId })
                  .MustAsync(async (userBookValues, cancellation) => await unitOfWork.Rentals.Exists(r => r.UserId == userBookValues.UserId
                                                                                                          && r.BookCopyId == userBookValues.BookCopyId
                                                                                                          && !r.DateReturned.HasValue))
                  .WithMessage("Rent event for bookCopyId and userId with no return date doesn't exist.");

            _ = RuleFor(r => r.DateReturned)
                   .Cascade(CascadeMode.Stop)
                   .NotEmpty().WithMessage("DateRented is required.")
                   .Must(date => ValidatorUtility.IsDateValid(date, ProjectConstants.DateFormat))
                   .WithMessage("Date is not in valid format. Required date format: " + ProjectConstants.DateFormat + " .");
        }
    }
}
