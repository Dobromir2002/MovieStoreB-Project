using MovieStoreB.Models.DTO;

namespace MovieStoreB.DL.Interfaces
{
    public interface IMovieRepository
    {
        Task<IEnumerable<Movie>> GetMovies();
        Task<Movie?> GetMovieById(string id);
        Task AddMovie(Movie movie);
        Task DeleteMovie(string id);
    }
}
