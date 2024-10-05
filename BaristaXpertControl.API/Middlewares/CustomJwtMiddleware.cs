using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace BaristaXpertControl.API.Middlewares
{
    public class CustomJwtMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomJwtMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var authHeader = context.Request.Headers["Authorization"].FirstOrDefault();

            if (!string.IsNullOrEmpty(authHeader) && !authHeader.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
            {
                // Automatically prepend "Bearer " if it's missing
                context.Request.Headers["Authorization"] = $"Bearer {authHeader.Trim()}";
            }

            await _next(context);
        }
    }
}
