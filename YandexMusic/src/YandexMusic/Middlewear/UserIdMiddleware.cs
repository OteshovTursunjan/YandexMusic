using System.Security.Claims;

namespace YandexMusic.Middlewear
{
    public class UserIdMiddleware
    {
        private readonly RequestDelegate _next;

        public UserIdMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var userIdClaim = context.User?.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim != null)
            {
                context.Items["UserId"] = userIdClaim.Value;
            }

            await _next(context);
        }
    }


}

