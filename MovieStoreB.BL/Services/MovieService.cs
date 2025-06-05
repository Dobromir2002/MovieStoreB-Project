using MovieStoreB.DL.Interfaces;
using MovieStoreB.Models.DTO;

namespace MovieStoreB.BL
{
    public class MovieService
    {
        private readonly IMovieRepository _repository;

        public MovieService(IMovieRepository repository)
        {
            _repository = repository;
        }

        public Task<IEnumerable<Movie>> GetAllAsync() => _repository.GetMovies();

        public Task<Movie?> GetByIdAsync(string id) => _repository.GetMovieById(id);

        public Task AddAsync(Movie movie) => _repository.AddMovie(movie);

        public Task DeleteAsync(string id) => _repository.DeleteMovie(id);
    }
}
