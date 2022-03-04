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
        private readonly IMapper _mapper;
        private readonly ICharactersRepository _repository;

        public CharactersController(IMapper mapper ,ICharactersRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }
        
        /// <summary>
        /// Get all characters from database
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CharacterDto>>> GetAllCharacters()
        {
            return _mapper.Map<List<CharacterDto>>(await _repository.GetAllCharactersAsync());
        }

        /// <summary>
        /// Get selected character from database by id
        /// </summary>
        /// <param name="id">Id of character</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<CharacterDto>> GetCharacter(int id)
        {
            Character character = await _repository.GetCharacterAsync(id);

            if (character == null)
            {
                return NotFound();
            }

            return _mapper.Map<CharacterDto>(character);
        }

        /// <summary>
        /// Update selecter character
        /// </summary>
        /// <param name="id">Id of character</param>
        /// <param name="characterUpdate">Character update DTO</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCharacter(int id, CharacterUpdateDto characterUpdate)
        {
            if (id != characterUpdate.Id)
            {
                return BadRequest();
            }

            if (!_repository.CharacterExists(id))
            {
                return NotFound();
            }

            Character characterDomain = _mapper.Map<Character>(characterUpdate);
            await _repository.PutCharacterAsync(characterDomain);
            return NoContent();
        }

        /// <summary>
        /// Add new character to database
        /// </summary>
        /// <param name="characterCreate">Character create DTO</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<Character>> PostCharacter(CharacterCreateDto characterCreate)
        {
            Character characterDomain = _mapper.Map<Character>(characterCreate);
            characterDomain = await _repository.PostCharacterAsync(characterDomain);
            return CreatedAtAction("GetCharacter", new { id = characterDomain.Id }, _mapper.Map<CharacterDto>(characterDomain));
        }

        /// <summary>
        /// Delete selected character from database
        /// </summary>
        /// <param name="id">Id of character</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCharacter(int id)
        {
            if (!_repository.CharacterExists(id))
            {
                return NotFound();
            }

            await _repository.DeleteCharacterAsync(id);
            return NoContent();
        }
    }
}
