using System.Collections.Concurrent;
using MovieStoreB.Models.DTO;

namespace MovieStoreB.Services.Memory
{
    public static class MemoryMovieStorage
    {
        private static readonly ConcurrentDictionary<string, Movie> _movies = new();

        public static void AddOrUpdate(Movie movie)
        {
            if (movie != null && !string.IsNullOrEmpty(movie.Id))
            {
                _movies[movie.Id] = movie;
            }
        }

        public static IEnumerable<Movie> GetAll() => _movies.Values;
    }
}
