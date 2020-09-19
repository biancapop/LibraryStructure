using Library.DTOModels;

namespace Library.Models
{
    public class DTOEditorial : DTOGeneric
    {
        /// <summary>
        /// The id of the editorial.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The name of the editorial.
        /// </summary>
        public string Name { get; set; }
    }
}