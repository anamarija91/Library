using Library.Core.Parsers;
using Library.Core.Services;
using Library.Infrastructure.Parsers;
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
            _ = services.AddScoped<IMicroblinkClientService, MicroblinkClientService>();
            _ = services.AddScoped<IMRZDataService, MrzDataService>();

            _ = services.AddSingleton<IParserFactory, ParserFactory>();

            return services;
        }
    }
}
