using Library.Core.Model.Entities;
using Library.Core.Requests;
using Library.Core.UnitsOfWork;
using Library.Core.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Core.Services
{
    /// <summary>
    /// Defines user contact service class.
    /// </summary>
    public class UserContactService : ServiceBase, IUserContactService
    {
        /// <summary>
        /// Initilizes new instance of <see cref="UserContactService"/>
        /// </summary>
        /// <param name="unitOfWork">Unit of work.</param>
        public UserContactService(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {

        }

        /// <inheritdoc/>
        public async Task UpdateUserContacts(User user, UserRequest request)
        {
            var emailContacts = CreateUserContactList(request.Emails, ContactType.EMAIL, user);
            var phoneContacts = CreateUserContactList(request.Phones, ContactType.PHONE, user);

            user.UserContact = emailContacts.Concat(phoneContacts).ToList();

            UnitOfWork.Users.Update(user);
            await UnitOfWork.Commit();
        }

        /// <summary>
        /// Creates new UserContact list from request contacts for a contact type
        /// If contact already exist in DB add id to list, if contact is new add new contact
        /// </summary>
        /// <param name="contacts">String list of contact values that should be assigned to user</param>
        /// <param name="type">Contact type</param>
        /// <returns></returns>
        private List<UserContact> CreateUserContactList(IEnumerable<string> contacts, ContactType type, User user)
        {
            var newContacts = new List<UserContact>();

            foreach (var contact in contacts)
            {
                var userContact = user.UserContact.FirstOrDefault(uc => uc.Contact.Equals(contact, StringComparison.OrdinalIgnoreCase) && uc.Type == type.ToString());

                newContacts.Add(userContact ??= new UserContact
                {
                    Type = type.ToString(),
                    Contact = contact.ToLower()
                });
            }

            return newContacts;
        }
    }
}
