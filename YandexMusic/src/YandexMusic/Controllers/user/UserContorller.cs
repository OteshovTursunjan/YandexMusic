using Microsoft.AspNetCore.Mvc;
using YandexMusic.Application.DTOs;
using YandexMusic.Application.Services;
using YandexMusics.Core.Entities.Musics;

namespace YandexMusic.Controllers.user
{

    public class UserContorller : Controller
    {
        private readonly IUserService _userService;

        public UserContorller(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet("GetID/{id}")]
        public async Task<IActionResult> GetUser([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var res = await _userService.GetByIdAsync(id);
            return res == null ? NotFound() : Ok(res);
        }

        [HttpPost("create-user")]
        public async Task<IActionResult> AddUser( UserForCreationDTO userDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var newUser = await _userService.AddUserAsync(userDto);
            return newUser == null ? NotFound() : Ok(newUser);
        }

        [HttpPut("update-user/{id}")]
        public async Task<IActionResult> UpdateUser([FromRoute] Guid id, [FromBody] UserDTO userDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var res = await _userService.UpdateUserAsync(id, userDTO);
            return res == null ? NotFound() : Ok(res);
        }
        [HttpPut("Delete/{ID}")]
        public async Task<IActionResult> DeleteUser([FromRoute] Guid ID)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var res = await _userService.DeleteUserAsync(ID);
            return res == null ? NotFound() : Ok(res);
        }

    }
}
