using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
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
    [Route("[controller]/")]
    public class FranchiseController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IFranchiseRepository _repository;

        public FranchiseController(IMapper mapper, IFranchiseRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        /// <summary>
        /// Get all franchises
        /// </summary>
        /// <returns>All franchises</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FranchiseDto>>> GetAllFranchises()
        {
            return _mapper.Map<List<FranchiseDto>>(await _repository.GetAllFranchisesAsync());
        }

        ///// <summary>
        ///// Get specific franchise by selected id
        ///// </summary>
        ///// <param name="id">Id of franchise</param>
        ///// <returns>Franchise by given id</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<FranchiseDto>> GetFranchise(int id)
        {
            Franchise franchise = await _repository.GetFranchiseAsync(id);
            if (franchise == null)
            {
                return NotFound();
            }

            return _mapper.Map<FranchiseDto>(franchise);
        }

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

            if (!_repository.FranchiseExists(id))
            {
                return NotFound();
            }

            Franchise franchiseDomain = _mapper.Map<Franchise>(franchiseUpdate);
            await _repository.PutFranchiseAsync(franchiseDomain);
            return NoContent();
        }

        /// <summary>
        /// Add new franchise to database
        /// </summary>
        /// <param name="franchiseCreate">Franchise create DTO</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<Franchise>> PostFranchise(FranchiseCreateDto franchiseCreate)
        {
            Franchise franchiseDomain = _mapper.Map<Franchise>(franchiseCreate);
            franchiseDomain = await _repository.PostFranchiseAsync(franchiseDomain);
            return CreatedAtAction("GetFranchise", new { id = franchiseDomain.Id }, _mapper.Map<FranchiseDto>(franchiseDomain));
        }

        /// <summary>
        /// Delete selected franchise from database
        /// </summary>
        /// <param name="id">Id of franchise</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFranchise(int id)
        {
            if (!_repository.FranchiseExists(id))
            {
                return NotFound();
            }

            await _repository.DeleteFranchiseAsync(id);
            return NoContent();
        }

        /// <summary>
        /// Update movies in selected franchise
        /// </summary>
        /// <param name="id">Id of franchise</param>
        /// <param name="moviesList">List of movies</param>
        /// <returns></returns>
        [HttpPut("{id}/movies")]
        public async Task<IActionResult> UpdateFranchiseMovies(int id, List<int> moviesList)
        {
            if (!_repository.FranchiseExists(id))
            {
                return NotFound();
            }

            try
            {
                await _repository.UpdateFranchiseMoviesAsync(id, moviesList);
            }
            catch (KeyNotFoundException)
            {
                return BadRequest();
            }

            return NoContent();
        }

        /// <summary>
        /// Get characters from selected franchise
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}/characters")]
        public async Task<ActionResult<IEnumerable<CharacterDto>>> GetFranchiseCharacters(int id)
        {
            if (!_repository.FranchiseExists(id))
            {
                return NotFound();
            }

            return _mapper.Map<List<CharacterDto>>(await _repository.GetFranchiseCharactersAsync(id));
        }

    }
}
