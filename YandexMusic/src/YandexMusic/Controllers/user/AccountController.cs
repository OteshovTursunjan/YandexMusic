using Microsoft.AspNetCore.Mvc;
using YandexMusic.DataAccess.DTOs;
using YandexMusic.Application.Services;
using YandexMusic.Application.Services.lmpl;
using YandexMusic.Migrations;

namespace YandexMusic.Controllers.user
{
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

            var newAccount = accountService.AddUserAsync(accountDTO);
            return newAccount == null ? NotFound() : Ok(newAccount);
        }

        [HttpGet("GetAccount/{id}")]
        public async Task<IActionResult> GetIdAccount([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var account = await accountService.GetByIdAsync(id); // Дождитесь завершения задачи
            return account == null ? NotFound() : Ok(account);
        }

        [HttpPost("UpdateAccounts")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] AccountDTO accountDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var account = await accountService.UpdateUserAsync(id, accountDTO);
            return account != null ? Ok(account) : NotFound();  
        }

        [HttpPut("DeleteAccount/{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            if (ModelState.IsValid)
                return BadRequest(ModelState);
            var account = await accountService.DeleteUserAsync(id);
            if (account)
                return Ok(new { message = "User deleted successfully." });
            else
                return NotFound(new { message = "User not found." });

        }

    }
}
