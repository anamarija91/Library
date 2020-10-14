using FluentValidation;
using Library.Core.Model.Entities;
using Library.Core.Requests;
using Library.Core.UnitsOfWork;
using Microsoft.AspNetCore.Http;

namespace Library.Core.Validators
{
    /// <summary>
    /// Defines class for user  validation rules.
    /// -- Required fields: FirstName, LastName, Date of Birth
    /// -- Additional validation: Emails, Phones 
    public class UserValidator : AbstractValidator<UserRequest, User, int>
    {
        public UserValidator(IUnitOfWork unitOfWork, IHttpContextAccessor accessor)
           : base(unitOfWork, accessor)
        {
            _ = RuleFor(r => r.FirstName).NameValidation();
            _ = RuleFor(r => r.LastName).NameValidation();

            _ = RuleFor(r => r.DateOfBirth)
                    .Cascade(CascadeMode.Stop)
                    .DateValidation();

            _ = RuleForEach(r => r.Emails).EmailListValidation();

            _ = RuleForEach(r => r.Phones).PhoneListValidation();
        }

    }

    /// <summary>
    /// Defines class for user create validation rules.
    /// 
    ///  /// TODO: Unique fields ??
    /// </summary>
    public class UserCreateValidator : UserValidator
    {
        public UserCreateValidator(IUnitOfWork unitOfWork, IHttpContextAccessor accessor)
            : base(unitOfWork, accessor)
        {
        }
    }

    /// <summary>
    /// Defines class for user update validation rules.      
    /// 
    /// TODO: Unique fields ?? (on update!!)
    /// </summary>
    public class UserUpdateValidator : UserValidator
    {
        public UserUpdateValidator(IUnitOfWork unitOfWork, IHttpContextAccessor accessor)
            : base(unitOfWork, accessor)
        {
        }
    }
}

