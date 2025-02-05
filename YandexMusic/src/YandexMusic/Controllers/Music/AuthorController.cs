using Microsoft.AspNetCore.Mvc;
using YandexMusic.DataAccess.DTOs;
//using YandexMusic.Application.DTOs.User;
using YandexMusic.Application.Services;
using YandexMusic.Application.Services.lmpl;
using MediatR;
using YandexMusic.Application.Features.Author.Commands;
using YandexMusic.Application.Features.Author.Queries;

namespace YandexMusic.Controllers.Music
{
    public class AuthorController : Controller
    {
        public readonly IMediator mediator;
        public AuthorController(IMediator mediator)
        {
           this.mediator = mediator;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("create_Author")]
        public async Task<IActionResult> AddAuthor([FromBody] AuthorDTO authorDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await mediator.Send(new CreateAuthorCommand(authorDTO.Authorname)) ;
            

            return result == null ? NotFound() : Ok(result);
        }

        [HttpPut("update-Author{id}")]
        public async Task<IActionResult> UpdateAuthor([FromRoute] Guid id, [FromBody] AuthorDTO authorDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var res = await mediator.Send(new UpdateAuthorCommand(id, authorDTO.Authorname)) ;
            return res == null ? NotFound() : Ok(res);
        }
        [HttpGet("GetAuthor{id}")]
        public async Task<IActionResult> GetAuthor([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var res = await mediator.Send(new GetAuthorByIdQueris(id)) ;

            return res == null ? NotFound() : Ok(res);

        }
        [HttpDelete("DeletAuthor{id}")]
        public async Task<IActionResult> DeleteAuthor([FromRoute,] Guid id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var res = await mediator.Send(new DeleteAuthorCommand(id));
            return res == null ? NotFound() : Ok(res);

        }
    }
}
