using Newtonsoft.Json;

namespace Library.DTOModels.DTOMappers
{
    public class DTOModelBase:DTOGeneric
    {
        /// <summary>
        /// The id of the entity
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The id of the editorial
        /// </summary>
        public string IdEditorial { get; set; }


        /// <summary>
        /// Returns the serialized object.
        /// </summary>
        /// 
        /// <returns>String - the serialized object.</returns>
        public string ToCustomString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
