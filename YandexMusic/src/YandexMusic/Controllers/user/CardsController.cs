using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using YandexMusic.Application.Services;
using YandexMusic.DataAccess.DTOs;

namespace YandexMusic.Controllers.user
{
 // [Authorize(Roles = "User")]
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
        [HttpGet("GetCards")]
        public async Task<IActionResult> GetCards(Guid id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var res = await _cardService.GetCards(id);
            return Ok(res);
        }
        [HttpPut("CreatCards")]
        public async Task<IActionResult> CreateCards(CardDTO cardDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var res = await _cardService.CreateCard(cardDTO);
            return Ok(res);
        }
        [HttpDelete("DeleteCards{id}")]
        public async Task<IActionResult> DeleteCards(Guid id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var res = await _cardService.Delete(id);
            return Ok(res);

        }

    }
}
