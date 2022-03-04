using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MovieCharacterAPI.Models;
using MovieCharacterAPI.Models.DTOs;

namespace MovieCharacterAPI.Repositories
{
    public interface ICharactersRepository
    {
        public bool CharacterExists(int id);
        Task<IEnumerable<Character>> GetAllCharactersAsync();
        Task<Character> GetCharacterAsync(int id);
        Task PutCharacterAsync(Character character);
        Task<Character> PostCharacterAsync(Character character);
        Task DeleteCharacterAsync(int id);
    }
}
