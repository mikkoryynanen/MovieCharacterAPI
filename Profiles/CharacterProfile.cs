using AutoMapper;
namespace MovieCharacterAPI.Models.DTOs
{
    public class CharacterProfile : Profile
    {
        public CharacterProfile()
        {
            CreateMap<Character, CharacterDto>()
                .ReverseMap();
            CreateMap<Character, CharacterCreateDto>()
                .ReverseMap();
            CreateMap<Character, CharacterUpdateDto>()
                .ReverseMap();
        }
    }
}
