using Library.Core.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Library.Api.Extensions
{
    /// <summary>
    /// Defines class for adding services
    /// </summary>
    public static class ServiceDependancyInjection
    {
        /// <summary>
        /// Adds services to dependency injection.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddServicesToDependencyInjection(this IServiceCollection services)
        {
            _ = services.AddScoped<IUserService, UserService>();
            _ = services.AddScoped<IRentalService, RentalService>();
            _ = services.AddScoped<IBookTitlesService, BookTitlesService>();

            return services;
        }
    }
}
