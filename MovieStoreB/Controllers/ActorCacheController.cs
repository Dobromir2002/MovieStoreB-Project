using Microsoft.AspNetCore.Mvc;
using MovieStoreB.Services.Memory;

namespace MovieStoreB.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ActorCacheController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAll()
        {
            var cached = MemoryActorStorage.GetAll();
            return Ok(cached);
        }
    }
}
