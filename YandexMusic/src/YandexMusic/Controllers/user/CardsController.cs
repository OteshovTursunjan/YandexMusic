using Microsoft.AspNetCore.Mvc;
using YandexMusic.Application.Services;
using YandexMusic.DataAccess.DTOs;

namespace YandexMusic.Controllers.user
{
    public class CardsController : Controller
    {
        public readonly ICardService _cardService;
        public CardsController(ICardService cardService)
        {
            _cardService = cardService;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPut("CreatCards")]
        public async Task<IActionResult> CreateCards(CardDTO cardDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var res = await _cardService.CreateCard(cardDTO);
            return Ok(res);
        }
    }
}
