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
using MovieCharacterAPI.Repositories;

namespace MovieCharacterAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FranchiseController : ControllerBase
    {
        private readonly IFranchiseRepository _repository;

        public FranchiseController(IFranchiseRepository repositoy)
        {
            _repository = repositoy;
        }

        /// <summary>
        /// Get all franchises
        /// </summary>
        /// <returns>All franchises</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FranchiseDto>>> GetAllFranchises()
        {
            return Ok(await _repository.GetAllFranchises());
        }

        ///// <summary>
        ///// Get specific franchise by selected id
        ///// </summary>
        ///// <param name="id">Id of franchise</param>
        ///// <returns>Franchise by given id</returns>
        //[HttpGet("{id}")]
        //public async Task<ActionResult<FranchiseDto>> GetFranchise(int id)
        //{
        //    var franchise = await _context.Franchises.FindAsync(id);
        //    if (franchise == null)
        //    {
        //        return NotFound();
        //    }

        //    var franchiseRead = _mapper.Map<FranchiseDto>(franchise);
        //    return franchiseRead;
        //}

        ///// <summary>
        ///// Update selected franchise
        ///// </summary>
        ///// <param name="id">Id of franchise</param>
        ///// <param name="franchiseUpdate">Franchise</param>
        ///// <returns></returns>
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutFranchise(int id, FranchiseUpdateDto franchiseUpdate)
        //{
        //    if (id != franchiseUpdate.Id)
        //    {
        //        return BadRequest();
        //    }

        //    Franchise domainFranchise = _mapper.Map<Franchise>(franchiseUpdate);
        //    _context.Entry(franchiseUpdate).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!FranchiseExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        ///// <summary>
        ///// Add new franchise to database
        ///// </summary>
        ///// <param name="franchiseCreate">Franchise</param>
        ///// <returns></returns>
        //[HttpPost]
        //public async Task<ActionResult<Franchise>> PostFranchise(FranchiseCreateDto franchiseCreate)
        //{
        //    Franchise domainFranchise = _mapper.Map<Franchise>(franchiseCreate);
        //    _context.Franchises.Add(domainFranchise);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetFranchise", new { id = domainFranchise.Id }, _mapper.Map<FranchiseDto>(domainFranchise));
        //}

        ///// <summary>
        ///// Delete selected franchise from database
        ///// </summary>
        ///// <param name="id">Id of franchise</param>
        ///// <returns></returns>
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteFranchise(int id)
        //{
        //    var franchise = await _context.Franchises.FindAsync(id);
        //    if (franchise == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Franchises.Remove(franchise);
        //    await _context.SaveChangesAsync();
        //    return NoContent();
        //}

        ///// <summary>
        ///// Update movies in franchise
        ///// </summary>
        ///// <param name="id">Id of franchise</param>
        ///// <param name="movies">List of movies</param>
        ///// <returns></returns>
        //[HttpPut("{id}/movies")]
        //public async Task<IActionResult> UpdateFranchiseMovies(int id, [FromBody] List<int> movies)
        //{
        //    if (!FranchiseExists(id))
        //    {
        //        return NotFound();
        //    }

        //    Franchise franchiseMovies = await _context.Franchises.Include(f => f.Movies).Where(f => f.Id == id).FirstAsync();

        //    List<Movie> newMovies = new();
        //    foreach (int movieId in movies)
        //    {
        //        Movie newMovie = await _context.Movies.FindAsync(movieId);
        //        if (newMovie == null)
        //        {
        //            return BadRequest($"Could not find movie with id {movieId}");
        //        }

        //        newMovies.Add(newMovie);
        //    }

        //    franchiseMovies.Movies = newMovies;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        throw;
        //    }

        //    return NoContent();
        //}

        ///// <summary>
        ///// Check if franchise exists in database
        ///// </summary>
        ///// <param name="id">Id of franchise</param>
        ///// <returns></returns>
        //private bool FranchiseExists(int id)
        //{
        //    return _context.Franchises.Any(f => f.Id == id);
        //}
    }
}
