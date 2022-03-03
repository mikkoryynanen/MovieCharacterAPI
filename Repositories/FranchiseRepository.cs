using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieCharacterAPI.Data;
using MovieCharacterAPI.Models;
using MovieCharacterAPI.Models.DTOs;

namespace MovieCharacterAPI.Repositories
{
    public class FranchiseRepository : IFranchiseRepository
    {
        private readonly MovieCharacterAPIDbContext _context;
        private readonly IMapper _mapper;

        public FranchiseRepository(MovieCharacterAPIDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// Check if franchise exists in database
        /// </summary>
        /// <param name="id">Id of franchise</param>
        /// <returns></returns>
        public bool FranchiseExists(int id)
        {
            return _context.Franchises.Any(f => f.Id == id);
        }

        /// <summary>
        /// Get all franchises
        /// </summary>
        /// <returns>All franchises</returns>
        public async Task<IEnumerable<Franchise>> GetAllFranchisesAsync()
        {
            return await _context.Franchises.Include(f => f.Movies).ToListAsync();
        }

        /// <summary>
        /// Get specific franchise by selected id
        /// </summary>
        /// <param name="id">Id of franchise</param>
        /// <returns>Franchise by given id</returns>
        public async Task<Franchise> GetFranchiseAsync(int id)
        {
            return await _context.Franchises.FindAsync(id);
        }

        /// <summary>
        /// Update selected franchise
        /// </summary>
        /// <param name="franchise">Franchise</param>
        /// <returns></returns>
        public async Task PutFranchiseAsync(Franchise franchise)
        {
           _context.Entry(franchise).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Add new franchise to database
        /// </summary>
        /// <param name="franchise">Franchise</param>
        /// <returns></returns>
        public async Task<Franchise> PostFranchiseAsync(Franchise franchise)
        {
            _context.Franchises.Add(franchise);
            await _context.SaveChangesAsync();
            return franchise;
        }

        /// <summary>
        /// Delete selected franchise from database
        /// </summary>
        /// <param name="id">Id of franchise</param>
        /// <returns></returns>
        public async Task DeleteFranchiseAsync(int id)
        {
            var franchise = await _context.Franchises.FindAsync(id);
            _context.Franchises.Remove(franchise);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Update movies in selected franchise
        /// </summary>
        /// <param name="franchiseId">Id of franchise</param>
        /// <param name="movies">List of movies</param>
        /// <returns></returns>
        /// <exception cref="KeyNotFoundException"></exception>
        public async Task UpdateFranchiseMoviesAsync(int franchiseId, List<int> movies)
        {
            Franchise updatedMovies = await _context.Franchises.Include(f => f.Movies).Where(f => f.Id == franchiseId).FirstAsync();
            List<Movie> newMovies = new();

            foreach (int movieId in movies)
            {
                Movie newMovie = await _context.Movies.FindAsync(movieId);

                if (newMovie == null)
                {
                    throw new KeyNotFoundException();
                }

                newMovies.Add(newMovie);
            }

            updatedMovies.Movies = newMovies;
            await _context.SaveChangesAsync();
        }
    }
}
