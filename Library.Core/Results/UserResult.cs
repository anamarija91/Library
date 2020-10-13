using Library.Core.Model.Entities;
using Library.Core.Utils;

namespace Library.Core.Results
{
    /// <summary>
    /// Defines user result class.
    /// </summary>
    public class UserResult
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DateOfBirth { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Initialize new instance of <see cref="UserResult"/> class.
        /// Copies data from User entity.
        /// </summary>
        /// <param name="user">User entity</param>
        public UserResult(User user)
        {
            Id = user.Id;
            FirstName = user.FirstName;
            LastName = user.LastName;
            DateOfBirth = user.DateOfBirth.Date.ToString(ProjectConstants.DateFormat);
            Email = user.Email;
            PhoneNumber = user.PhoneNumber;
        }
    }
}
