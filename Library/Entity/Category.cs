using Library.DTOModels.DTOMappers.Converters;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Library.Entity
{
    public class Category
    {
        /// <summary>
        /// Id of the category
        /// </summary>
        [JsonProperty("Id")]
        public string Id { get; set; }

        /// <summary>
        /// Editorial that has this category
        /// </summary>
        [JsonConverter(typeof(EditorialJsonConverter))]
        public virtual Editorial Editorial { get; set; }

        /// <summary>
        /// Name of the category
        /// </summary>
        [JsonProperty("Name")]
        public string Name { get; set; }

        /// <summary>
        /// The books of the category
        /// </summary>
        [JsonConverter(typeof(BookJsonConverter))]
        public virtual ICollection<Book> Books { get; set; }
    }
}