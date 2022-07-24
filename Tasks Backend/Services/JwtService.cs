using Repositories.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Domain.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Tasks_Backend.Models;
using Tasks_Backend.Services.Interfaces;

namespace Tasks_Backend.Services;

public class JwtService : IJwtService
{
    private readonly IRepositoryWrapper _repository;
    
    private readonly AppSettings _appSettings;
    
    private const double ExpireHours = 24.0;
    
    public JwtService(IOptions<AppSettings> appsettings,IRepositoryWrapper repository)
    {
        _appSettings = appsettings.Value;
        _repository = repository;
    }
    public string CreateJwt(User user)
    {
        var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
        var jwtHandler = new JwtSecurityTokenHandler();
        var payload = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new("Id", user.Id.ToString()),
            }),
            Expires = DateTime.UtcNow.AddHours(ExpireHours),
            Issuer = "http://localhost:5000",
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = jwtHandler.CreateToken(payload);
        return jwtHandler.WriteToken(token);
    }
}