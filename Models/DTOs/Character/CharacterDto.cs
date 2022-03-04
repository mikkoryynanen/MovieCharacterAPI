using System;
using System.Collections.Generic;

namespace MovieCharacterAPI.Models.DTOs
{
    public class CharacterDto
    {
        public string FullName { get; set; }
        public string Alias { get; set; }
        public string Gender { get; set; }
        public string CharacterPicture { get; set; }
    }
}
