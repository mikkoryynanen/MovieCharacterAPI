using System.Collections.Generic;

namespace MovieCharacterAPI.Models

{
    public class Character
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Alias { get; set; }
        public string Gender { get; set; }
        public string CharacterPicture { get; set; }
        public ICollection<Movie> Movies { get; set; }
    }
}
