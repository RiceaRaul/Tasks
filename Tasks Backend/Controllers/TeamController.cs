using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Repositories.Interfaces;
using Tasks_Backend.Attributes;
using Tasks_Backend.Models.Requests;
using Tasks_Backend.Attributes;
namespace Tasks_Backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TeamController : AbstractController
{

   private readonly IRepositoryWrapper _repository;
   
   public TeamController(IRepositoryWrapper repository)
   {
      _repository = repository;
   }

   #region PostRegion

   [Attributes.Authorize]
   [HttpPost("createteam")]
   public async Task<ActionResult<Team>> CreateTeam(TeamRequest request)
   {
      var Owner = GetUserFromContext();
      var CheckTeamExist =
         _repository.Team.FindByCondition(x => x.TeamName == request.TeamName && x.TeamOwnerId == Owner.Id);
      if (CheckTeamExist.Any())
      {
         return BadRequest(new {message = "Team exist"});
      }

      var newTeam = new Team()
      {
         TeamName = request.TeamName,
         TeamOwnerId = Owner.Id
      };
      _repository.Team.Create(newTeam);
      await _repository.Save();
      var newUserInTeam = new UserTeam()
      {
         UserId = Owner.Id,
         TeamId = newTeam.Id
      };
      _repository.UserTeam.Create(newUserInTeam);
      _repository.Save();
      return Ok(newTeam);
   }
   [Authorize]
   [HttpPost("adduser-in-team")]
   public async Task<ActionResult> AddUserInTeam(AddUserTeamRequest request)
   {
      var owner = GetUserFromContext();
      var checkTeam = _repository.Team.FindById(request.TeamId);
      if (checkTeam == null) return BadRequest(new {message = "Team not found"});
      if (checkTeam.TeamOwnerId != owner.Id) return BadRequest(new {message = "You are not a leader"});
      var checkUser = _repository.User.FindById(request.UserId);
      if (checkUser == null) return BadRequest(new {message = "User not found"});
      var newUserTeam = new UserTeam()
      {
         UserId = request.UserId,
         TeamId = request.TeamId
      };
      _repository.UserTeam.Create(newUserTeam);
      await _repository.Save();
      return Ok(new {message = "Success"});
   }

   #endregion
   
   
   [HttpGet("getall")]
   public async Task<ActionResult<List<Team>>> GetAll()
   {
      return Ok(_repository.Team.FindAll().ToList());
   }
   
   
}