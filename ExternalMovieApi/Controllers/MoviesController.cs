
using Microsoft.AspNetCore.Mvc;
using MovieStoreB.Models.DTO;

namespace ExternalMovieApi.Controllers
{
    [ApiController]
    [Route("external/[controller]")]
    public class MoviesController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetMockMovies()
        {
            var movies = new List<Movie>
            {
                new Movie { Id = "100", Title = "Mock Movie 1", Genre = "Drama" },
                new Movie { Id = "101", Title = "Mock Movie 2", Genre = "Comedy" }
            };

            return Ok(movies);
        }
    }
}
