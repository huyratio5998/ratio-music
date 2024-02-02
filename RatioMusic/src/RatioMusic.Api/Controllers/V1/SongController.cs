using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using RatioMusic.Application.Services;
using RatioMusic.Application.ViewModels;
using RatioMusic.Application.ViewModels.Validations;

namespace RatioMusic.Api.Controllers.V1
{
    [ApiController]
    [ApiVersion("1.0")]
    [ApiVersion("3.0")]
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
        public async Task<IActionResult> Get([FromQuery] SongQueryParams queryParams)
        {
            var songs = await _songService.GetAllSongsAsync(queryParams);
            if (songs == null || songs.Items == null || !songs.Items.Any()) return NotFound();

            return Ok(songs);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]        
        public async Task<IActionResult> GetById(int id)
        {
            if (id <= 0) return BadRequest();
            var song = await _songService.GetSongById(id,false);

            if (song == null) return NotFound();

            return Ok(song);
        }        

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPost]        
        public async Task<IActionResult> CreateSongAsync([FromBody] SongApiRequest newSong)
        {
            var songValidator = new SongApiRequestValidation().Validate(newSong);
            if (!songValidator.IsValid) return BadRequest(newSong);

            // validation
            var res = await _songService.CreateSongAsync(newSong);
            if (res.Song == null || res.Song.Id == 0) return Ok(null);

            return CreatedAtAction(nameof(GetById), new { id = res.Song.Id }, res);

        }        

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPut]
        public async Task<IActionResult> UpdateSong([FromBody] SongApiRequest song)
        {
            if (song == null || song.Id == 0) return BadRequest();

            var res = await _songService.UpdateSongAsync(song);

            return Ok(res);
        }

        [HttpDelete("{songId:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteSong(int songId)
        {
            if (songId == 0) return BadRequest();

            var song = await _songService.GetSongById(songId, false);
            if(song == null) return NotFound();

            var result = await _songService.DeleteSong(songId);
            return Ok(result);
        }

        // Example for another version the same controller.
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [MapToApiVersion("3.0")]
        [HttpGet]
        public async Task<IActionResult> Get(int id)
        {
            if (id == 0) return NotFound();
            var song = await _songService.GetSongById(id, false);

            if (song == null) return NotFound();

            return Ok(song);
        }
    }
}
