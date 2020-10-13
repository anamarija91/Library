using Library.Core.Requests;
using Library.Core.Utils;
using System.Collections.Generic;

namespace Library.Core.Model.Entities
{
    public partial class User
    {
        public User(UserCreateRequest request)
        {
            UpdateUserData(request);

            Rental = new HashSet<Rental>();
        }

        public void UpdateUserData(UserRequest request)
        {
            FirstName = request.FirstName;
            LastName = request.LastName;
            Email = request.Email;
            PhoneNumber = request.PhoneNumber;
            DateOfBirth = Helpers.GetDateFromString(request.DateOfBirth, ProjectConstants.DateFormat);
        }
    }
}
