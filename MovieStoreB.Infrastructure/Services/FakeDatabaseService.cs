
using MovieStoreB.Models.DTO;

namespace MovieStoreB.Infrastructure.Services
{
    public class FakeDatabaseService
    {
        public Task<List<Movie>> GetAllMoviesAsync()
        {
            var mockMovies = new List<Movie>
            {
                new Movie { Id = "1", Title = "Inception", Genre = "Sci-Fi" },
                new Movie { Id = "2", Title = "The Matrix", Genre = "Action" }
            };

            return Task.FromResult(mockMovies);
        }
    }
}
