using Microsoft.AspNetCore.Mvc;
using Repositories.Interfaces;
using Tasks_Backend.Models.Requests;
using Tasks_Backend.Services.Interfaces;
using Tasks_Backend.Attributes;
using Domain.Models;

namespace Tasks_Backend.Controllers;
[ApiController]
[Route("api/[controller]")]
public class AuthentificationController : ControllerBase
{
    private readonly IRepositoryWrapper _repository;
    
    private readonly ISecurityService _security;

    private readonly IJwtService _jwtservice;
    
    public AuthentificationController( 
        IRepositoryWrapper repository,
        ISecurityService security,
        IJwtService jwtservice)
    {
        _repository = repository;
        _security = security;
        _jwtservice = jwtservice;
    }
  
    [HttpPost("login")]
    public async Task<ActionResult<AuthentificationResponse>> LoginAccount(AuthentificationRequest request)
    {
        var users = _repository.User.FindByCondition(x => x.UserName == request.Username);
        
        if (!users.Any()) return BadRequest(new {message = "Username or password is incorrect"});
        
        var currentUser = users.First();
        
        var currentPass = $"{request.Password}{currentUser.Secret}";
        
        if (!(await _security.Verify(currentPass,currentUser.Password)))
        {
            return BadRequest(new {message = "Username or password is incorrect"});
        }
  
        var response = new AuthentificationResponse
        {
            AuthToken = _jwtservice.CreateJwt(currentUser),
            UserData = new AuthentificationUserResponse
            {
                Username = currentUser.UserName
            }
        };
        return Ok(response);
    }


    [HttpPost("register")]
    public async Task<ActionResult<GeneralRequestResponse>> RegisterAccount(AuthentificationRegisterRequest request)
    {
        var checkUser = _repository.User.FindByCondition(x => x.UserName == request.Username || x.Email == request.Email);
        if (checkUser.Count() != 0)
        {
            return BadRequest(new {message = "Account exist"});
        }
        var newUser = new User();
        var generatedSecret = _security.GenerateSecret();
        var password = await _security.Encrypt($"{request.Password}{generatedSecret}");
        newUser.UserName = request.Username;
        newUser.Email = request.Email;
        newUser.Secret = generatedSecret;
        newUser.Password = password;
        _repository.User.Create(newUser);
        await _repository.Save();
        return Ok(new GeneralRequestResponse
        {
            Status = "success",
            Response = "Account created"
        });
    }
    
    [Authorize]
    [HttpGet("getall")]
    public IEnumerable<User> GetAll()
    {
        return _repository.User.FindAll().ToList();
    }
}