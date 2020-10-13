using Microsoft.AspNetCore.Http;
using System;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;

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

        /// <summary>
        /// Gets base64 data from file
        /// </summary>
        /// <param name="file">Form file</param>
        /// <returns></returns>
        public static async Task<string> GetBase64StringForImage(IFormFile file)
        {
            using (var ms = new MemoryStream())
            {
                await file.CopyToAsync(ms);
                var fileBytes = ms.ToArray();
                string s = Convert.ToBase64String(fileBytes);
                return s;
            }
        }
    }
}
