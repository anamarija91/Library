using Library.Core.Exceptions;
using System;
using System.ComponentModel;
using System.Globalization;

namespace Library.Core.Utils
{
    /// <summary>
    /// Defines type utility class.
    /// </summary>
    public static class TypeUtility
    {
        /// <summary>
        /// Get converter for a certain type
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static TypeConverter GetTypeConverter(Type type) => TypeDescriptor.GetConverter(type);

        /// <summary>
        /// Parses 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parseString"></param>
        /// <exception cref="InternalConflictException">When type converter is not supported</exception>
        /// <returns></returns>
        public static T ParseFromString<T>(string parseString)
        {
            var converter = GetTypeConverter(typeof(T));

            try
            {
                return (T)converter.ConvertFromString(null, CultureInfo.InvariantCulture, parseString);
            }
            catch (NotSupportedException ex)
            {
                throw new InternalConflictException(ex.Message);
            }
        }
    }
}
