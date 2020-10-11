using FluentValidation;
using Library.Core.Model.Entities;
using Library.Core.Requests;
using Library.Core.UnitsOfWork;
using Library.Core.Utils;
using Microsoft.AspNetCore.Http;

namespace Library.Core.Validators
{
    /// <summary>
    /// Defines class for user create validation rules.
    /// -- Required fields: FirstName, LastName, Email, Date of Birth, Address
    /// 
    /// TODO: Phone number validation
    /// TODO: Unique fields ??
    /// </summary>
    public class UserCreateValidator : AbstractValidator<UserCreateRequest, User, int>
    {
        public UserCreateValidator(IUnitOfWork unitOfWork, IHttpContextAccessor accessor)
            : base(unitOfWork, accessor)
        {
            _ = RuleFor(r => r.FirstName)
                    .NotEmpty().WithMessage("FirstName is required.");
            _ = RuleFor(r => r.LastName)
                    .NotEmpty().WithMessage("LastName is required.");
            _ = RuleFor(r => r.Email)
                    .NotEmpty().WithMessage("Email is required.");

            _ = RuleFor(r => r.Email)
                    .EmailAddress().When(r => !string.IsNullOrEmpty(r.DateOfBirth)).WithMessage("Valid email required.");
            
            //.MustAsync(async (mail, cancellation) => !await Exists(mail, "Email"))
            //.WithMessage("Email must be unique!");

            _ = RuleFor(r => r.DateOfBirth)
                    .Cascade(CascadeMode.Stop)
                    .NotEmpty().WithMessage("Date of Birth is required.")
                    .Must(date => ValidatorUtility.IsDateValid(date, ProjectConstants.DateFormat))
                    .WithMessage("Date is not in valid format. Required date format: " + ProjectConstants.DateFormat + " .");
           
            _ = RuleFor(r => r.Address)
                    .NotEmpty().WithMessage("Address is required.");

            // ?? phone number
        }
    }

    /// <summary>
    /// Defines class for user update validation rules.
    /// -- Required fields: FirstName, LastName, Email, Date of Birth, Address
    /// 
    /// TODO: Phone number validation
    /// TODO: Unique fields ??
    /// </summary>
    public class UserUpdateValidator : AbstractValidator<UserUpdateRequest, User, int>
    {
        public UserUpdateValidator(IUnitOfWork unitOfWork, IHttpContextAccessor accessor)
            : base(unitOfWork, accessor)
        {
            _ = RuleFor(r => r.FirstName)
                    .NotEmpty().WithMessage("FirstName is required.");
            _ = RuleFor(r => r.LastName)
                    .NotEmpty().WithMessage("LastName is required.");
            _ = RuleFor(r => r.Email)
                    .NotEmpty().WithMessage("Email is required.");

            _ = RuleFor(r => r.Email)
                    .EmailAddress().When(r => !string.IsNullOrEmpty(r.DateOfBirth)).WithMessage("Valid email required.");

            //.MustAsync(async (mail, cancellation) => !await IsUpdateUnique(mail, "Email"))
            //.WithMessage("Email must be unique!");

            _ = RuleFor(r => r.DateOfBirth)
                    .Cascade(CascadeMode.Stop)
                    .NotEmpty().WithMessage("Date of Birth is required.")
                    .Must(date => ValidatorUtility.IsDateValid(date, ProjectConstants.DateFormat))
                    .WithMessage("Date is not in valid format. Required date format: " + ProjectConstants.DateFormat + " .");

            _ = RuleFor(r => r.Address)
                    .NotEmpty().WithMessage("Address is required.");

            // ?? phone number
        }
    }
}
