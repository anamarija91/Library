namespace Library.Core.Exceptions
{
    /// <summary>
    ///  Defines an exception that is thrown when the requested entity has not been found.
    /// </summary>
    public class EntityNotFoundException 
        : CustomException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EntityNotFoundException"/> class.
        /// </summary>
        /// <param name="type">Exception type.</param>
        /// <param name="id">Object.</param>
        public EntityNotFoundException(string type, object id)
            : base(404, "EntityNotFound", $"{type} '{id}' was not found.")
        {
        }
    }
}
