using Library.DBRepositories;
using Library.Entity;
using Library.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace Library.DTOModels.DTOMappers
{
    public class BookMapper : BaseMapper
    {
        public BookMapper()
        {
        }

        /// <summary>
        /// Serializes the object.
        /// </summary>
        /// 
        /// <param name="book"> The book to serialize. </param>
        /// <returns> String - the json of the entity. </returns>
        public string CreateJson(Book book)
        {
            return JsonConvert.SerializeObject(book);
        }

        /// <summary>
        /// Maps the JSON to the entity.
        /// </summary>
        ///
        /// <param name="book"> The book entity. </param>
        /// <param name="json"> Json to be mapped. </param>
        /// <param name="unitOfWork"> UnitOfWork repository. </param>
        public void MapJson(Book book, string json, UnitOfWork unitOfWork)
        {
            dynamic jsonObj = JObject.Parse(json);

            if (string.IsNullOrEmpty(book.Id))
                book.Id = jsonObj.id;
            book.Name = jsonObj.Name;
            book.IBAN = jsonObj.IBAN;
            book.Author = jsonObj.Author;
            book.Description = jsonObj.Description;
            FillPrice(book, json);
            FillEditorial(book, jsonObj, unitOfWork);
            FillCategory(book, jsonObj, unitOfWork);

        }

        /// <summary>
        /// Fills the price of the book from the json.
        /// </summary>
        /// 
        /// <param name="book"> Book whose price has to be filled. </param>
        /// <param name="json"> The json data of the book. </param>
        private void FillPrice(Book book, dynamic json)
        {
            if (!DynamicIsEmpty(json.Price))
                book.Price = Int32.Parse(json.Price.ToString());
            else
                book.Price = 0;
        }

        /// <summary>
        /// Fills the category of the book from the json.
        /// </summary>
        /// 
        /// <param name="book"> Book whose category has to be filled. </param>
        /// <param name="json"> The json data of the book. </param>
        /// <param name="unitOfWork"> Repository access. </param>
        private void FillCategory(Book book, dynamic json, UnitOfWork unitOfWork)
        {
            if (!DynamicIsEmpty(json.Category))
                book.Category = unitOfWork.CategoryRepository.FindById(json.Category.ToString());
        }


        /// <summary>
        /// Fills the editorial of the book from the json.
        /// </summary>
        /// 
        /// <param name="book"> Book whose editorial has to be filled. </param>
        /// <param name="json"> The json data of the book. </param>
        /// <param name="unitOfWork"> Repository access. </param>
        private void FillEditorial(Book book, dynamic json, UnitOfWork unitOfWork)
        {
            if (!DynamicIsEmpty(json.Editorial))
            {
                book.Editorial = unitOfWork.EditorialRepository.FindById(json.Editorial.ToString());
            }
        }


        /// <summary>
        /// Mapps the DTO to the entity.
        /// </summary>
        /// <param name="book"> The book entity</param>
        /// <param name="bookDto"> The DTO </param>
        /// <param name="unitOfWork"> Repositories Access</param>
        public void MapDTO(Book book, DTOBook bookDto, UnitOfWork unitOfWork)
        {
            if (string.IsNullOrEmpty(book.Id))
                book.Id = bookDto.Id;
            book.Name = bookDto.Name;
            book.IBAN = bookDto.IBAN;
            book.Author = bookDto.Author;
            book.Description = bookDto.Description;
            book.Price = bookDto.Price;

            FillEditorial(book, bookDto.IdEditorial, unitOfWork);
            FillCategory(book, bookDto.Category, unitOfWork);
        }

        /// <summary>
        /// Fills the category from the DTO.
        /// </summary>
        /// 
        /// <param name="book"> Book whose category has to be filled. </param>
        /// <param name="editorialId"> The if of the category. </param>
        /// <param name="unitOfWork"> Repository access. </param>
        private void FillCategory(Book book, string categoryId, UnitOfWork unitOfWork)
        {
            if (!string.IsNullOrEmpty(categoryId))
            {
                book.Category = unitOfWork.CategoryRepository.FindById(categoryId.ToString());
            }
        }


        /// <summary>
        /// Fills the editorial from the DTO.
        /// </summary>
        /// 
        /// <param name="book"> Book whose editorial has to be filled. </param>
        /// <param name="editorialId"> The if of the editorial. </param>
        /// <param name="unitOfWork"> Repository access. </param>
        private void FillEditorial(Book book, string editorialId, UnitOfWork unitOfWork)
        {
            if (!string.IsNullOrEmpty(editorialId))
            {
                book.Editorial = unitOfWork.EditorialRepository.FindById(editorialId);
            }
        }


    }
}