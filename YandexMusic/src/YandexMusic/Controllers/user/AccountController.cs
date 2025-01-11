using Microsoft.AspNetCore.Mvc;
using YandexMusic.DataAccess.DTOs;
using YandexMusic.Application.Services;
using YandexMusic.Application.Services.lmpl;
using YandexMusic.Migrations;
using Microsoft.AspNetCore.Authorization;

namespace YandexMusic.Controllers.user
{
    //[Authorize]
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
        
        [HttpPost("CreateAccount")]
        public async Task<IActionResult> CreateAccount(AccountDTO accountDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var newAccount = await accountService.AddAccountAsync(accountDTO);
            return Ok(newAccount);
          }

        [HttpGet("GetAccount/{id}")]
        public async Task<IActionResult> GetIdAccount([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var account = await accountService.GetByIdAsync(id); // Дождитесь завершения задачи
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
