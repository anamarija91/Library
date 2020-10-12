using Library.Core.Clients;
using Library.Core.Utils;
using Library.Infrastructure.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Library.Api.Extensions
{
    /// <summary>
    /// Defines class for adding Validatable settings
    /// </summary>
    public static class SettingsDependancyInjection
    {
        /// <summary>
        /// Adds validatable settings to dependency injection.
        /// </summary>
        /// <param name="services">Services for extension.</param>
        /// <param name="configuration">Configuration.</param>
        /// <returns></returns>
        public static IServiceCollection AddSettingsToDependencyInjection(this IServiceCollection services, IConfiguration configuration)
        {
            _ = services.ConfigureValidatableSetting<DatabaseSettings>(configuration?.GetSection(ProjectConstants.DatabaseSection));
            _ = services.ConfigureValidatableSetting<MicroblinkSettings>(configuration?.GetSection(ProjectConstants.MicroblinkSection));

            return services;
        }
    }
}
