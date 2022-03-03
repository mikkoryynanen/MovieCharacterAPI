using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieCharacterAPI.Models.DTOs;
using MovieCharacterAPI.Repositories;

namespace MovieCharacterAPI.Controllers
{
    [ApiController]
    [Route("[controller]/")]
    public class MovieController : ControllerBase
    {
        private readonly IMovieRepository _repository;

        public MovieController(IMovieRepository repository)
        {
            _repository = repository;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] MovieCreateDto newMovie)
        {
            try
            {
                if (await _repository.Create(newMovie))
                    return CreatedAtAction("CreateMovie", newMovie);

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
                if (await _repository.Delete(id))
                    return Ok();
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
                var movies = _repository.GetAll();

                if (movies != null)
                    return Ok(movies);
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
                var movie = await _repository.Get(id);
                if (movie != null)
                    return Ok(movie);
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
                if (await _repository.Update(id, updatedMovie))
                    return Ok(updatedMovie);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

            return BadRequest();
        }
    }
}
