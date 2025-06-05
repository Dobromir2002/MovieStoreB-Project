using Microsoft.AspNetCore.Mvc;
using MovieStoreB.BL;
using MovieStoreB.Models.DTO;

namespace MovieStoreB.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ActorController : ControllerBase
    {
        private readonly ActorService _actorService;

        public ActorController(ActorService actorService)
        {
            _actorService = actorService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() =>
            Ok(await _actorService.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var actor = await _actorService.GetByIdAsync(id);
            return actor == null ? NotFound() : Ok(actor);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] Actor actor)
        {
            await _actorService.AddAsync(actor);
            return Ok(actor);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _actorService.DeleteAsync(id);
            return NoContent();
        }
    }
}
