
using MovieStoreB.DL.DB;
using MovieStoreB.DL.Interfaces;
using MovieStoreB.Models.DTO;

namespace MovieStoreB.DL.Repositories
{
    public class MovieStaticRepository : IMovieRepository
    {
        public Task<List<Movie>> GetMovies()
        {
            return Task.FromResult(StaticData.Movies.ToList());
        }

        public Task AddMovie(Movie movie)
        {
            StaticData.Movies.Add(movie);
            return Task.CompletedTask;
        }

        public Task DeleteMovie(string id)
        {
            var movie = StaticData.Movies.FirstOrDefault(x => x.Id == id);
            if (movie != null)
            {
                StaticData.Movies.Remove(movie);
            }
            return Task.CompletedTask;
        }

        public Task<Movie?> GetMoviesById(string id)
        {
            var movie = StaticData.Movies.FirstOrDefault(x => x.Id == id);
            return Task.FromResult(movie);
        }
    }
}
