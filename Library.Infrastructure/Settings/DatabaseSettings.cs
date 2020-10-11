using NetEscapades.Configuration.Validation;
using System;

namespace Library.Infrastructure.Settings
{
    /// <summary>
    /// Defines validation class for database settings.
    /// </summary>
    public class DatabaseSettings : IValidatable
    {
        public string Host { get; set; }

        public string Database { get; set; }

        public string User { get; set; }

        public string Password { get; set; }

        /// <summary>
        /// Validates mandatory database settings. If a setting is not defined throws a <see cref="ArgumentException"/>
        /// </summary>
        public void Validate()
        {
            if (string.IsNullOrEmpty(Host))
            {
                throw new ArgumentException("Host url is required!");
            }

            if (string.IsNullOrEmpty(Database))
            {
                throw new ArgumentException("Databse is required!");
            }

            if (string.IsNullOrEmpty(User))
            {
                throw new ArgumentException("User is required!");
            }

            if (string.IsNullOrEmpty(Password))
            {
                throw new ArgumentException("Password is required!");
            }
        }
    }
}
