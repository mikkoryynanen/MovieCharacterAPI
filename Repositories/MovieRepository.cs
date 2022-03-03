using System;
using System.Collections.Generic;
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
        private readonly IMapper _mapper;

        public MovieRepository(MovieCharacterAPIDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<bool> Create(MovieCreateDto newMovie)
        {
            try
            {
                bool hasFranchise = await _context.Movies.FirstOrDefaultAsync(f => f.Id == newMovie.FranchiseId) != null;
                if (!hasFranchise)
                    return false;

                _context.Movies.Add(_mapper.Map<Movie>(newMovie));
                bool hasChanges = await _context.SaveChangesAsync() > 0;
                return hasChanges;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return false;
        }

        public async Task<bool> Delete(int? id)
        {
            try
            {
                var movie = await _context.Movies.FirstOrDefaultAsync(m => m.Id == id.Value);
                if (movie != null)
                {
                    _context.Movies.Remove(movie);
                    bool hasChanges = await _context.SaveChangesAsync() > 0;
                    return hasChanges;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return false;
        }

        public async Task<MovieDto> Get(int? id)
        {
            try
            {
                var movie = await _context.Movies.FirstOrDefaultAsync(f => f.Id == id.Value);

                if (movie != null)
                    return _mapper.Map<MovieDto>(movie);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return null;
        }

        public IEnumerable<MovieDto> GetAll()
        {
            try
        {
                var movies = _context.Movies;

                if (movies != null)
                    return _mapper.Map<IEnumerable<MovieDto>>(movies);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return null;
        }

        public async Task<bool> Update(int? id, MovieCreateDto updatedMovie)
        {
            try
            {
                var movie = await _context.Movies.FirstOrDefaultAsync(f => f.Id == id.Value);
                if (movie != null)
                {
                    bool hasFranchise = await _context.Franchises.FirstOrDefaultAsync(f => f.Id == updatedMovie.FranchiseId) != null;

                    if (hasFranchise)
                    {
                        _context.Entry(movie).CurrentValues.SetValues(updatedMovie);
                        _context.Entry(movie).State = EntityState.Modified;

                        bool hasChanges = await _context.SaveChangesAsync() > 0;
                        return hasChanges;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return false;
        }
    }
}
