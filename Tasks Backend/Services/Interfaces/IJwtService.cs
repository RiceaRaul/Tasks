using Domain.Models;

namespace Tasks_Backend.Services.Interfaces;

public interface IJwtService
{
    string CreateJwt(User user);
}