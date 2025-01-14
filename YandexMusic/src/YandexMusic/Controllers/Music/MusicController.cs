using Microsoft.AspNetCore.Mvc;
using YandexMusic.Application.Services;
using YandexMusic.Application.Services.Impl;
using YandexMusic.DataAccess.DTOs;

namespace YandexMusic.Controllers.Music
{
    public class MusicController : Controller
    {
        public readonly IMusicService _musicService;
        public readonly IMinoService _minoService;
        public MusicController(IMusicService musicService, IMinoService minoService)
        {
            _musicService = musicService;
            _minoService = minoService;
        }
        public IActionResult Index()
        {
            return View();
        }
      
        [HttpPost("upload")]
        public async Task<IActionResult> UploadMusic(IFormFile file, MusicDTO musicDTO)
        {
            if (file == null || file.Length == 0)
                return BadRequest("Invalid file.");

            using var stream = file.OpenReadStream();
            await _minoService.UploadFileAsync(file.FileName, musicDTO, stream);

            return Ok("File uploaded successfully.");
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
