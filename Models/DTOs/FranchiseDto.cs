using System.Collections.Generic;
using MovieCharacterAPI.Models;

namespace MovieCharacterAPI.Models.DTOs
{
    public class FranchiseDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<MovieDto> Movies { get; set; }
    }
}
