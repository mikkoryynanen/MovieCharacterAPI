using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MovieCharacterAPI.Data;
using MovieCharacterAPI.Models;
using MovieCharacterAPI.Models.DTOs;

namespace MovieCharacterAPI.Repositories
{
    public class MovieRepository : IMovieRepository
    {
        private readonly MovieCharacterAPIDbContext _context;

        public MovieRepository(MovieCharacterAPIDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Check if movie exists in  database
        /// </summary>
        /// <param name="id">Id of movie</param>
        /// <returns></returns>
        public bool MovieExists(int id)
        {
            return _context.Movies.Any(m => m.Id == id);
        }

        /// <summary>
        /// Get all movies from database
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Movie>> GetAllMoviesAsync()
        {
            return await _context.Movies.Include(m => m.Characters).ToListAsync();
        }

        /// <summary>
        /// Get selected movie from database by id
        /// </summary>
        /// <param name="id">Id of movie</param>
        /// <returns></returns>
        public async Task<Movie> GetMovieAsync(int id)
        {
        return  await _context.Movies.FindAsync(id);
        }

        /// <summary>
        /// Update selected movie
        /// </summary>
        /// <param name="movie">Movie</param>
        /// <returns></returns>
        public async Task PutMovieAsync(Movie movie)
        {
            _context.Entry(movie).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Add new movie to database
        /// </summary>
        /// <param name="movie">Movie</param>
        /// <returns></returns>
        public async Task<Movie> PostMovieAsync(Movie movie)
        {
            _context.Movies.Add(movie);
            await _context.SaveChangesAsync();
            return movie;
        }

        /// <summary>
        /// Delete movie from database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task DeleteMovieAsync(int id)
        {
            var movie = await _context.Movies.FindAsync(id);
            _context.Movies.Remove(movie);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Update characters in selected movie
        /// </summary>
        /// <param name="movieId">Id of movie</param>
        /// <param name="characters">List of characters</param>
        /// <returns></returns>
        /// <exception cref="KeyNotFoundException"></exception>
        public async Task UpdateMovieCharactersAsync(int movieId, List<int> characters)
        {
            Movie updatedCharacters = await _context.Movies.Include(m => m.Characters).Where(m => m.Id == movieId).FirstAsync();
            List<Character> newCharacters = new();

            foreach (var characterId in characters)
            {
                Character newCharacter = await _context.Characters.FindAsync(characterId);

                if (newCharacter == null)
                {
                    throw new KeyNotFoundException();
                }

                newCharacters.Add(newCharacter);
            }

            updatedCharacters.Characters = newCharacters;
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Get characters from selected movie
        /// </summary>
        /// <param name="id">Id of movie</param>
        /// <returns></returns>
        public async Task<IEnumerable<Character>> GetMovieCharactersAsync(int movieId)
        {
            Movie movie = await _context.Movies.Include(m => m.Characters).Where(m => m.Id == movieId).FirstAsync();
            return movie.Characters;
        }
    }
}
