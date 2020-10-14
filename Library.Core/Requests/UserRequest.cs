using System.Collections.Generic;

namespace Library.Core.Requests
{
    /// <summary>
    /// Defines user request.
    /// </summary>
    public class UserRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DateOfBirth { get; set; }
        public IEnumerable<string> Emails { get; set; }
        public IEnumerable<string> Phones { get; set; }
    }

    /// <summary>
    /// Defines user create request.
    /// </summary>
    public class UserCreateRequest
        : UserRequest
    { }

    /// <summary>
    /// Defines user update request.
    /// </summary>
    public class UserUpdateRequest
        : UserRequest
    { }
}
