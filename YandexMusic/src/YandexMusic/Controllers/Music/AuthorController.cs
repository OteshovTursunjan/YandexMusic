using Microsoft.AspNetCore.Mvc;
using YandexMusic.Application.DTOs;
using YandexMusic.Application.DTOs.User;
using YandexMusic.Application.Services;
using YandexMusic.Application.Services.lmpl;

namespace YandexMusic.Controllers.Music
{
    public class AuthorController : Controller
    {
        public readonly IAuthorService _authorService;
        public AuthorController(IAuthorService authorService)
        {
            _authorService = authorService;
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

            var newAuthor = await _authorService.AddAuthorAsync(authorDTO);

            return newAuthor == null ? NotFound() : Ok(newAuthor);
        }

        [HttpPut("update-Author{id}")]
        public async Task<IActionResult> UpdateAuthor([FromRoute] Guid id, [FromBody] AuthorDTO authorDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var res = await _authorService.UpdateAuthorAsync(id, authorDTO);
            return res == null ? NotFound() : Ok(res);
        }
        [HttpGet("GetAuthor{id}")]
        public async Task<IActionResult> GetAuthor([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var res = await _authorService.GetByIdAsync(id);
            return res == null ? NotFound() : Ok(res);

        }
        [HttpPost("DeletAuthor{id}")]
        public async Task<IActionResult> DeleteAuthor([FromRoute,] Guid id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var res = await _authorService.DeleteAuthorAsync(id);
            return res == null ? NotFound() : Ok(res);

        }
    }
}
