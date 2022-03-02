using System;
using System.Collections;
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
    [Route("api/[controller]")]
    public class FranchiseController : ControllerBase
    {
        private readonly MovieCharacterAPIDbContext _context;
        private readonly IMapper _mapper;

        public FranchiseController(MovieCharacterAPIDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        //[HttpGet("all")]
        //public ActionResult<FranchiseDto[]> GetAll()
        //{
        //    try
        //    {
        //        var franchises = _context.Franchises;

        //        if (franchises != null)
        //            return Ok(_mapper.Map<FranchiseDto[]>(franchises));
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        //    }

        //    return NotFound("Francises not found");
        //}

        /// <summary>
        /// Get all franchises
        /// </summary>
        /// <returns>All franchises</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Franchise>>> GetAllFranchises()
        {
            return await _context.Franchises.ToListAsync();
        }

        //[HttpGet("{id?}")]
        //public async Task<ActionResult<FranchiseDto>> Get(int? id)
        //{
        //    try
        //    {
        //        var franchise = await _context.Franchises.FirstOrDefaultAsync(f => f.Id == id.Value);

        //        if (franchise != null)
        //            return Ok(_mapper.Map<FranchiseDto>(franchise));                
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        //    }

        //    return NotFound($"Could not find franchise with ID {id.Value}");
        //}

        /// <summary>
        /// Get specific franchise by selected id
        /// </summary>
        /// <param name="id">Id of franchise</param>
        /// <returns>Franchise by given id</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<FranchiseDto>> GetFranchise(int id)
        {
            var franchise = await _context.Franchises.FindAsync(id);
            if (franchise == null)
            {
                return NotFound();
            }

            var franchiseRead = _mapper.Map<FranchiseDto>(franchise);
            return franchiseRead;
        }

        //[HttpPut("{id}")]
        //public async Task<ActionResult<FranchiseDto>> Update(int? id, [FromBody] FranchiseDto updatedFranchise)
        //{
        //    try
        //    {
        //        var franchise = await _context.Franchises.FirstOrDefaultAsync(f => f.Id == id.Value);
        //        if(franchise != null)
        //        {
        //            franchise.Description = updatedFranchise.Description;
        //            franchise.Name = updatedFranchise.Name;
        //            //franchise.Movies = updatedFranchise.Movies;

        //            bool hasChanges = await _context.SaveChangesAsync() > 0;
        //            if (hasChanges)
        //                return Ok(_mapper.Map<FranchiseDto>(franchise));
        //        }
        //        else
        //        {
        //            return NotFound($"Did not find Franchise with id {id.Value}");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        //    }

        //    return BadRequest();
        //}

        /// <summary>
        /// Update selected franchise
        /// </summary>
        /// <param name="id">Id of franchise</param>
        /// <param name="franchiseUpdate">Franchise</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFranchise(int id, FranchiseUpdateDto franchiseUpdate)
        {
            if (id != franchiseUpdate.Id)
            {
                return BadRequest();
            }

            Franchise domainFranchise = _mapper.Map<Franchise>(franchiseUpdate);
            _context.Entry(franchiseUpdate).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FranchiseExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        //[HttpPost]
        //public async Task<IActionResult> Create([FromBody] FranchiseCreateDto newFranchise)
        //{
        //    try
        //    {
        //        _context.Franchises.Add(_mapper.Map<Franchise>(newFranchise));
        //        bool hasChanges = await _context.SaveChangesAsync() > 0;

        //        if (hasChanges)
        //            return CreatedAtAction(nameof(Get), newFranchise);

        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        //    }

        //    return BadRequest();
        //}

        /// <summary>
        /// Add new franchise to database
        /// </summary>
        /// <param name="franchiseCreate">Franchise</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<Franchise>> PostFranchise(FranchiseCreateDto franchiseCreate)
        {
            Franchise domainFranchise = _mapper.Map<Franchise>(franchiseCreate);
            _context.Franchises.Add(domainFranchise);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFranchise", new { id = domainFranchise.Id }, _mapper.Map<FranchiseDto>(domainFranchise));
        }

        //[HttpDelete("{id}")]
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    try
        //    {
        //        var franchise = await _context.Franchises.FirstOrDefaultAsync(f => f.Id == id.Value);
        //        if (franchise != null)
        //        {
        //            _context.Franchises.Remove(franchise);
        //            bool hasChanges = await _context.SaveChangesAsync() > 0;
        //            if (hasChanges)
        //                return Ok($"Franchise with id {id.Value} deleted");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        //    }

        //    return BadRequest();
        //}

        /// <summary>
        /// Delete selected franchise from database
        /// </summary>
        /// <param name="id">Id of franchise</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFranchise(int id)
        {
            var franchise = await _context.Franchises.FindAsync(id);
            if (franchise == null)
            {
                return NotFound();
            }

            _context.Franchises.Remove(franchise);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        //[HttpPut("{id}/movies")]
        //public async Task<ActionResult<FranchiseDto>> UpdateMovies(int? id, [FromBody] int[] movieIds)
        //{
        //    try
        //    {
        //        var franchise = _context.Franchises.FirstOrDefaultAsync(f => f.Id == id.Value);
        //        if (franchise != null)
        //        {
        //            var listOfMovieIds = franchise.MovieIds.ToList();
        //            listOfMovieIds.AddRange(movieIds);
        //            franchise.MovieIds = listOfMovieIds.ToArray();

        //            //foreach (var movieId in movieIds)
        //            //{
        //            //    franchise.MovieIds.Add(movieId);
        //            //}

        //            bool hasChanges = await _context.SaveChangesAsync() > 0;
        //            if (hasChanges)
        //                return Ok(_mapper.Map<FranchiseDto>(franchise));
        //        }
        //        else
        //        {
        //            return NotFound($"Did not find Franchise with id {id.Value}");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        //    }

        //    return BadRequest();
        //}

        /// <summary>
        /// Update movies in franchise
        /// </summary>
        /// <param name="id">Id of franchise</param>
        /// <param name="movies">List of movies</param>
        /// <returns></returns>
        [HttpPut("{id}/movies")]
        public async Task<IActionResult> UpdateFranchiseMovies(int id, List<int> movies)
        {
            if (!FranchiseExists(id))
            {
                return NotFound();
            }

            Franchise franchiseMovies = await _context.Franchises.Include(f => f.Movies).Where(f => f.Id == id).FirstAsync();

            List<Movie> newMovies = new();
            foreach (int movieId in movies)
            {
                Movie newMovie = await _context.Movies.FindAsync(movieId);
                if (newMovie == null)
                {
                    return BadRequest();
                }

                newMovies.Add(newMovie);
            }

            franchiseMovies.Movies = newMovies;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return NoContent();
        }

        /// <summary>
        /// Check if franchise exists in database
        /// </summary>
        /// <param name="id">Id of franchise</param>
        /// <returns></returns>
        private bool FranchiseExists(int id)
        {
            return _context.Franchises.Any(f => f.Id == id);
        }
    }
}
