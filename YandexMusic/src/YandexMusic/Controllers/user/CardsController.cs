using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;
using YandexMusic.Application.Features.Cards.Commands;
using YandexMusic.Application.Features.Cards.Queries;
using YandexMusic.Application.Services;
using YandexMusic.DataAccess.DTOs;

namespace YandexMusic.Controllers.user
{
   // [Authorize]
    public class CardsController : Controller
    {
        public readonly IMediator mediator;
        public CardsController(IMediator mediator)
        {
            this.mediator = mediator;
        }
        protected Guid GetUserIdFromToken()
        {
            foreach (var claim in User.Claims)
            {
                Debug.WriteLine($"Claim Type: {claim.Type}, Value: {claim.Value}");
            }

            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim))
            {
                userIdClaim = User.FindFirst("sub")?.Value;
            }

            if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out Guid userId))
            {
                throw new UnauthorizedAccessException("Invalid or missing user ID in token");
            }
            return userId;
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
            var res = await mediator.Send(new CreateCardsCommand(cardDTO));
            return Ok(res);
        }
        [HttpGet("GetCards")]
        public async Task<IActionResult> GetCards()
        {
            var UserId = GetUserIdFromToken();
            var res = await mediator.Send(new GetCardsByIdQueries(UserId));
            return Ok(res);
        }
    }
}
