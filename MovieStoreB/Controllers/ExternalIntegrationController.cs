
using Microsoft.AspNetCore.Mvc;
using MovieStoreB.BL.Interfaces;

namespace MovieStoreB.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ExternalIntegrationController : ControllerBase
    {
        private readonly IExternalMovieApiService _externalService;

        public ExternalIntegrationController(IExternalMovieApiService externalService)
        {
            _externalService = externalService;
        }

        [HttpPost("fetch-and-save")]
        public async Task<IActionResult> FetchAndSave()
        {
            await _externalService.FetchAndSaveExternalMoviesAsync();
            return Ok("Fetched and saved external movies.");
        }
    }
}
