using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MovieCharacterAPI.Models

{
    public class Character
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string FullName { get; set; }

        [Required]
        [MaxLength(50)]
        public string Alias { get; set; }

        [MaxLength(25)]
        public string Gender { get; set; }
        public string CharacterPicture { get; set; }
        public ICollection<Movie> Movies { get; set; }
    }
}
