using Library.DBRepositories.Interfaces;
using Library.DBRepositories.Repos;

namespace Library.DBRepositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private DatabaseContext Contexto { get; }

        public IEditorialRepository EditorialRepository { get; }

        public IBookRepository BookRepository { get; }

        public ICategoryRepository CategoryRepository { get; }

        public UnitOfWork(DatabaseContext context)
        {
            Contexto = context;

            EditorialRepository = new EditorialRepository(Contexto);
            BookRepository = new BookRepository(Contexto);
            CategoryRepository = new CategoryRepository(Contexto);
        }

        public void Dispose()
        {
            Contexto.Dispose();
        }

        public int Complete()
        {
            return Contexto.SaveChanges();
        }
    }
}