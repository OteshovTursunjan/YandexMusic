using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using YandexMusic.DataAccess.DTOs;
using YandexMusic.Application.Services;
using YandexMusic.DataAccess.Authentication;
using YandexMusics.Core.Entities.Music;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace YandexMusic.Controllers.user
{
 //   [Authorize(Roles = "User")]

    public class UserContorller : Controller
    {
        private readonly IUserService _userService;
        private readonly IJwtTokenHandler _jwtTokenHandler;

        public UserContorller(IUserService userService, IJwtTokenHandler jwtTokenHandler)
        {
            _userService = userService;
            _jwtTokenHandler = jwtTokenHandler;
        }


        [HttpGet("GetCards")]
        public async Task<IActionResult> GetUser(Guid id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var res = await _userService.GetByIdAsync(id);
            return res == null ? NotFound() : Ok(res);
        }

        [HttpPost("create-user")]
        public async Task<IActionResult> AddUser(UserForCreationDTO userForCreationDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var createdUser = await _userService.AddUserAsync(userForCreationDTO);
                var accessToken = _jwtTokenHandler.GenerateAccesToken(createdUser);
                var refreshToken = _jwtTokenHandler.GenerateRefreshToken();

                return Ok(new
                {
                    AccessToken = new JwtSecurityTokenHandler().WriteToken(accessToken),
                    RefreshToken = refreshToken,
                    User = createdUser
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }


        [HttpPut("update-user/{id}")]
        public async Task<IActionResult> UpdateUser([FromRoute] Guid id,  UserDTO userDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var res = await _userService.UpdateUserAsync(id, userDTO);
            return res == null ? NotFound() : Ok(res);
        }
        [HttpDelete("Delete/{ID}")]
        public async Task<IActionResult> DeleteUser([FromRoute] Guid ID)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var res = await _userService.DeleteUserAsync(ID);
            return res == null ? NotFound() : Ok(res);
        }
       
    }
}
