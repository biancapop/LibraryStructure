using Library.Entity;
using System.Collections.Generic;

namespace Library.DBRepositories.Interfaces
{
    public interface IEditorialRepository
    {
        /// <summary>
        /// Updates the editorial.
        /// </summary>
        ///
        /// <param name="editorial"> The editorial entity. </param>
        /// <returns> The updated editorial. </returns>
        Editorial Update(Editorial editorial);

        /// <summary>
        /// Creates the editorial.
        /// </summary>
        ///
        /// <param name="editorial"> The editorial entity. </param>
        /// <returns> The created editorial. </returns>
        Editorial Create(Editorial editorial);

        /// <summary>
        /// Searches an editorial by its id.
        /// </summary>
        ///
        /// <param name="idEditorial"> The id of the editorial. </param>
        /// <returns> The searched editorial. </returns>
        Editorial FindById(string idEditorial);

        /// <summary>
        /// Deletes the editorial.
        /// </summary>
        ///
        /// <param name="id"> The id of the editorial. </param>
        void Delete(string id);

        /// <summary>
        /// Gets all the editorials.
        /// </summary>
        ///
        /// <returns> The editorials from the database. </returns>
        List<Editorial> GetAll();
    }
}