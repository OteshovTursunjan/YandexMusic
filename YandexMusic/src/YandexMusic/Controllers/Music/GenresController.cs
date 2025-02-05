using Microsoft.AspNetCore.Mvc;
using YandexMusic.DataAccess.DTOs;
using YandexMusic.Application.Services;
using MediatR;
using YandexMusic.Application.Features.Genre.Commands;
using YandexMusic.Application.Features.Genre.Queries;

namespace YandexMusic.Controllers.Music
{
    public class GenresController : Controller
    {
        public readonly IMediator mediator;
        public GenresController(IMediator mediator)
        {
            this.mediator = mediator;
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
          
            var res = await mediator.Send(new CreateGenreCommand(genreDTO));
            return res == null ? NotFound() : Ok(res);

        }
        [HttpGet("GetGenres{id}")]
        public async Task<IActionResult> GetGenres([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var res = await mediator.Send(new GetGenreByIdQueries(id));
            return Ok(res);

        }
        [HttpPut("UpdateGenres{id}")]
        public async Task<IActionResult> UpdateGenres([FromRoute] Guid id, GenreDTO genreDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var res = await mediator.Send(new UpdateGenreCommand(id,genreDTO));
            return Ok(res);
        }
        [HttpDelete("DeleteGenres{id}")]
        public async Task<IActionResult> DeleteGenres([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var res = await mediator.Send(new DeleteGenreCommand(id));
            return Ok(res);
        }

    }
}
