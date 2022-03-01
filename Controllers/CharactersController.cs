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
    public class CharactersController : ControllerBase
    {
        private readonly MovieCharacterAPIDbContext _context;
        private readonly IMapper _mapper;

        public CharactersController(MovieCharacterAPIDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CharacterCreateDto newCharacter)
        {
            try
            {
                _context.Characters.Add(_mapper.Map<Character>(newCharacter));
                bool hasChanges = await _context.SaveChangesAsync() > 0;

                if (hasChanges)
                    return CreatedAtAction("Create", newCharacter);

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
                var character = await _context.Characters.FirstOrDefaultAsync(c => c.Id == id.Value);
                if (character != null)
                {
                    _context.Characters.Remove(character);
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
        public ActionResult<CharacterDto[]> GetAll()
        {
            try
            {
                var characters = _context.Characters;

                if (characters != null)
                    return Ok(_mapper.Map<CharacterDto[]>(characters));
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
                var character = await _context.Characters.FirstOrDefaultAsync(c => c.Id == id.Value);

                if (character != null)
                    return Ok(_mapper.Map<CharacterDto>(character));
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
                var character = await _context.Characters.FirstOrDefaultAsync(c => c.Id == id.Value);

                if (character != null)
                {
                    _context.Entry(character).CurrentValues.SetValues(updatedCharacter);

                    bool hasChanges = await _context.SaveChangesAsync() > 0;
                    if (hasChanges)
                        return Ok(_mapper.Map<CharacterDto>(character));
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
