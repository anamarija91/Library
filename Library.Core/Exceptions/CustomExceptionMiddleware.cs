using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace Library.Core.Exceptions
{
    /// <summary>
    /// Defines custom middleware.
    /// </summary>
    public class CustomExceptionMiddleware
    {
        /// <summary>
        /// Processor for http request.
        /// </summary>
        private readonly RequestDelegate next;

        /// <summary>
        /// Defines weather in production environment.
        /// </summary>
        private readonly bool production;

        ///// <summary>
        ///// Logger.
        ///// </summary>
        //private readonly ILogger logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomExceptionMiddleware"/> class.
        /// </summary>
        /// <param name="next">Processor.</param>
        /// <param name="config">Configuration.</param>
        /// <param name="factory">Logger.</param>
        public CustomExceptionMiddleware(RequestDelegate next, IConfiguration config/*, ILoggerFactory factory*/)
        {
            this.next = next ?? throw new ArgumentNullException(nameof(next));
            production = config?.GetSection("Logging")?.GetValue<bool?>("Production") ?? true;
            //logger = factory?.CreateLogger<CustomExceptionMiddleware>() ?? throw new ArgumentNullException(nameof(factory));
        }

        /// <summary>
        /// Invoke.
        /// </summary>
        /// <param name="context">Context.</param>
        /// <returns>Returns delegate.</returns>
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next.Invoke(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        /// <summary>
        /// Defines handling exceptions.
        /// </summary>
        /// <param name="context">Context.</param>
        /// <param name="ex">Exception.</param>
        /// <returns>.</returns>
        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            int statusCode;
            string errorCode;
            string message;

            if (ex is CustomException cex)
            {
                statusCode = cex.StatusCode;
                errorCode = cex.ErrorCode;
                message = cex.Message;
                //logger.LogError(message);
            }
            else
            {
                statusCode = 500;
                errorCode = "InternalError";
                message = production ? "Oops! Something went wrong. Please try again." : ex.ToString();
                // logger.LogError(message);
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;

            await context.Response.WriteAsync(JsonConvert.SerializeObject(new
            {
                errorCode,
                message
            }));
        }
    }
}
