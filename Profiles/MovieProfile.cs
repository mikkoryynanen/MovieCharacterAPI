using System;
using System.Linq;
using AutoMapper;
using MovieCharacterAPI.Models;

namespace MovieCharacterAPI.Models.DTOs
{
    public class MovieProfile : Profile
    {
        public MovieProfile()
        {
            CreateMap<Movie, MovieDto>()
                .ForMember(mdto => mdto.Characters, opt => opt
                .MapFrom(m => m.Characters.Select(c => c.Id).ToList()))
                .ReverseMap();
            CreateMap<Movie, MovieCreateDto>()
                .ReverseMap();
            CreateMap<Movie, MovieUpdateDto>()
                .ReverseMap();
        }
    }
}
