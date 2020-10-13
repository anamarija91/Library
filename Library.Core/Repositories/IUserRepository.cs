using Library.Core.Model.Entities;

namespace Library.Core.Repositories
{
    /// <summary>
    /// Defines user repository interface
    /// </summary>
    public interface IUserRepository
        : IRepository<User, int>
    {
    }
}
