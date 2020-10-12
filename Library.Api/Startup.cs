using FluentValidation.AspNetCore;
using Library.Api.Extensions;
using Library.Api.Extensions.Configuration;
using Library.Api.Extensions.Swagger;
using Library.Core.Exceptions;
using Library.Core.UnitsOfWork;
using Library.Core.Utils;
using Library.Infrastructure.EfModels;
using Library.Infrastructure.EfUnitsOfWork;
using Library.Infrastructure.SieveProcessors;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Sieve.Models;
using Sieve.Services;
using System.Globalization;


namespace Library.Api
{
    /// <summary>
    /// Defines startup class.
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Gets configuration
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Initializes a new instance of <see cref="Startup"/> class.
        /// </summary>
        /// <param name="configuration">Configuration</param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        ///  Adds funcionality to API.
        ///     - Controllers
        ///     - Swagger documentation    
        ///     - Filtering, Sorting, Paging
        ///     - Database    
        ///     - Dependancy Injection configuration (Services, Validatable settings)
        ///     - HttpContext Accessor
        ///     - Unit of work
        ///     - Validation
        ///     - HttpClients
        ///     
        ///     Missing:
        ///     -- Versioning
        ///     -- Authorization
        ///     -- Logging
        ///     -- Cors
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            var cultureInfo = new CultureInfo("en-US");

            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

            services.AddControllers();

            services.AddSwaggerDocumentation(typeof(Startup).Namespace);

            // Validation
            //services.AddMvcCore(options => options.Filters.Add(typeof(ValidateModelStateAttribute)));
            services.AddMvc().AddFluentValidation(options => options.RegisterValidatorsFromAssembly(typeof(Startup).Assembly));
            services.UseConfigurationValidation();

            // Filter
            services.Configure<SieveOptions>(Configuration.GetSection("Sieve"));

            // TODO: add identity / authorization configuration

            // TODO: add and setup Logging
            //services.AddLogService(configuration);

            _ = services.AddDbContext<LibraryContext>(builder =>
            {
                _ = builder.UseSqlServer(Configuration.CreateConnectionString(ProjectConstants.DatabaseSection));
                _ = builder.EnableSensitiveDataLogging(true);
            });

            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            _ = services.AddServicesToDependencyInjection();
            _ = services.AddValidatorsToDependencyInjection();
            _ = services.AddSettingsToDependencyInjection(Configuration);

            _ = services.AddHttpClientsToDependancyInjection();

            _ = services.AddScoped<IUnitOfWork, UnitOfWork>();
            _ = services.AddScoped<ISieveProcessor, LibrarySieveProcessor>();
        }

        /// <summary>
        /// Configure HTTP request pipeline.
        /// 
        ///     - Middleware
        ///     - Routing
        ///     - Swagger
        ///     - Endpoints
        ///     
        ///  Missing:
        ///     
        ///     - Authentication
        ///     - Cors
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMiddleware<CustomExceptionMiddleware>();

            app.UseRouting();

            app.UseSwaggerDocumentation();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
