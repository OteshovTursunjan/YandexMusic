using Microsoft.AspNetCore.Mvc;
using YandexMusic.DataAccess.DTOs;
using YandexMusic.Application.Services;

namespace YandexMusic.Controllers.Music
{
    public class GenresController : Controller
    {
        public readonly IGenresService _genresService;
        public GenresController(IGenresService genresService)
        {
            _genresService = genresService;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPut("AddGenres")]
        public async Task<IActionResult> AddGenres(GenreDTO genreDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var res = await _genresService.AddGenresAsync(genreDTO);
            return res == null ? NotFound() : Ok(res);

        }
        [HttpGet("GetGenres{id}")]
        public async Task<IActionResult> GetGenres([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var res = await _genresService.GetByIdAsync(id);
            return Ok(res);

        }
        [HttpPut("UpdateGenres{id}")]
        public async Task<IActionResult> UpdateGenres([FromRoute] Guid id, GenreDTO genreDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var res = await _genresService.UpdateGenresAsync(id, genreDTO);
            return Ok(res);
        }
        [HttpDelete("DeleteGenres{id}")]
        public async Task<IActionResult> DeleteGenres([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var res = await  _genresService.DeleteGenresAsync(id);
            return Ok(res);
        }

    }
}
