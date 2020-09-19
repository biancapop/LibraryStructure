using System;

namespace Library.DBRepositories.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IEditorialRepository EditorialRepository { get; }
        IBookRepository BookRepository { get; }
        ICategoryRepository CategoryRepository { get; }

        int Complete();
    }
}