using Newtonsoft.Json;
using System.Collections.Generic;

namespace Library.Entity
{
    public class Editorial
    {
        /// <summary>
        /// Id of the editorial
        /// </summary>
        [JsonProperty("Id")]
        public string Id { get; set; }

        /// <summary>
        /// The name of the editorial
        /// </summary>
        [JsonProperty("Name")]
        public string Name { get; set; }

        /// <summary>
        /// The books that the editorial has
        /// </summary>
        [JsonIgnore]
        public virtual ICollection<Book> Books { get; set; }

        /// <summary>
        /// The categories that the editorial has
        /// </summary>
        [JsonIgnore]
        public virtual ICollection<Category> Categories { get; set; }
    }
}