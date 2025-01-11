using Microsoft.AspNetCore.Mvc;
using YandexMusic.DataAccess.DTOs;
using YandexMusic.Application.Services;
using Microsoft.AspNetCore.Authorization;

namespace YandexMusic.Controllers.user
{
    [Authorize(Roles = "Admin")]
    public class CardTypeController : Controller
    {
        private readonly ICard_TypeService _card_TypeService;

        public CardTypeController(ICard_TypeService card_TypeService)
        {
            _card_TypeService = card_TypeService;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost("create-CardType")]
        public async Task<IActionResult> AddCards([FromBody] CardTypeDTO cardTypeDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var newUser = await _card_TypeService.AddCardrAsync(cardTypeDTO);
            return newUser == null ? NotFound() : Ok(newUser);
        }
        [HttpPut("update-CardType/{Id}")]
        public async Task<IActionResult> UpdateCards([FromRoute] Guid Id, [FromBody] CardTypeDTO cardTypeDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var res = await _card_TypeService.UpdateCardAsync(Id, cardTypeDTO);
            return res == null ? NotFound() : Ok(res);
        }
        [HttpPost("GetCardType{id}")]
        public async Task<IActionResult> GetCards([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var res = await _card_TypeService.GetByIdAsync(id);
            return res == null ? NotFound() : Ok(res);
        }
    }
}
