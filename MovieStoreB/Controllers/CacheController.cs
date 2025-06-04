
using Microsoft.AspNetCore.Mvc;
using MovieStoreB.Infrastructure.Interfaces;

namespace MovieStoreB.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CacheController : ControllerBase
    {
        private readonly ICacheConsumerService _cacheConsumer;

        public CacheController(ICacheConsumerService cacheConsumer)
        {
            _cacheConsumer = cacheConsumer;
        }

        [HttpGet("all")]
        public IActionResult GetAllCachedMovies()
        {
            var movies = _cacheConsumer.GetAllCachedMovies();
            return Ok(movies);
        }
    }
}
