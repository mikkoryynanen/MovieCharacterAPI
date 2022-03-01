using System;
using AutoMapper;
using MovieCharacterAPI.Models.DTOs;

namespace MovieCharacterAPI.Models
{
    public class FranchiseProfile : Profile
    {
        public FranchiseProfile()
        {
            CreateMap<Franchise, FranchiseDto>()
                .ReverseMap();
            CreateMap<FranchiseCreateDto, Franchise>()
                .ReverseMap();
        }
    }
}
