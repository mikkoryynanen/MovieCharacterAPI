using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MovieCharacterAPI.Models;
using MovieCharacterAPI.Models.DTOs;

namespace MovieCharacterAPI.Repositories
{
    public interface IFranchiseRepository
    {
        Task<IEnumerable<FranchiseDto>> GetAllFranchises();
        Task<FranchiseDto> GetFranchise(int id);
        Task<bool> PutFranchise(int id, FranchiseUpdateDto franchiseUpdate);
        Task<FranchiseDto> PostFranchise(FranchiseCreateDto franchiseCreate);
        Task<bool> DeleteFranchise(int id);
        Task<bool> UpdateFranchiseMovies(int id, [FromBody] List<int> movies);
    }
}
