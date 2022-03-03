using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MovieCharacterAPI.Models.DTOs;

namespace MovieCharacterAPI.Repositories
{
    public interface ICharactersRepository
    {
        Task<bool> Create(CharacterCreateDto newCharacter);
        Task<bool> Delete(int? id);
        IEnumerable<CharacterDto> GetAll();
        Task<CharacterDto> Get(int? id);
        Task<bool> Update(int? id, CharacterCreateDto updatedCharacter);

    }
}
