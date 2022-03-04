using System;
using System.Collections.Generic;
using System.Linq;
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

        public CharactersRepository(MovieCharacterAPIDbContext context)
        {
            _context = context;
        }
        
        /// <summary>
        /// Check if character exists in database
        /// </summary>
        /// <param name="id">Id of character</param>
        /// <returns></returns>
        public bool CharacterExists(int id)
        {
            return _context.Characters.Any(c => c.Id == id);
        }

        /// <summary>
        /// Get all characters from database
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Character>> GetAllCharactersAsync()
        {
            return await _context.Characters.ToListAsync();
        }

        /// <summary>
        /// Get selected character from database by id
        /// </summary>
        /// <param name="id">Id of character</param>
        /// <returns></returns>
        public async Task<Character> GetCharacterAsync(int id)
        {
            return await _context.Characters.FindAsync(id);
        }

        /// <summary>
        /// Update selected character
        /// </summary>
        /// <param name="character">Character</param>
        /// <returns></returns>
        public async Task PutCharacterAsync(Character character)
        {
            _context.Entry(character).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Add new character to database
        /// </summary>
        /// <param name="character">Character</param>
        /// <returns></returns>
        public async Task<Character> PostCharacterAsync(Character character)
        {
            _context.Characters.Add(character);
            await _context.SaveChangesAsync();
            return character;
        }

        /// <summary>
        /// Delete selected character from database
        /// </summary>
        /// <param name="id">Id of character</param>
        /// <returns></returns>
        public async Task DeleteCharacterAsync(int id)
        {
            var character = await _context.Characters.FindAsync(id);
            _context.Characters.Remove(character);
            await _context.SaveChangesAsync();
        }
    }
}
