using Library.DTOModels.DTOMappers;

namespace Library.Models
{
    public class DTOBook : DTOModelBase
    {
        /// <summary>
        /// The name of the book.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The iban of the book.
        /// </summary>
        public string IBAN { get; set; }

        /// <summary>
        /// The price of the book.
        /// </summary>
        public int Price { get; set; }

        /// <summary>
        /// The author of the book.
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        /// The description of the book.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// The category of the book.
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// The editorial of the book.
        /// </summary>
        public string Editorial { get; set; }
    }
}