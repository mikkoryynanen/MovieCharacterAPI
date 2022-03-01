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
    [Route("[controller]/")]
    public class FranchiseController : ControllerBase
    {
        private readonly MovieCharacterAPIDbContext _context;
        private readonly IMapper _mapper;

        public FranchiseController(MovieCharacterAPIDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] FranchiseCreateDto newFranchise)
        {
            try
            {
                _context.Franchises.Add(_mapper.Map<Franchise>(newFranchise));
                bool hasChanges = await _context.SaveChangesAsync() > 0;

                if (hasChanges)
                    return CreatedAtAction(nameof(Get), newFranchise);

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
                var franchise = await _context.Franchises.FirstOrDefaultAsync(f => f.Id == id.Value);
                if (franchise != null)
                {
                    _context.Franchises.Remove(franchise);
                    bool hasChanges = await _context.SaveChangesAsync() > 0;
                    if (hasChanges)
                        return Ok($"Franchise with id {id.Value} deleted");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

            return BadRequest();
        }

        [HttpGet("all")]
        public ActionResult<FranchiseDto[]> GetAll()
        {
            try
            {
                var franchises = _context.Franchises;

                if (franchises != null)
                    return Ok(_mapper.Map<FranchiseDto[]>(franchises));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

            return NotFound("Francises not found");
        }

        [HttpGet("{id?}")]
        public async Task<ActionResult<FranchiseDto>> Get(int? id)
        {
            try
            {
                var franchise = await _context.Franchises.FirstOrDefaultAsync(f => f.Id == id.Value);

                if (franchise != null)
                    return Ok(_mapper.Map<FranchiseDto>(franchise));                
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

            return NotFound($"Could not find franchise with ID {id.Value}");
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<FranchiseDto>> Update(int? id, [FromBody] FranchiseDto updatedFranchise)
        {
            try
            {
                var franchise = await _context.Franchises.FirstOrDefaultAsync(f => f.Id == id.Value);
                if(franchise != null)
                {
                    franchise.Description = updatedFranchise.Description;
                    franchise.Name = updatedFranchise.Name;
                    //franchise.Movies = updatedFranchise.Movies;

                    bool hasChanges = await _context.SaveChangesAsync() > 0;
                    if (hasChanges)
                        return Ok(_mapper.Map<FranchiseDto>(franchise));
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

        //[HttpPut("update-movies/{id}")]
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
    }
}
