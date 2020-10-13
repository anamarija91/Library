using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace Library.Api.Extensions.Cors
{
    /// <summary>
    /// Defines cors extension.
    /// </summary>
    public static class CorsServiceExtensions
    {
            /// <summary>
            /// Sets constant for cors policy.
            /// </summary>
            private const string CORSPOLICY = "CorsPolicy";

            /// <summary>
            /// Sets constant for allow origin.
            /// </summary>
            private const string ALLOWORIGIN = "Allow-Origin";

            /// <summary>
            /// Sets constant for expose headers.
            /// </summary>
            private const string EXPOSEHEADERS = "Expose-Headers";

            /// <summary>
            /// Adds api cors to service descriptors.
            /// </summary>
            /// <param name="services">Service descriptors.</param>
            /// <param name="configuration">Configuration properties. </param>
            public static void AddApiCors(this IServiceCollection services, IConfiguration configuration)
            {
                var cors = configuration?.GetSection("Cors");

                var origins = Array.Empty<string>();
                var exposedHeaders = Array.Empty<string>();

                if (cors.Exists())
                {
                    if (cors.GetSection(ALLOWORIGIN).Exists())
                    {
                        origins = cors.GetSection(ALLOWORIGIN).GetChildren().Select(origin => origin.Value).ToArray();
                    }

                    if (cors.GetSection(EXPOSEHEADERS).Exists())
                    {
                        exposedHeaders = cors.GetSection(EXPOSEHEADERS).GetChildren().Select(exposedHeader => exposedHeader.Value).ToArray();
                    }
                }

                services.AddCors(options =>
                {
                    options.AddPolicy(
                        CORSPOLICY,
                        builder => builder.WithOrigins(origins)
                        .WithExposedHeaders(exposedHeaders)
                        .WithExposedHeaders("Location")
                        .WithExposedHeaders("Content-Disposition")
                        .WithExposedHeaders("X-Total-Count")
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials());
                });
            }

            /// <summary>
            /// Extends application pipeline to use cors.
            /// </summary>
            /// <param name="app">Application pipeline.</param>
            public static void UseApiCors(this IApplicationBuilder app)
            {
                app.UseCors(CORSPOLICY);
            }
        }
}
