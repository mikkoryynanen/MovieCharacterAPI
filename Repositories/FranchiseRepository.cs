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
        /// Get all franchises
        /// </summary>
        /// <returns>All franchises</returns>
        [HttpGet]
        public async Task<IEnumerable<FranchiseDto>> GetAllFranchises()
        {
            var list = await _context.Franchises.ToListAsync();
            return _mapper.Map<IEnumerable<FranchiseDto>>(list);
        }

        /// <summary>
        /// Get specific franchise by selected id
        /// </summary>
        /// <param name="id">Id of franchise</param>
        /// <returns>Franchise by given id</returns>
        [HttpGet("{id}")]
        public async Task<FranchiseDto> GetFranchise(int id)
        {
            var franchise = await _context.Franchises.FindAsync(id);
            if (franchise == null)
            {
                return null;
            }

            var franchiseRead = _mapper.Map<FranchiseDto>(franchise);
            return franchiseRead;
        }

        /// <summary>
        /// Update selected franchise
        /// </summary>
        /// <param name="id">Id of franchise</param>
        /// <param name="franchiseUpdate">Franchise</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<bool> PutFranchise(int id, FranchiseUpdateDto franchiseUpdate)
        {
            if (id != franchiseUpdate.Id)
            {
                return false;
            }

            Franchise domainFranchise = _mapper.Map<Franchise>(franchiseUpdate);
            _context.Entry(franchiseUpdate).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FranchiseExists(id))
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }
        }

        /// <summary>
        /// Add new franchise to database
        /// </summary>
        /// <param name="franchiseCreate">Franchise</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<FranchiseDto> PostFranchise(FranchiseCreateDto franchiseCreate)
        {
            Franchise domainFranchise = _mapper.Map<Franchise>(franchiseCreate);
            _context.Franchises.Add(domainFranchise);
            await _context.SaveChangesAsync();

            return _mapper.Map<FranchiseDto>(domainFranchise);
        }

        /// <summary>
        /// Delete selected franchise from database
        /// </summary>
        /// <param name="id">Id of franchise</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<bool> DeleteFranchise(int id)
        {
            var franchise = await _context.Franchises.FindAsync(id);
            if (franchise == null)
            {
                return false;
            }

            _context.Franchises.Remove(franchise);
            await _context.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// Update movies in franchise
        /// </summary>
        /// <param name="id">Id of franchise</param>
        /// <param name="movies">List of movies</param>
        /// <returns></returns>
        [HttpPut("{id}/movies")]
        public async Task<bool> UpdateFranchiseMovies(int id, [FromBody] List<int> movies)
        {
            if (!FranchiseExists(id))
            {
                return false;
            }

            Franchise franchiseMovies = await _context.Franchises.Include(f => f.Movies).Where(f => f.Id == id).FirstAsync();

            List<Movie> newMovies = new();
            foreach (int movieId in movies)
            {
                Movie newMovie = await _context.Movies.FindAsync(movieId);
                if (newMovie == null)
                {
                    return false;
                }

                newMovies.Add(newMovie);
            }

            franchiseMovies.Movies = newMovies;

            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }

        /// <summary>
        /// Check if franchise exists in database
        /// </summary>
        /// <param name="id">Id of franchise</param>
        /// <returns></returns>
        bool FranchiseExists(int id)
        {
            return _context.Franchises.Any(f => f.Id == id);
        }
    }
}
