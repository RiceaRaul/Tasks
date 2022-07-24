using Microsoft.AspNetCore.Mvc;
using Repositories.Interfaces;
using Tasks_Backend.Models.Requests;

using Domain.Models;
using Microsoft.EntityFrameworkCore;


namespace Tasks_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly IRepositoryWrapper _repository;

     
        public ProjectsController(IRepositoryWrapper repository)
        {
            _repository = repository;      
        }

        [HttpPost("createProject")]
        public Task<ActionResult<Project>> CreateProject(ProjectRequest request)
        {
            var checkProject = _repository.Project.FindByCondition(x => x.ProjectName == request.ProjectName && x.OwnerId == request.ProjectOwner);
            if(checkProject.Count() != 0)
            {
                return Task.FromResult<ActionResult<Project>>(BadRequest(new { message = "Project name exists" }));
            }

            var newProject = new Project
            {
                OwnerId = request.ProjectOwner,
                ProjectName = request.ProjectName
            };
            _repository.Project.Create(newProject);
            _repository.Save();

            return Task.FromResult<ActionResult<Project>>(Ok(newProject));
        }

        [HttpGet("{id}/{includeowner?}/{includetasks?}")]
        public Task<ActionResult<Project>> GetProjectById(int id,bool includeowner=false,bool includetasks=false)
        {
            if (includeowner && includetasks)
            {
                return Task.FromResult<ActionResult<Project>>(Ok(_repository.Project.Include(x => x.Owner, x => x.Tasks)
                    .FirstOrDefault(x => x.Id == id)));
            }
            else if(includeowner)
            {
                return Task.FromResult<ActionResult<Project>>(Ok(_repository.Project.Include(x => x.Owner)
                    .FirstOrDefault(x => x.Id == id)));
            }
            else if(includetasks)
            {
                return Task.FromResult<ActionResult<Project>>(Ok(_repository.Project.Include(x => x.Tasks)
                    .FirstOrDefault(x => x.Id == id)));
            }

            return Task.FromResult<ActionResult<Project>>(Ok(_repository.Project.FindById(id)));

        }
        

        [HttpGet("getTasks/{id}")]
        public Task<ActionResult<List<TaskModel>>>GetTasks(int id)
        {
            var project = _repository.Project.Include(x => x.Tasks).FirstOrDefault(x => x.Id == id);
            if(project != null)
                return Task.FromResult<ActionResult<List<TaskModel>>>(Ok(project.Tasks));
            
            return Task.FromResult<ActionResult<List<TaskModel>>>(Ok(new List<TaskModel>()));
        }
        
        [HttpDelete("delete/{id}")]
        public async Task<ActionResult> DeleteProjectById(int id)
        {
            var project = _repository.Project.FindById(id);
            if (project == null) return BadRequest(new {message = "Project not found"});
            _repository.Project.Delete(project);
            await _repository.Save();
            return Ok();
        }
    }
}
