﻿using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using YandexMusic.DataAccess.DTOs;
using YandexMusic.Application.Services;
using YandexMusic.DataAccess.Authentication;
using YandexMusics.Core.Entities.Music;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using YandexMusic.Application.Services.lmpl;
using System.Diagnostics;

namespace YandexMusic.Controllers.user
{
  
   // [Authorize]
    public class UserContorller : Controller
    {
        private readonly IUserService _userService;
        private readonly IJwtTokenHandler _jwtTokenHandler;
        public readonly TokenService _tokenService;

        public UserContorller(IUserService userService, IJwtTokenHandler jwtTokenHandler, TokenService tokenService)
        {
            _userService = userService;
            _jwtTokenHandler = jwtTokenHandler;
            _tokenService = tokenService;
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
        [HttpGet("GetID")]
        public async Task<IActionResult> GetUser()
        {

            var userId = GetUserIdFromToken();
            var res = await _userService.GetByIdAsync(userId);
            return res == null ? NotFound() : Ok(res);
        }

        [HttpPost("create-user")]
        public async Task<IActionResult> AddUser(UserForCreationDTO userForCreationDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                var CreateUser = await _userService.AddUserAsync(userForCreationDTO);
                var accesTokent = _jwtTokenHandler.GenerateAccesToken(CreateUser);
                var refreshToken = _jwtTokenHandler.GenerateRefreshToken();

                return Ok(new
                {
                    AccessToken = new JwtSecurityTokenHandler().WriteToken(accesTokent),
                    RefreshToken = refreshToken,
                    User = CreateUser
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new {Message = ex.Message});
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
