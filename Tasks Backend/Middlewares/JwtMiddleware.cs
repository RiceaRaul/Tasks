using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Tasks_Backend.Models;
using Repositories.Interfaces;

namespace Tasks_Backend.Middlewares
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
        public async Task Invoke(HttpContext context, IRepositoryWrapper repository)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token != null)
                AttachUserToContext(context, repository, token);

            await _next(context);
        }

        private void AttachUserToContext(HttpContext context, IRepositoryWrapper repository, string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var userId = int.Parse(jwtToken.Claims.First(x => x.Type == "Id").Value);

                context.Items["User"] = repository.User.FindById(userId);
            }
            catch
            {
               
            }
        }
    }
}
