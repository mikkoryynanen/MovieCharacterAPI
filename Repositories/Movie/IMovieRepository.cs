using System.Collections.Generic;
using System.Threading.Tasks;
using MovieCharacterAPI.Models;
using MovieCharacterAPI.Models.DTOs;

namespace MovieCharacterAPI.Repositories
{
    public interface IMovieRepository
    {
        public bool MovieExists(int id);
        Task<IEnumerable<Movie>> GetAllMoviesAsync();
        Task<Movie> GetMovieAsync(int id);
        Task PutMovieAsync(Movie movie);
        Task<Movie> PostMovieAsync(Movie movie);
        Task DeleteMovieAsync(int id);
        Task UpdateMovieCharactersAsync(int movieId, List<int> characters);
        Task<IEnumerable<Character>> GetMovieCharactersAsync(int id);
    }
}
