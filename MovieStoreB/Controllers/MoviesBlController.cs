using Microsoft.AspNetCore.Mvc;
using MovieStoreB.BL.Interfaces;
using Microsoft.Extensions.Logging;

namespace MovieStoreB.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MoviesBlController : ControllerBase
    {
        private readonly IBlMovieService _movieService;
        private readonly ILogger<MoviesBlController> _logger;

        public MoviesBlController(
            IBlMovieService movieService,
            ILogger<MoviesBlController> logger)
        {
            _movieService = movieService;
            _logger = logger;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _movieService.GetAllMovieDetails();

            if (result == null || !result.Any())
            {
                return NotFound();
            }

            return Ok(result);
        }
    }

    public class TestRequest
    {
        public int Id { get; set; }
        public string Title { get; set; }
    }
}
