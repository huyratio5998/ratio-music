using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using RatioMusic.Application.Services;
using RatioMusic.Application.ViewModels.Validations;
using RatioMusic.Application.ViewModels;

namespace RatioMusic.Api.Controllers.V1
{
    [ApiController]
    [ApiVersion("1.0")]    
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ArtistController : ControllerBase
    {
        private IArtistService _artistService;

        public ArtistController(IArtistService artistService)
        {
            _artistService = artistService;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var Artists = await _artistService.GetArtistsAsync();
            if (!Artists.Any() || Artists == null) return NotFound();

            return Ok(Artists);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            if (id <= 0) return BadRequest();
            var Artist = await _artistService.GetArtistAsync(id, false);

            if (Artist == null) return NotFound();

            return Ok(Artist);
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPost]
        public async Task<IActionResult> CreateArtistAsync([FromBody] ArtistApiRequest newArtist)
        {
            var ArtistValidator = new ArtistApiRequestValidation().Validate(newArtist);
            if (!ArtistValidator.IsValid) return BadRequest(newArtist);

            // validation
            var res = await _artistService.CreateArtistAsync(newArtist);
            if (res.Artist == null || res.Artist.Id == 0) return Ok(null);

            return CreatedAtAction(nameof(GetById), new { id = res.Artist.Id }, res);

        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPut]
        public async Task<IActionResult> UpdateArtist([FromBody] ArtistApiRequest Artist)
        {
            if (Artist == null || Artist.Id == 0) return BadRequest();

            var res = await _artistService.UpdateArtistAsync(Artist);

            return Ok(res);
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteArtist(int id)
        {
            if (id == 0) return BadRequest();

            var Artist = await _artistService.GetArtistAsync(id, false);
            if (Artist == null) return NotFound();

            var result = await _artistService.DeleteArtistAsync(id);
            return Ok(result);
        }
    }
}
