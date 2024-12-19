using Microsoft.Extensions.Options;
using WebApplication1.Helpers;
using WebApplication1.Models;
using WebApplication1.Repositories;
namespace WebApplication1.Authorization
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly AppSettings _appSettings;



      public JwtMiddleware(RequestDelegate next, IOptions<AppSettings> appSettings)
        {
            _next = next;
            _appSettings = appSettings.Value;
        }

        public async Task Invoke(HttpContext context, modelsContext wrapper, IJwtUtils jwtUtils)
        {
              var token = context.Request.Headers["Autorization"].FirstOrDefault()?.Split(" ").Last();
              var accountId = jwtUtils.ValidateJwtToken(token);
            if (accountId != null)
            {
                // Directly retrieve the user by ID without external method call
                var user = await wrapper.Customers.FindAsync(accountId.Value);

                if (user != null)
                {
                    context.Items["User"] = user;
                }
            }

            await _next(context);

            await _next(context);
        }
    }
}
