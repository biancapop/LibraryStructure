using Library.DTOModels;

namespace Library.Entity
{
    public class CustomRequest
    {

        public const int FLAG_NULL = 0;
        public const int FLAG_CREATE = 1;
        public const int FLAG_UPDATE = 2;
        public const int FLAG_DELETE = 3;

        /// <summary>
        /// Request id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Action that has to be made
        /// </summary>
        public int Action { get; set; }

        /// <summary>
        /// The generic dto 
        /// </summary>
        public DTOGeneric Dto { get; set; }

        /// <summary>
        /// The entity id
        /// </summary>
        public string EntityId { get; set; }

        /// <summary>
        /// The editorial id
        /// </summary>
        public string EditorialId { get; set; }


        
        public CustomRequest(string id, int flag, DTOGeneric dtoEntity, string idEditorial)
        {
            Id = id;
            Action = flag;
            Dto = dtoEntity;
            EntityId = null;
            EditorialId = idEditorial;
        }


        public CustomRequest(string id, int flag, string idEntity, DTOGeneric dtoEntity, string idEditorial)
        {
            Id = id;
            Action = flag;
            EntityId = idEntity;
            Dto = dtoEntity;
            EditorialId = idEditorial;
        }

        /// <summary>
        /// Returns the action that has to be made.
        /// </summary>
        /// <param name="action"> Flag number. </param>
        /// <returns>String - name of the action.</returns>
        public static string GetAction(int action)
        {
            if (action == FLAG_CREATE)
                return "create";
            if (action == FLAG_UPDATE)
                return "update";
            if (action == FLAG_DELETE)
                return "delete";
            return "null";
        }
    }
}
