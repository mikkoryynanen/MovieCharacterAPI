using System.ComponentModel.DataAnnotations;

namespace MovieCharacterAPI.Models.DTOs
{
    public class CharacterUpdateDto
    {
        public int Id { get; set; }
        [MaxLength(50)]
        public string FullName { get; set; }
        [MaxLength(50)]
        public string Alias { get; set; }
        [MaxLength(25)]
        public string Gender { get; set; }
        public string CharacterPicture { get; set; }
    }
}
