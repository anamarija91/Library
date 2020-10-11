using System.Collections.Generic;

namespace Library.Core.Exceptions
{
    /// <summary>
    ///  Defines an exception that is thrown when the validation fails.
    /// </summary>
    public class ValidationFailedException 
        : CustomException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationFailedException"/> class.
        /// </summary>
        /// <param name="errors">List of errors.</param>
        public ValidationFailedException(IList<string> errors)
            : base(400, "ValidationFailed", string.Join(" ", errors == null || errors.Count == 0 ? new List<string> { "N/A" } : errors))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationFailedException"/> class.
        /// </summary>
        /// <param name="message">One error.</param>
        public ValidationFailedException(string message)
            : base(400, "ValidationFailed", message)
        {
        }
    }
}
