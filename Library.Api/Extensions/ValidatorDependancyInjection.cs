using FluentValidation;
using Library.Core.Requests;
using Library.Core.Validators;
using Microsoft.Extensions.DependencyInjection;

namespace Library.Api.Extensions
{
    /// <summary>
    /// Defines class for adding validators
    /// </summary>
    public static class ValidatorDependancyInjection
    {
        /// <summary>
        /// Adds validators to dependency injection.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddValidatorsToDependencyInjection(this IServiceCollection services)
        {
            _ = services.AddTransient<IValidator<UserCreateRequest>, UserCreateValidator>();
            _ = services.AddTransient<IValidator<UserUpdateRequest>, UserUpdateValidator>();
            _ = services.AddTransient<IValidator<CreateRentalRequest>, CreateRentalValidator>();

            return services;
        }
    }
}
