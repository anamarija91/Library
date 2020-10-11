using System;
using System.Globalization;

namespace Library.Core.Utils
{
    public static class Helpers
    {
        /// <summary>
        /// Gets DateTime object from string in given format
        /// </summary>
        /// <param name="date">String value of date </param>
        /// <param name="format">Format of date</param>
        /// <returns></returns>
        public static DateTime GetDateFromString(string date, string format)
        {
            return DateTime.ParseExact(date, format, CultureInfo.InvariantCulture, DateTimeStyles.None);
        }
    }
}
