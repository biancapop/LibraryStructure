using Library.DBRepositories.Interfaces;
using Library.Entity;
using System.Collections.Generic;
using System.Linq;

namespace Library.DBRepositories.Repos
{
    public class BookRepository : IBookRepository
    {
        private readonly DatabaseContext Context;

        public BookRepository(DatabaseContext contexto)
        {
            Context = contexto;
        }

        public void Create(Book book)
        {
            if (FindById(book.Id) == null && book.Editorial != null)
            {
                Context.Books.Add(book);
                Context.SaveChanges();
            }
        }

        public void Update(Book book)
        {
            if (FindById(book.Id) != null && book.Editorial != null)
            {
                Context.Books.Update(book);
                Context.SaveChanges();
            }
        }

        public void Delete(Book book)
        {
            if (book != null)
            {
                Context.Books.Remove(book);
                Context.SaveChanges();
            }
        }

        public void DeleteByEditorial(string idEditorial)
        {
            List<Book> books = FindByEditorial(idEditorial);
            foreach (Book book in books)
            {
                Delete(book);
            }
        }

        public Book FindById(string idBook)
        {
            return Context.Books
                .Where(f => f.Id.Equals(idBook))
                .FirstOrDefault();
        }

        public List<Book> FindByEditorial(string idEditorial)
        {
            return Context.Books
                .Where(sb => sb.Editorial.Id.Equals(idEditorial))
                .ToList();
        }

        public List<Book> FindByCategory(string idCategory)
        {
            return Context.Books
                .Where(f => f.Category.Id.Equals(idCategory))
                .ToList();
        }
    }
}