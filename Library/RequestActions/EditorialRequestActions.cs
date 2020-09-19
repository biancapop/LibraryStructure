using Library.DBRepositories;
using Library.DTOModels.DTOConverters;
using Library.Entity;
using Library.Models;
using Library.RequestActions;
using System;

namespace Library.RequestManagers
{
    public class EditorialRequestActions : BaseRequestActions, IRequestActions
    {
        /// <summary>
        /// DTO entity.
        /// </summary>
        public DTOEditorial Dto { get; set; }

        /// <summary>
        /// Mapper of this entity
        /// </summary>
        private readonly EditorialMapper EMapper;


        public EditorialRequestActions(CustomRequest request, CustomScope customScope)
        {
            Scope = customScope;
            Request = request;
            EMapper = new EditorialMapper();

            if (request.Dto.GetType() == typeof(DTOEditorial))
                Dto = (DTOEditorial)Convert.ChangeType(request.Dto, typeof(DTOEditorial));
            else
                Dto = null;
        }


        /// <summary>
        /// Creates the new editorial.
        /// </summary>
        private void CreateEditorial()
        {
            Editorial editorial = new Editorial();
            EMapper.MapDTO(editorial, Dto);
            UnitOfWork.EditorialRepository.Create(editorial);
        }

        /// <summary>
        /// Reverts the creation of the editorial.
        /// </summary>
        private void RevertCreation()
        {
            UnitOfWork.EditorialRepository.Delete(Dto.Id);
        }


        /// <summary>
        /// Updates the editotial with the new data.
        /// </summary>
        private void UpdateEditorial()
        {
            Editorial editorial = UnitOfWork.EditorialRepository.FindById(Dto.Id);
            JsonOfTheEntity = EMapper.CreateJson(editorial);
            EMapper.MapDTO(editorial, Dto);
            UnitOfWork.EditorialRepository.Update(editorial);
        }

        /// <summary>
        /// Reverts the update action.
        /// </summary>
        private void RevertUpdate()
        {
            Editorial editorial = UnitOfWork.EditorialRepository.FindById(Dto.Id);
            if (editorial != null && !string.IsNullOrEmpty(JsonOfTheEntity))
            {
                EMapper.MapJson(editorial, JsonOfTheEntity);
                UnitOfWork.EditorialRepository.Update(editorial);
            }
        }

        /// <summary>
        /// Deletes the editorial.
        /// </summary>
        private void DeleteEditorial()
        {
            Editorial editorial = UnitOfWork.EditorialRepository.FindById(Dto.Id);
            JsonOfTheEntity = EMapper.CreateJson(editorial);
            UnitOfWork.EditorialRepository.Delete(Dto.Id);
        }


        /// <summary>
        /// Reverts the delete action.
        /// </summary>
        private void RevertDelition()
        {
            Editorial editorial = UnitOfWork.EditorialRepository.FindById(Dto.Id);
            if (editorial == null)
                editorial = new Editorial() { Id = Dto.Id };
            EMapper.MapJson(editorial, JsonOfTheEntity);
            UnitOfWork.EditorialRepository.Create(editorial);
        }



        public override void Process()
        {
            switch (Request.Action)
            {
                case CustomRequest.FLAG_CREATE:
                    CheckEntity(Dto);
                    CreateEditorial();
                    break;
                case CustomRequest.FLAG_UPDATE:
                    CheckEntity(Dto);
                    UpdateEditorial();
                    break;
                case CustomRequest.FLAG_DELETE:
                    CheckString(Request.EntityId);
                    DeleteEditorial();
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
                    RevertDelition();
                    break;
            }
        }
    }
}