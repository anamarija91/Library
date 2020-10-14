using System;

namespace Library.Core.Exceptions
{
    /// <summary>
    /// Defines the base exception class which can be handled by the global exception handling middleware.
    /// </summary>
    public class CustomException
        : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomException"/> class.
        /// </summary>
        /// <param name="statusCode">Status code.</param>
        /// <param name="errorCode">Error code.</param>
        /// <param name="message">Message.</param>
        public CustomException(int statusCode, string errorCode, string message)
            : base(message ?? "N/A")
        {
            StatusCode = statusCode;
            ErrorCode = errorCode ?? "N/A";
        }

        /// <summary>
        /// Gets exception status code.
        /// </summary>
        public int StatusCode { get; }

        /// <summary>
        /// Gets exception error code.
        /// </summary>
        public string ErrorCode { get; }
    }
}
