using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YandexMusics.Core.Entities.Musics;

namespace YandexMusic.DataAccess.Authentication
{
    public interface IJwtTokenHandler
    {
        JwtSecurityToken GenerateAccesToken(User user);
        string GenerateRefreshToken();
    }
}
