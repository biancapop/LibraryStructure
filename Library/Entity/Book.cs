using Library.DTOModels.DTOMappers.Converters;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Library.Entity
{
    public class Book
    {
        /// <summary>
        /// Id of the book
        /// </summary>
        [Key]
        [JsonProperty("Id")]
        public string Id { get; set; }

        /// <summary>
        /// Name of the book
        /// </summary>
        [JsonProperty("Name")]
        public string Name { get; set; }

        /// <summary>
        /// Iban of the book
        /// </summary>
        [JsonProperty("IBAN")]
        public string IBAN { get; set; }

        /// <summary>
        /// Price of the book
        /// </summary>
        [JsonProperty("Price")]
        public int Price { get; set; }

        /// <summary>
        /// Author of the book
        /// </summary>
        [JsonProperty("Author")]
        public string Author { get; set; }

        /// <summary>
        /// Description of the book
        /// </summary>
        [JsonProperty("Description")]
        public string Description { get; set; }

        /// <summary>
        /// Category of the book
        /// </summary>
        [JsonConverter(typeof(CategoryJsonConverter))]
        public virtual Category Category { get; set; }

        /// <summary>
        /// The id of the category used for the database relationship
        /// </summary>
        [JsonIgnore]
        public string IdCategory { get; set; }

        /// <summary>
        /// Editorial which has this book
        /// </summary>
        [JsonConverter(typeof(EditorialJsonConverter))]
        public virtual Editorial Editorial { get; set; }
    }
}