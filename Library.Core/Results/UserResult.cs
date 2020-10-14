using Library.Core.Model.Entities;
using Library.Core.Utils;
using System.Collections.Generic;
using System.Linq;

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

        public IEnumerable<string> Emails { get; set; }
        public IEnumerable<string> Phones { get; set; }

        public MRTDUserResult MRTDdata { get; set; }

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

            Emails = user.UserContact?.Where(uc => uc.Type == ContactType.EMAIL.ToString()).Select(uc => uc.Contact);
            Phones = user.UserContact?.Where(uc => uc.Type == ContactType.PHONE.ToString()).Select(uc => uc.Contact);

            var mrzdata = user.Mrzdata.FirstOrDefault();
            if (mrzdata != null)
            {
                MRTDdata = new MRTDUserResult(mrzdata);
            }
        }
    }
}
