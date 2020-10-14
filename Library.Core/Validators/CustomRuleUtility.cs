using FluentValidation;
using Library.Core.Utils;

namespace Library.Core.Validators
{
    /// <summary>
    /// Defines extension class for fluent validation rules.
    /// </summary>
    public static class CustomRuleUtility
    {
        /// <summary>
        /// Defines extension rule for name validaton.
        /// Name should have at least 2 characters.
        /// </summary>
        /// <typeparam name="T">Type for validation.</typeparam>
        /// <param name="rule">Rule builder.</param>
        /// <returns></returns>
        public static IRuleBuilderOptions<T, string> NameValidation<T>(this IRuleBuilder<T, string> rule)
        {
            return rule.MinimumLength(2).WithMessage("Minimum 2 characters required.");
        }

        /// <summary>
        /// Defines extension rule for date validation.
        /// Date should have required format <see cref="ProjectConstants.DateFormat"/>
        /// </summary>
        /// <typeparam name="T">Type for validation</typeparam>
        /// <param name="rule">Rule builder</param>
        /// <returns></returns>
        public static IRuleBuilderOptions<T, string> DateValidation<T>(this IRuleBuilder<T, string> rule)
        {
            return rule
                    .NotEmpty().WithMessage("Date is required.")
                    .Must(date => ValidatorUtility.IsDateValid(date, ProjectConstants.DateFormat))
                    .WithMessage("Date is not in valid format. Required date format: " + ProjectConstants.DateFormat + " .");
        }

        /// <summary>
        /// Defines extension rule for email list validation.
        /// </summary>
        /// <typeparam name="T">Type for validation</typeparam>
        /// <param name="rule">Rule builder</param>
        /// <returns></returns>
        public static IRuleBuilderOptions<T, string> EmailListValidation<T>(this IRuleBuilder<T, string> rule)
        {
            return rule.ChildRules(email =>
            {
                email.RuleFor(e => e).EmailAddress().WithMessage("Valid email required.");
            }
            );
        }

        /// <summary>
        /// Defines extension rule for phone list validation.
        /// </summary>
        /// <typeparam name="T">Type for validation</typeparam>
        /// <param name="rule">Rule builder</param>
        /// <returns></returns>
        public static IRuleBuilderOptions<T, string> PhoneListValidation<T>(this IRuleBuilder<T, string> rule)
        {
            return rule.ChildRules(phone =>
            {
                phone.RuleFor(p => p).Matches(@"^\+385-(1|[1-9][0-9])-[0-9]{3}-[0-9]{4}$").WithMessage("Valid phone required. ex. '+385-99-555-7447', or '+385-1-888-7777");
            });

        }
    }
}


