using Microsoft.Extensions.Configuration;
using SmartFormat;
using System.Collections.Generic;
using System.Linq;

namespace Library.Api.Extensions.Configuration
{
    /// <summary>
    /// Defines configuration extension.
    /// </summary>
    public static class ConfigurationExtension
    {
        private const string CONNSTRINGKEY = "ConnectionString";

        /// <summary>
        /// Formats configuration extension for section and it's child properties.
        /// </summary>
        /// <param name="configuration">Configuration.</param>
        /// <param name="sectionKey">Section key.</param>
        /// <param name="formattingKey">Formatting key.</param>
        /// <returns>Returns formated configuration for section. Formatting is defined in formatting key section.</returns>
        public static string FormatConfigurationSection(this IConfiguration configuration, string sectionKey, string formattingKey)
        {
            var parameters = new Dictionary<string, string>();
            var section = configuration?.GetSection(sectionKey);
            if (!section.Exists())
            {
                return string.Empty;
            }

            foreach (var param in section.AsEnumerable(true))
            {
                parameters[param.Key] = param.Value;
            }

            return Smart.Format(section[formattingKey], parameters);
        }

        /// <summary>
        /// Calls creation of connection string.
        /// </summary>
        /// <param name="configuration">Configuration.</param>
        /// <param name="sectionKey">Section key.</param>
        /// <returns>Return foramted configuration.</returns>
        public static string CreateConnectionString(this IConfiguration configuration, string sectionKey)
        {
            return FormatConfigurationSection(configuration, sectionKey, CONNSTRINGKEY);
        }
    }
}
