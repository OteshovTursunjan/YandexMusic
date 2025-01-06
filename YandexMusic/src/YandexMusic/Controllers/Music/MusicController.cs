using Microsoft.AspNetCore.Mvc;
using YandexMusic.Application.Services;
using YandexMusic.DataAccess.DTOs;

namespace YandexMusic.Controllers.Music
{
    public class MusicController : Controller
    {
        public readonly IMusicService _musicService;
        public MusicController(IMusicService musicService)
        {
            _musicService = musicService;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost("UploadMusic")]
        public async Task<IActionResult> UploadMusic(IFormFile formFile, MusicDTO musicDTO)
        {
            if (!ModelState.IsValid)
                      return BadRequest(ModelState);
            var res = await _musicService.CreateMusic(formFile, musicDTO);
            return res == null ? NotFound() : Ok(res);
        }
        [HttpDelete("DeleteMusic{id}")]
        public async Task<IActionResult> DeleteMusic([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var res = await _musicService.DeleteMusic(id);
            return res == null ? NotFound() : Ok(res);
        }
    }
}
