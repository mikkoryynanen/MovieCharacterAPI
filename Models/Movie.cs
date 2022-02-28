using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MovieCharacterAPI.Models

{
    public class Movie
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string MovieTitle { get; set; }

        [MaxLength(25)]
        public string Genre { get; set; }

        [MaxLength(10)]
        public string ReleaseYear { get; set; }

        [MaxLength(50)]
        public string Director { get; set; }
        public string MoviePicture { get; set; }
        public string Trailer { get; set; }
        public ICollection<Character> Characters { get; set; }
        public int FranchiseId { get; set; }
        public Franchise Franchise { get; set; }
    }
}
