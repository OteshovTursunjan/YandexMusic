using Microsoft.AspNetCore.Mvc;
using YandexMusic.Application.DTOs.User;
using YandexMusic.Application.Services;

namespace YandexMusic.Controllers.user
{
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
        [HttpPost("create-cardtype")]
        public async Task<IActionResult> AddCards([FromBody] CardTypeDTO cardTypeDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var newUser = await _card_TypeService.AddCardrAsync(cardTypeDTO);
            return newUser == null ? NotFound() : Ok(newUser);
        }
        [HttpPut("update-cards/{Id}")]
        public async Task<IActionResult> UpdateCards([FromRoute] Guid Id, [FromBody] CardTypeDTO cardTypeDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var res = await _card_TypeService.UpdateCardAsync(Id, cardTypeDTO);
            return res == null ? NotFound() : Ok(res);
        }
        [HttpPost("GetCards{id}")]
        public async Task<IActionResult> GetCards([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var res = await _card_TypeService.GetByIdAsync(id);
            return res == null ? NotFound() : Ok(res);
        }
    }
}
