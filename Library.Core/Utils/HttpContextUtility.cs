using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System;

namespace Library.Core.Utils
{
    /// <summary>
    /// Defines HttpContext utility class.
    /// </summary>
    public static class HttpContextUtility
    {
        /// <summary>
        /// Uses the <see cref="IHttpContextAccessor"/> to retreive the id named <paramref name="idIdentifier"/> from RouteData.
        /// </summary>
        /// <exception cref="InternalConflictException">When the selected parameter name can't be parsed to type
        /// <typeparamref name="T"/></exception>
        /// <typeparam name="T">Type of the id</typeparam>
        /// <param name="accessor">Instance of <see cref="IHttpContextAccessor"/></param>
        /// <param name="idIdentifier">Id identifier string that matches the one from the route</param>
        /// <returns>Parsed id value of type <typeparamref name="T"/></returns>
        public static T GetIdFromRoute<T>(IHttpContextAccessor accessor, string idIdentifier)
        {
            if (accessor is null)
            {
                throw new ArgumentNullException(nameof(accessor));
            }

            var id = accessor.HttpContext.GetRouteData()?.Values[idIdentifier]?.ToString();

            return TypeUtility.ParseFromString<T>(id);
        }
    }
}
