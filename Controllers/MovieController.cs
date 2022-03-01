using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieCharacterAPI.Data;
using MovieCharacterAPI.Models;
using MovieCharacterAPI.Models.DTOs;

namespace MovieCharacterAPI.Controllers
{
    [ApiController]
    [Route("[controller]/")]
    public class MovieController : ControllerBase
    {
        private readonly MovieCharacterAPIDbContext _context;
        private readonly IMapper _mapper;

        public MovieController(MovieCharacterAPIDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] MovieCreateDto newMovie)
        {
            try
            {
                bool hasFranchise = await _context.Movies.FirstOrDefaultAsync(f => f.Id == newMovie.FranchiseId) != null;
                if (!hasFranchise)
                    return BadRequest($"Franchise with id {newMovie.FranchiseId} does not exist");

                _context.Movies.Add(_mapper.Map<Movie>(newMovie));
                bool hasChanges = await _context.SaveChangesAsync() > 0;

                if (hasChanges)
                    return CreatedAtAction("Create", newMovie);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

            return BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int? id)
        {
            try
            {
                var movie = await _context.Movies.FirstOrDefaultAsync(m => m.Id == id.Value);
                if (movie != null)
                {
                    _context.Movies.Remove(movie);
                    bool hasChanges = await _context.SaveChangesAsync() > 0;
                    return Ok();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

            return BadRequest();
        }

        [HttpGet("all")]
        public ActionResult<MovieDto[]> GetAll()
        {
            try
            {
                var movies = _context.Movies;

                if (movies != null)
                    return Ok(_mapper.Map<MovieDto[]>(movies));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

            return NotFound("Movies not found");
        }

        [HttpGet("{id?}")]
        public async Task<ActionResult<MovieDto>> Get(int? id)
        {
            try
            {
                var movie = await _context.Movies.FirstOrDefaultAsync(f => f.Id == id.Value);

                if (movie != null)
                    return Ok(_mapper.Map<MovieDto>(movie));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

            return NotFound($"Could not find Movie with ID {id.Value}");
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<MovieDto>> Update(int? id, [FromBody] MovieCreateDto updatedMovie)
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

                        bool hasChanges = await _context.SaveChangesAsync() > 0;
                        if (hasChanges)
                            return Ok(_mapper.Map<MovieDto>(movie));
                    }
                    else
                    {
                        return BadRequest($"Could not find Franchise for movie '{updatedMovie.MovieTitle}' with id {updatedMovie.FranchiseId}");
                    }
                }
                else
                {
                    return NotFound($"Did not find Franchise with id {id.Value}");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

            return BadRequest();
        }
    }
}
