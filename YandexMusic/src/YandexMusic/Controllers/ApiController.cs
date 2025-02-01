using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace YandexMusic.Controllers
{
    [ApiController]
    [Route("api/[controlller]")]
    public class ApiController : ControllerBase
    {
        protected Guid GetUserIdFromToken()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out Guid userId))
            {
                throw new UnauthorizedAccessException("Invalid or missing user ID in token");
            }
            return userId;
        }
    }
}
