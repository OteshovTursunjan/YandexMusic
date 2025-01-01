using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YandexMusic.DataAccess.DTOs;
using YandexMusics.Core.Entities.Musics;
namespace YandexMusic.DataAccess.Authentication
{
    public interface IJwtTokenHandler
    {
        JwtSecurityToken GenerateAccesToken(UserForCreationDTO user);
        string GenerateRefreshToken();
    }
}
