using AutoMapper;
namespace MovieCharacterAPI.Models.DTOs
{
    public class CharacterProfile : Profile
    {
        public CharacterProfile()
        {
            CreateMap<Character, CharacterDto>();
            CreateMap<CharacterCreateDto, Character>();
            CreateMap<CharacterCreateDto, CharacterDto>();
        }
    }
}
