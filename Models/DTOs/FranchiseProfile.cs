using System;
using AutoMapper;
using MovieCharacterAPI.Models.DTOs;

namespace MovieCharacterAPI.Models
{
    public class FranchiseProfile : Profile
    {
        public FranchiseProfile()
        {
            CreateMap<FranchiseDto, Franchise>();
            CreateMap<Franchise, FranchiseDto>();
            CreateMap<FranchiseCreateDto, Franchise>();
        }
    }
}
