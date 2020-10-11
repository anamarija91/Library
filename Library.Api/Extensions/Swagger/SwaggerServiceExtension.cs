using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Library.Api.Extensions.Swagger
{
    /// <summary>
    /// Defines extension class for swagger services
    /// </summary>
    public static class SwaggerServiceExtension
    {
        /// <summary>
        /// Sets up all swagger services components.
        /// </summary>
        /// <param name="services">Collection of service descriptors.</param>
        /// <param name="title">Title.</param>
        /// <returns>Returns updated services.</returns>
        public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services, string title)
        {
            _ = services.AddSwaggerDocument(o => o.Title = title);

            return services;
        }

        /// <summary>
        /// Sets app swagger for application builder.
        /// </summary>
        /// <param name="app">Application pipeline.</param>
        /// <returns>Returns updated application pipeline.</returns>
        public static IApplicationBuilder UseSwaggerDocumentation(this IApplicationBuilder app)
        {
            app.UseOpenApi();
            app.UseSwaggerUi3();

            return app;
        }
    }
}
