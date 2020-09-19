using Library.DBRepositories;
using Library.DTOModels.DTOMappers;
using Library.Entity;
using Library.Models;
using Library.RequestActions;
using System;
using System.Collections.Generic;
namespace Library.RequestManagers
{
    public class CategoryRequestActions : BaseRequestActions, IRequestActions
    {
        /// <summary>
        /// DTO entity.
        /// </summary>
        public DTOCategory Dto { get; set; }

        /// <summary>
        /// Mapper of this entity
        /// </summary>
        private readonly CategoryMapper CMapper;



        public CategoryRequestActions(CustomRequest request, CustomScope customScope)
        {
            Scope = customScope;
            Request = request;
            CMapper = new CategoryMapper();

            if (request.Dto.GetType() == typeof(DTOCategory))
                Dto = (DTOCategory)Convert.ChangeType(request.Dto, typeof(DTOCategory));
            else 
                Dto = null;
        }
        

        /// <summary>
        /// Creates the category
        /// </summary>
        private void CreateCategory()
        {
            Category category = new Category();
            CMapper.MapDTO(category, Dto, UnitOfWork);
            UnitOfWork.CategoryRepository.Create(category);
        }

        /// <summary>
        /// Reverts the creation of the category
        /// </summary>
        private void RevertCreation()
        {
            UnitOfWork.CategoryRepository.Delete(Dto.Id);
        }

        /// <summary>
        /// Updates the category
        /// </summary>
        private void UpdateCategory()
        {
            Category category = UnitOfWork.CategoryRepository.FindById(Dto.Id);
            JsonOfTheEntity = CMapper.CreateJson(category);
            CMapper.MapDTO(category, Dto, UnitOfWork);
            UnitOfWork.CategoryRepository.Update(category);
        }

       
        /// <summary>
        /// Reverts the action of update.
        /// </summary>
        private void RevertUpdate()
        {
            Category category = UnitOfWork.CategoryRepository.FindById(Dto.Id);
            CMapper.MapJson(category, JsonOfTheEntity, UnitOfWork);
            UnitOfWork.CategoryRepository.Update(category);
        }

        /// <summary>
        /// Deletes the category
        /// </summary>
        private void DeleteCategory()
        {
            Category category = UnitOfWork.CategoryRepository.FindById(Request.EntityId);
            JsonOfTheEntity = CMapper.CreateJson(category);
            UnitOfWork.CategoryRepository.Delete(Request.EntityId);
        }

        /// <summary>
        /// Revert the delition
        /// </summary>
        private void RevertDelition()
        {
            Category category = UnitOfWork.CategoryRepository.FindById(Dto.Id);
            if (category == null)
                category = new Category() { Id = Dto.Id };
            CMapper.MapJson(category, JsonOfTheEntity, UnitOfWork);
            UnitOfWork.CategoryRepository.Create(category);
        }


        public override void Process()
        {
            switch (Request.Action)
            {
                case CustomRequest.FLAG_CREATE:
                    Logger.Error("Crear category");
                    CheckEntity(Dto);
                    CreateCategory();
                    break;
                case CustomRequest.FLAG_UPDATE:
                    CheckEntity(Dto);
                    UpdateCategory();
                    break;
                case CustomRequest.FLAG_DELETE:
                    CheckString(Request.EntityId);
                    DeleteCategory();
                    break;
            }
        }

        public override void Revert()
        {
            switch (Request.Action)
            {
                case CustomRequest.FLAG_CREATE:
                    RevertCreation();
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