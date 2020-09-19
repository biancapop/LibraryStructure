using Library.Entity;
using System.Collections.Generic;

namespace Library.DBRepositories.Interfaces
{
    public interface ICategoryRepository
    {
        /// <summary>
        /// Creates the category in the database.
        /// </summary>
        /// <param name="category">The category to be created.</param>
        void Create(Category category);

        /// <summary>
        /// Updates the category.
        /// </summary>
        ///
        /// <param name="category">The category to be created.</param>
        void Update(Category category);

        /// <summary>
        /// Deletes the category.
        /// </summary>
        /// <param name="idCategory">The category id.</param>
        void Delete(string idCategory);

        /// <summary>
        /// Finds the category by its id.
        /// </summary>
        /// <param name="idCategory">The id of the category.</param>
        /// <returns>The category if found or null.</returns>
        Category FindById(string idCategory);

        /// <summary>
        /// Searches all the category by its editorial.
        /// </summary>
        /// <param name="idEditorial">The editorial's id.</param>
        /// <returns>A list of categories.</returns>
        List<Category> FindByEditorial(string idEditorial);
    }
}