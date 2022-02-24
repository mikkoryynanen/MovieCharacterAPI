using System.Collections.Generic;

namespace MovieCharacterAPI.Models

{
    public class Movie
    {
        public int Id { get; set; }
        public string MovieTitle { get; set; }
        public string Genre { get; set; }
        public string ReleaseYear { get; set; }
        public string Director { get; set; }
        public string MoviePicture { get; set; }
        public string Trailer { get; set; }
        public ICollection<Character> Characters { get; set; }
        public int FranchiseId { get; set; }
        public Franchise Franchise { get; set; }
    }
}
