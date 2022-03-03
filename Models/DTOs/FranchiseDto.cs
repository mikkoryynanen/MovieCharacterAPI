using System.Collections.Generic;
using MovieCharacterAPI.Models;

namespace MovieCharacterAPI.Models.DTOs
{
    public class FranchiseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<int> Movies { get; set; }
    }
}
