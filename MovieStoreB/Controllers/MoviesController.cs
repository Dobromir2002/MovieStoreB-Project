using Microsoft.AspNetCore.Mvc;
using MovieStoreB.BL;
using MovieStoreB.Models.DTO;
using MovieStoreB.Models.Requests;

namespace MovieStoreB.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MovieController : ControllerBase
    {
        private readonly MovieService _service;

        public MovieController(MovieService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var movies = await _service.GetAllAsync();
            return Ok(movies);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var movie = await _service.GetByIdAsync(id);
            return movie == null ? NotFound() : Ok(movie);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] AddMovieRequest request)
        {
            var movie = new Movie
            {
                Id = Guid.NewGuid().ToString(), // ?? генерираме ID
                Title = request.Title,
                Genre = request.Genre,
                Description = request.Description,
                Rating = request.Rating,
                Year = request.Year, // ?? взимаме годината
                ActorIds = request.ActorIds // ?? свързваме актьори
            };

            await _service.AddAsync(movie);
            return CreatedAtAction(nameof(GetById), new { id = movie.Id }, movie);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}
