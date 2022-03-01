using System;
using System.Collections.Generic;

namespace MovieCharacterAPI.Models.DTOs
{
    public class MovieDto
    {
        public string MovieTitle { get; set; }

        public string Genre { get; set; }

        public string ReleaseYear { get; set; }
                
        public string Director { get; set; }
        public string MoviePicture { get; set; }
        public string Trailer { get; set; }
        //public ICollection<CharacterDto> Characters { get; set; }
        public int FranchiseId { get; set; }
        //public Franchise Franchise { get; set; }
    }
}
