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
using MovieCharacterAPI.Repositories;

namespace MovieCharacterAPI.Controllers
{
    [ApiController]
    [Route("[controller]/")]
    public class CharactersController : ControllerBase
    {
        private readonly ICharactersRepository _repository;

        public CharactersController(ICharactersRepository repository)
        {
            _repository = repository;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CharacterCreateDto newCharacter)
        {
            try
            {
                if (await _repository.Create(newCharacter))
                    return CreatedAtAction("CreateCharacter", newCharacter);

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
                    return Ok($"Character with id {id.Value} deleted");
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

            return BadRequest();
        }

        [HttpGet("all")]
        public ActionResult<IEnumerable<CharacterDto>> GetAll()
        {
            try
            {
                var characters = _repository.GetAll();
                if(characters != null)
                    return Ok(characters);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

            return NotFound("Movies not found");
        }

        [HttpGet("{id?}")]
        public async Task<ActionResult<CharacterDto>> Get(int? id)
        {
            try
            {
                var character = await _repository.Get(id);
                if (character != null)
                    return Ok(character);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

            return NotFound($"Could not find Character with ID {id.Value}");
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<CharacterDto>> Update(int? id, [FromBody] CharacterCreateDto updatedCharacter)
        {
            try
            {
                if (await _repository.Update(id, updatedCharacter))
                {
                    return Ok();
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
