using System.Collections.Generic;
using System.Threading.Tasks;
using MovieCharacterAPI.Models.DTOs;

namespace MovieCharacterAPI.Repositories
{
    public interface IMovieRepository
    {
        Task<bool> Create(MovieCreateDto newMovie);
        Task<bool> Delete(int? id);
        IEnumerable<MovieDto> GetAll();
        Task<MovieDto> Get(int? id);
        Task<bool> Update(int? id, MovieCreateDto updatedMovie);
    }
}
