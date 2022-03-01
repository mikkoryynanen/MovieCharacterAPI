using System;
using AutoMapper;
using MovieCharacterAPI.Models;

namespace MovieCharacterAPI.Models.DTOs
{
    public class MovieProfile : Profile
    {
        public MovieProfile()
        {
            CreateMap<Movie, MovieDto>();
            CreateMap<MovieCreateDto, Movie>();
        }
    }
}
