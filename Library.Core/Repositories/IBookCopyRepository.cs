using Library.Core.Model.Entities;

namespace Library.Core.Repositories
{
    /// <summary>
    /// Defines interface for a BookCopy repository.
    /// </summary>
    public interface IBookCopyRepository 
        : IRepository<BookCopy, int>
    {
    }
}
