using Library.Entity;
using System.Collections.Generic;

namespace Library.DBRepositories.Interfaces
{
    public interface IBookRepository
    {
        /// <summary>
        /// Creates the new book.
        /// </summary>
        /// <param name="book"> The book to be created.</param>
        void Create(Book book);

        /// <summary>
        /// Updates the book.
        /// </summary>
        /// <param name="book">The book to be updated.</param>
        void Update(Book book);

        /// <summary>
        /// Deletes the book.
        /// </summary>
        ///
        /// <param name="book">The book to be deleted.</param>
        void Delete(Book book);

        /// <summary>
        /// Deletes all the books of an editorial.
        /// </summary>
        /// <param name="idEditorial">The id of the editorial.</param>
        void DeleteByEditorial(string idEditorial);

        /// <summary>
        /// Searches all the books of a category.
        /// </summary>
        ///
        /// <param name="idCategory">The id of the category.</param>
        /// <returns></returns>
        List<Book> FindByCategory(string idCategory);

        /// <summary>
        /// Searches a book by its id.
        /// </summary>
        /// <param name="id">The id of the book.</param>
        /// <returns> The book if found or null.</returns>
        Book FindById(string id);

        /// <summary>
        /// Finds all the books by the editorial.
        /// </summary>
        /// <param name="idEditorial">The id of the editorial.</param>
        /// <returns>A list of books.</returns>
        List<Book> FindByEditorial(string idEditorial);
    }
}