using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MovieCharacterAPI.Models

{
    public class Franchise
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<Movie> Movies { get; set; }
    }
}
