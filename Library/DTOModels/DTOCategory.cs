using Library.DTOModels.DTOMappers;

namespace Library.Models
{
    public class DTOCategory : DTOModelBase
    {
        public string Name { get; set; }
        public string[] Books { get; set; }
    }
}