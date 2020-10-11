
namespace Library.Core.Exceptions
{
    /// <summary>
    /// Defines exception that is thrown when there is a request conflict with current state of the target resource
    /// </summary>
    public class InternalConflictException 
        : CustomException
    {
        /// <summary>
        /// Initailizes new instance of <see cref="InternalConflictException"/>
        /// </summary>
        /// <param name="message">Exception message</param>
        public InternalConflictException(string message) : base(409, "InternalConflict", message) { }
    }
}
