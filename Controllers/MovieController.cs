using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieCharacterAPI.Models;
using MovieCharacterAPI.Models.DTOs;
using MovieCharacterAPI.Repositories;

namespace MovieCharacterAPI.Controllers
{
    [ApiController]
    [Route("[controller]/")]
    public class MovieController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IMovieRepository _repository;

        public MovieController(IMapper mapper, IMovieRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        /// <summary>
        /// Get all movies from database
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MovieDto>>> GetAllMovies()
        {
            return _mapper.Map<List<MovieDto>>(await _repository.GetAllMoviesAsync());
        }

        /// <summary>
        /// Get selected movie from database by id
        /// </summary>
        /// <param name="id">Id of movie</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<MovieDto>> GetMovie(int id)
        {
            Movie movie = await _repository.GetMovieAsync(id);

            if (movie == null)
            {
                return NotFound();
            }

            return _mapper.Map<MovieDto>(movie);
        }

        /// <summary>
        /// Update selected movie
        /// </summary>
        /// <param name="id">Id of movie</param>
        /// <param name="movieUpdate">Movie update DTO</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMovie(int id, MovieUpdateDto movieUpdate)
        {
            if (id != movieUpdate.Id)
            {
                return BadRequest();
            }

            if (!_repository.MovieExists(id))
            {
                return NotFound();
            }

            Movie movieDomain = _mapper.Map<Movie>(movieUpdate);
            await _repository.PutMovieAsync(movieDomain);
            return NoContent();
        }

        /// <summary>
        /// Add new movie to database
        /// </summary>
        /// <param name="movieCreate"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> PostMovie(MovieCreateDto movieCreate)
        {
            Movie movieDomain = _mapper.Map<Movie>(movieCreate);
            movieDomain = await _repository.PostMovieAsync(movieDomain);
            return CreatedAtAction("GetMovie", new { id = movieDomain.Id }, _mapper.Map<MovieDto>(movieDomain));
        }

        /// <summary>
        /// Delete movie from database
        /// </summary>
        /// <param name="id">Id of movie</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            if (!_repository.MovieExists(id))
            {
                return NotFound();
            }

            await _repository.DeleteMovieAsync(id);
            return NoContent();
        }

        /// <summary>
        /// Update character in selected movie
        /// </summary>
        /// <param name="id">Id of movie</param>
        /// <param name="charactersList">List of characters</param>
        /// <returns></returns>
        [HttpPut("{id}/characters")]
        public async Task<IActionResult> UpdateMovieCharacters(int id, List<int> charactersList)
        {
            if (!_repository.MovieExists(id))
            {
                return NotFound();
            }

            try
            {
                await _repository.UpdateMovieCharactersAsync(id, charactersList);
            }
            catch (KeyNotFoundException)
            {
                return BadRequest();
            }

            return NoContent();
        }

        /// <summary>
        /// Get characters from selected movie
        /// </summary>
        /// <param name="id">Id of movie</param>
        /// <returns></returns>
        [HttpGet("{id}/characters")]
        public async Task<ActionResult<IEnumerable<CharacterDto>>> GetMovieCharacters(int id)
        {
            if (!_repository.MovieExists(id))
            {
                return NotFound();
            }

            return _mapper.Map<List<CharacterDto>>(await _repository.GetMovieCharactersAsync(id));
        }
    }
}
