using MovieStoreB.BL.Interfaces;
using MovieStoreB.DL.Interfaces;
using MovieStoreB.Models.DTO;
using MovieStoreB.Models.Responses;

namespace MovieStoreB.BL.Services
{
    public class BlMovieService : IBlMovieService
    {
        private readonly IMovieService _movieService;
        private readonly IActorRepository _actorRepository;

        public BlMovieService(IMovieService movieService, IActorRepository actorRepository)
        {
            _movieService = movieService;
            _actorRepository = actorRepository;
        }

        public async Task<List<FullMovieDetails>> GetAllMovieDetails()
        {
            var result = new List<FullMovieDetails>();
            var movies = await _movieService.GetMovies();

            foreach (var movie in movies)
            {
                var actorList = await _actorRepository.GetActors(movie.ActorIds);

                var movieDetails = new FullMovieDetails
                {
                    Id = movie.Id,
                    Title = movie.Title,
                    Year = movie.Year,
                    Genre = movie.Genre,
                    Description = movie.Description,
                    Rating = movie.Rating,
                    Actors = actorList.ToList()
                };

                result.Add(movieDetails);
            }

            return result;
        }
    }
}
