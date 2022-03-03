using System;
using System.Linq;
using AutoMapper;
using MovieCharacterAPI.Models.DTOs;

namespace MovieCharacterAPI.Models
{
    public class FranchiseProfile : Profile
    {
        public FranchiseProfile()
        {
            CreateMap<Franchise, FranchiseDto>()
                .ForMember(fdto => fdto.Movies, opt => opt
                .MapFrom(f => f.Movies.Select(m => m.Id).ToList()))
                .ReverseMap();
            CreateMap<Franchise, FranchiseCreateDto>()
                .ReverseMap();
            CreateMap<Franchise, FranchiseUpdateDto>()
                .ReverseMap();
        }
    }
}
