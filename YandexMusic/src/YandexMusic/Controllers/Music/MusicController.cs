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
        [HttpGet("Get")]
        public async Task<IActionResult> GetMusic(Guid id)
        {
            try
            {
                var (stream, contentType) = await _minoService.GetMusicAsync(id);
                return File(stream, contentType, enableRangeProcessing: true);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpGet("GetMusics")]
        public async Task<IActionResult> GetMusics()
        {
            var (stream, contentType) = await _minoService.GetMusic();
            return File(stream, contentType);
        }
        [HttpPost("upload")]
        public async Task<IActionResult> UploadMusic(IFormFile file,  MusicDTO musicDTO)
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
             await _minoService.DeleteFileAsync(id);
           return Ok("File is deleted");
        }
    }
}
