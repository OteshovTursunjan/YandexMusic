using Microsoft.AspNetCore.Mvc;
using YandexMusic.DataAccess.DTOs;
using YandexMusic.Application.Services;
using YandexMusic.Application.Services.lmpl;
using YandexMusic.Migrations;
using Microsoft.AspNetCore.Authorization;
using System.Diagnostics;
using System.Security.Claims;

namespace YandexMusic.Controllers.user
{
    [Authorize(Roles = "User")]
    public class AccountController : Controller
    {
        public readonly IAccountService accountService;
       public AccountController(IAccountService accountService)
        {
            this.accountService = accountService;
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

            var newAccount = accountService.AddAccountAsync(accountDTO);
            return newAccount == null ? NotFound() : Ok(newAccount);
        }

        [HttpGet("GetAccount")]
        public async Task<IActionResult> GetIdAccount()
        {
            var userId = GetUserIdFromToken();

            var account = await accountService.GetByIdAsync(userId); // Дождитесь завершения задачи
            return account == null ? NotFound() : Ok(account);
        }

        [HttpPut("UpdateAccounts/{id}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] AccountDTO accountDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var account = await accountService.UpdateAccountAsync(id, accountDTO);
            return account != null ? Ok(account) : NotFound();  
        }

        [HttpDelete("DeleteAccount/{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var account = await accountService.DeleteAccountAsync(id);
            if (account)
                return Ok(new { message = "User deleted successfully." });
            else
                return NotFound(new { message = "User not found." });

        }

    }
}
