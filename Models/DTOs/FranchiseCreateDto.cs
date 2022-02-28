using System.ComponentModel.DataAnnotations;

namespace MovieCharacterAPI.Models.DTOs
{
    public class FranchiseCreateDto
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
