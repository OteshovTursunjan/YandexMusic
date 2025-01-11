using Microsoft.AspNetCore.Mvc;
using YandexMusic.Application.Services;
using YandexMusic.DataAccess.DTOs;

namespace YandexMusic.Controllers.Music
{
    public class FavouriteController : Controller
    {
        public readonly IFavouriteService _favouriteService;
        public FavouriteController(IFavouriteService favouriteService)
        {
            _favouriteService = favouriteService;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPut("AddFavourite")]
        public async Task<IActionResult> AddFavourite(FavouriteDTO favouriteDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var res = await _favouriteService.AddFavourite(favouriteDTO);
            return Ok(res);
        }
    }
}
