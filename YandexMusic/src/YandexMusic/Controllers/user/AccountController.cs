using Microsoft.AspNetCore.Mvc;
using YandexMusic.DataAccess.DTOs;
using YandexMusic.Application.Services;
using YandexMusic.Application.Services.lmpl;
using YandexMusic.Migrations;
using Microsoft.AspNetCore.Authorization;
using System.Diagnostics;
using System.Security.Claims;
using MediatR;
using YandexMusic.Application.Features.Account.Commands;
using YandexMusic.Application.Features.Account.Handler;
using YandexMusic.Application.Features.Account.Queries;

namespace YandexMusic.Controllers.user
{
    //[Authorize(Roles = "User")]
    public class AccountController : Controller
    {
        public readonly IMediator mediator;
        public AccountController(IMediator mediator)
        {
            this.mediator = mediator;
        }
        public IActionResult Index()
        {
            return View();
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
        [HttpPost("CreateAccount")]
        public async Task<IActionResult> CreateAccount(AccountDTO accountDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var newAccount = await mediator.Send(new CreateAccountCommand(accountDTO));
            return newAccount == null ? NotFound() : Ok(newAccount);
        }

        [HttpGet("GetAccount")]
        public async Task<IActionResult> GetIdAccount()
        {
            Guid  userId = GetUserIdFromToken();

            var account = await mediator.Send(new GetAccountByIdQueries(userId)); // Дождитесь завершения задачи
            return account == null ? NotFound() : Ok(account);
        }

        [HttpPut("UpdateAccounts/{id}")]
        public async Task<IActionResult> Update([FromBody] AccountDTO accountDTO)
        {
            var userId = GetUserIdFromToken();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var account = await mediator.Send(new UpdateAccountCommand(userId,accountDTO));
            return account != null ? Ok(account) : NotFound();  
        }

        [HttpDelete("DeleteAccount/{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var account = await mediator.Send(new DeletAccountCommand(id));
            if (account)
                return Ok(new { message = "User deleted successfully." });
            else
                return NotFound(new { message = "User not found." });

        }

    }
}
