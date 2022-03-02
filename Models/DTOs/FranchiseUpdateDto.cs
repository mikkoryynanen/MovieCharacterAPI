using System.ComponentModel.DataAnnotations;

namespace MovieCharacterAPI.Models.DTOs
{
    public class FranchiseUpdateDto
    {
        public int Id { get; set; }

        [MaxLength(50)]
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
