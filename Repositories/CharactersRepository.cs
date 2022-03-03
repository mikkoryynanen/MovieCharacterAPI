using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieCharacterAPI.Data;
using MovieCharacterAPI.Models;
using MovieCharacterAPI.Models.DTOs;

namespace MovieCharacterAPI.Repositories
{
    public class CharactersRepository : ICharactersRepository
    {
        private readonly MovieCharacterAPIDbContext _context;
        private readonly IMapper _mapper;

        public CharactersRepository(MovieCharacterAPIDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<bool> Create(CharacterCreateDto newCharacter)
        {
            try
            {
                _context.Characters.Add(_mapper.Map<Character>(newCharacter));
                bool hasChanges = await _context.SaveChangesAsync() > 0;

                if (hasChanges)
                    return true;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return false;
        }

        public async Task<bool> Delete(int? id)
        {
            try
            {
                var character = await _context.Characters.FirstOrDefaultAsync(c => c.Id == id.Value);
                if (character != null)
                {
                    _context.Characters.Remove(character);
                    bool hasChanges = await _context.SaveChangesAsync() > 0;
                    return hasChanges;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return false;
        }

        public async Task<CharacterDto> Get(int? id)
        {
            try
            {
                var character = await _context.Characters.FirstOrDefaultAsync(c => c.Id == id.Value);

                if (character != null)
                    return _mapper.Map<CharacterDto>(character);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return null;
        }

        public IEnumerable<CharacterDto> GetAll()
        {
            try
            {
                var characters = _context.Characters;
                if (characters != null)
                    return _mapper.Map<IEnumerable<CharacterDto>>(characters);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return null;
        }

        public async Task<bool> Update(int? id, CharacterCreateDto updatedCharacter)
        {
            try
            {
                var character = await _context.Characters.FirstOrDefaultAsync(c => c.Id == id.Value);

                if (character != null)
                {
                   _context.Entry(character).CurrentValues.SetValues(updatedCharacter);
                    _context.Entry(character).State = EntityState.Modified;

                    bool hasChanges = await _context.SaveChangesAsync() > 0;
                    return hasChanges;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return false;
        }
    }
}
