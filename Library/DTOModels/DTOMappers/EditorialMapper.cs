using Library.Entity;
using Library.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Library.DTOModels.DTOConverters
{
    public class EditorialMapper
    {
        public EditorialMapper()
        { }


        /// <summary>
        /// Maps the DTO to the entity.
        /// </summary>
        /// <param name="editorial"> The editorial entity. </param>
        /// <param name="editorialDTO"> The editorial DTO. </param>
        public void MapDTO(Editorial editorial, DTOEditorial editorialDTO)
        {
            editorial.Id = editorialDTO.Id;
            editorial.Name = editorialDTO.Name;
        }

        /// <summary>
        /// Maps the Json to the entity.
        /// </summary>
        /// <param name="editorial"> The editorial entity. </param>
        /// <param name="json"> The json data. </param>
        public void MapJson(Editorial editorial, string json) {
            dynamic jsonObj = JObject.Parse(json);
            editorial.Name = jsonObj.Name;

        }

        /// <summary>
        /// Serializes the object.
        /// </summary>
        /// 
        /// <param name="editorial"> The editorial to serialize. </param>
        /// <returns> String - the json of the entity. </returns>
        public string CreateJson(Editorial editorial)
        {
            return JsonConvert.SerializeObject(editorial);
        }
    }
}