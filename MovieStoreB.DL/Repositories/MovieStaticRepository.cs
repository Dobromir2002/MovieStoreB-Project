
using Microsoft.Extensions.Options;
using MovieStoreB.DL.DB;
using MovieStoreB.DL.Interfaces;
using MovieStoreB.DL.Repositories.MongoRepositories;
using MovieStoreB.Models.Configurations;
using MovieStoreB.Models.DTO;

namespace MovieStoreB.DL.Repositories
{
    public class MovieStaticRepository : MoviesRepository
    {
        public MovieStaticRepository(IOptionsMonitor<MongoDbConfiguration> mongoConfig) : base(mongoConfig)
        {
        }

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
