using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using RatioMusic.Application.Services;

namespace RatioMusic.Api.Controllers.V2
{
    [ApiController]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class SongController : ControllerBase
    {
        private readonly ISongService _songService;

        public SongController(ISongService songService)
        {
            _songService = songService;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var songs = await _songService.GetAllSongsAsync();
            
            if(!songs.Any() || songs == null) return NotFound();

            return Ok(songs);
        }
    }
}
