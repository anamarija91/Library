using NetEscapades.Configuration.Validation;
using System;

namespace Library.Core.Clients
{
    /// <summary>
    /// Defines validation class for microblink settings.
    /// </summary>
    public class MicroblinkSettings
        : IValidatable
    {
        public string Host { get; set; }

        public string AuthorizationHeader { get; set; }


        /// <summary>
        /// Validates mandatory microblink settings. If a setting is not defined throws a <see cref="ArgumentException"/>
        /// </summary>
        public void Validate()
        {
            if (string.IsNullOrEmpty(Host))
            {
                throw new ArgumentException("Host url is required!");
            }

            if (string.IsNullOrEmpty(AuthorizationHeader))
            {
                throw new ArgumentException("AuthorizationHeader is required!");
            }
        }
    }
}
