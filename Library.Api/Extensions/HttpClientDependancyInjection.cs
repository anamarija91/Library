using Library.Core.Clients;
using Microsoft.Extensions.DependencyInjection;

namespace Library.Api.Extensions
{
    /// <summary>
    /// Adds http clients to dependancy injection 
    /// </summary>
    public static class HttpClientDependancyInjection
    {
        /// <summary>
        /// Adds http clients to dependency injection.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddHttpClientsToDependancyInjection(this IServiceCollection services)
        {
            _ = services.AddHttpClient<MicroblinkClient>();

            return services;
        }
    }
}
