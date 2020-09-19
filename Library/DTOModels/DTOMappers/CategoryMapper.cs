using Library.DBRepositories;
using Library.Entity;
using Library.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;

namespace Library.DTOModels.DTOMappers
{
    public class CategoryMapper : BaseMapper
    {
        public CategoryMapper()
        {
        }

        /// <summary>
        /// Serializes the object.
        /// </summary>
        /// 
        /// <param name="category"> The category to serialize. </param>
        /// <returns> String - the json of the entity. </returns>
        public string CreateJson(Category category)
        {
            return JsonConvert.SerializeObject(category);
        }

        /// <summary>
        /// Maps the JSON to the entity.
        /// </summary>
        ///
        /// <param name="category"> The category entity. </param>
        /// <param name="json"> Json to be mapped. </param>
        /// <param name="unitOfWork"> UnitOfWork repository. </param>
        public void MapJson(Category category, string json, UnitOfWork unitOfWork)
        {
            dynamic jsonObj = JObject.Parse(json);
            category.Id = jsonObj.id;
            category.Name = jsonObj.Name;

            FillEditorial(category, jsonObj, unitOfWork);
        }

        /// <summary>
        /// Fills the editorial of the entity.
        /// </summary>
        /// 
        /// <param name="category"> The category entity. </param>
        /// <param name="json"> Json to be mapped. </param>
        /// <param name="unitOfWork"> UnitOfWork repository. </param>
        private void FillEditorial(Category category, dynamic json, UnitOfWork unitOfWork)
        {
            if (!DynamicIsEmpty(json.Editorial))
                category.Editorial = unitOfWork.CategoryRepository.FindById(json.Editorial.ToString());
        }



        /// <summary>
        /// Mapps the DTO to the entity.
        /// </summary>
        /// <param name="category"> The category entity</param>
        /// <param name="categoryDTO"> The DTO </param>
        /// <param name="unitOfWork"> Repositories Access</param>
        public void MapDTO(Category category, DTOCategory categoryDTO, UnitOfWork unitOfWork)
        {
            category.Id = categoryDTO.Id;
            category.Name = categoryDTO.Name;
            FillBooks(category, categoryDTO.IdEditorial, categoryDTO.Books, unitOfWork);
            FillEditorial(category, categoryDTO.IdEditorial, unitOfWork);
        }

        /// <summary>
        /// Fills the books in the category.
        /// </summary>
        /// 
        /// <param name="category"> The category entity </param>
        /// <param name="idEditorial"> The editorial id</param>
        /// <param name="idsBooks">The list of ids of the books</param>
        /// <param name="unitOfWork">Repositories access </param>
        private void FillBooks(Category category, string idEditorial, string[] idsBooks, UnitOfWork unitOfWork)
        {
            List<Book> books = unitOfWork.BookRepository.FindByEditorial(idEditorial);
            if (books.Count() > 0 && idsBooks.Count() > 0)
            {
                category.Books = new List<Book>();
                foreach (string idBook in idsBooks)
                    category.Books.Add(books.Where(f => f.Id == idBook).FirstOrDefault());
            }
        }


        /// <summary>
        /// Fills the editorial of the category.
        /// </summary>
        /// <param name="category"> The category entity </param>
        /// <param name="idEditorial"> The editorial id</param>
        /// <param name="unitOfWork">Repositories access </param>
        private void FillEditorial(Category category, string idEditorial, UnitOfWork unitOfWork)
        {
            category.Editorial = unitOfWork.EditorialRepository.FindById(idEditorial);
        }
    }
}