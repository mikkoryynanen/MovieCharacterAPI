using System.Collections.Generic;

namespace MovieCharacterAPI.Models.DTOs
{
    public class FranchiseDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<Movie> Movies { get; set; }
    }
}
