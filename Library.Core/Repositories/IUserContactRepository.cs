using Library.Core.Model.Entities;

namespace Library.Core.Repositories
{
    /// <summary>
    /// Defines user contact repository interface
    /// </summary>
    public interface IUserContactRepository
        : IRepository<UserContact, int>
    {
    }
}
