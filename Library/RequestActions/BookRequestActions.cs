using Library.DBRepositories;
using Library.DTOModels.DTOMappers;
using Library.Entity;
using Library.Models;
using Library.RequestActions;
using System;
using System.Collections.Generic;

namespace Library.RequestManagers
{
    public class BookRequestActions : BaseRequestActions, IRequestActions
    {
        /// <summary>
        /// DTO entity.
        /// </summary>
        public DTOBook Dto { get; set; }


        /// <summary>
        /// Mapper of this entity.
        /// </summary>
        private readonly BookMapper BMapper;


        public BookRequestActions(CustomRequest request, CustomScope customScope)
        {
            Scope = customScope;
            Request = request;
            BMapper = new BookMapper();

            if (request.Dto.GetType() == typeof(DTOBook))
                Dto = (DTOBook)Convert.ChangeType(request.Dto, typeof(DTOBook));
            else
                Dto = null;
        }

        /// <summary>
        /// Creates the new book.
        /// </summary>
        private void CreateBook()
        {
            Book book = new Book();
            BMapper.MapDTO(book, Dto, UnitOfWork);
            if (book.Editorial != null)
                UnitOfWork.BookRepository.Create(book);
            else
                throw new ArgumentException("The book has no editorial.");

        }

        /// <summary>
        /// Reverts the creation of the book.
        /// </summary>
        private void RevertirCreacion()
        {
            UnitOfWork.BookRepository.Delete(UnitOfWork.BookRepository.FindById(Dto.Id));
        }

        /// <summary>
        /// Deletes the book.
        /// </summary>
        private void DeleteBook()
        {
            Book book = UnitOfWork.BookRepository.FindById(Request.EntityId);
            JsonOfTheEntity = BMapper.CreateJson(book);
            UnitOfWork.BookRepository.Delete(book);
        }

        /// <summary>
        /// Reverts the deletion.
        /// </summary>
        private void RevertDelition()
        {
            Book book = new Book();
            BMapper.MapJson(book, JsonOfTheEntity, UnitOfWork);
            UnitOfWork.BookRepository.Create(book);
        }

        /// <summary>
        /// Updates the book.
        /// </summary>
        private void UpdateBook()
        {
            Book book = UnitOfWork.BookRepository.FindById(Dto.Id);
            JsonOfTheEntity = BMapper.CreateJson(book);
            BMapper.MapDTO(book, Dto, UnitOfWork);
            UnitOfWork.BookRepository.Update(book);
        }

        private void RevertUpdate()
        {
            Book book = UnitOfWork.BookRepository.FindById(Dto.Id);
            BMapper.MapJson(book, JsonOfTheEntity, UnitOfWork);
            UnitOfWork.BookRepository.Update(book);
        }


        public override void Process()
        {
            switch (Request.Action)
            {
                case CustomRequest.FLAG_CREATE:
                    CheckEntity(Dto);
                    CreateBook();
                    break;
                case CustomRequest.FLAG_UPDATE:
                    CheckEntity(Dto);
                    UpdateBook();
                    break;
                case CustomRequest.FLAG_DELETE:
                    CheckString(Request.EntityId);
                    DeleteBook();
                    break;
            }
        }

        public override void Revert()
        {
            switch (Request.Action)
            {
                case CustomRequest.FLAG_CREATE:
                    RevertirCreacion();
                    break;
                case CustomRequest.FLAG_UPDATE:
                    CheckString(JsonOfTheEntity);
                    RevertUpdate();
                    break;
                case CustomRequest.FLAG_DELETE:
                    CheckString(JsonOfTheEntity);
                    RevertDelition();
                    break;
            }
        }
    }
}