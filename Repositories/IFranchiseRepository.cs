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
        public bool FranchiseExists(int id);
        Task<IEnumerable<Franchise>> GetAllFranchisesAsync();
        Task<Franchise> GetFranchiseAsync(int id);
        Task PutFranchiseAsync(Franchise franchise);
        Task<Franchise> PostFranchiseAsync(Franchise franchise);
        Task DeleteFranchiseAsync(int id);
        Task UpdateFranchiseMoviesAsync(int franchiseId, List<int> movies);
        
    }
}
