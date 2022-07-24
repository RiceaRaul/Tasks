using Microsoft.AspNetCore.Mvc;
using Repositories.Interfaces;
using Tasks_Backend.Models.Requests;
using Domain.Models;
using Tasks_Backend.Attributes;

namespace Tasks_Backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TasksController : AbstractController
{
    private readonly IRepositoryWrapper _repository;
    
    public TasksController(IRepositoryWrapper repository) 
    {
        _repository = repository;      
    }

    [HttpPost("createTask")]
    public async Task<ActionResult<TaskModel>> CreateTask(TaskRequest request)
    {
        var checkTask = _repository.Task.FindByCondition(x =>
            x.TaskName == request.TaskName && x.TaskProjectId == request.TaskProjectId);
        if (checkTask.Any()) return BadRequest(new {message = "Task exist"});
        var newTask = new TaskModel()
        {
            TaskName = request.TaskName,
            TaskProjectId = request.TaskProjectId,
            TaskStatus = 0
        };
        _repository.Task.Create(newTask);
        await _repository.Save();
        return Ok(newTask);
    }

    [HttpGet("{id}/{includeproject}")]
    public async Task<ActionResult<TaskModel>> GetTaskById(int id,bool includeproject = false)
    {
        var Task = _repository.Task.FindById(id);
        if (includeproject)
        {
            Task = _repository.Task.Include(x => x.TaskProject).First(x => x.Id == id);
        }
        if (Task != null)   return Ok(Task);
        return BadRequest(new {message = "Task not found"});
    }
    
    [HttpGet("gettasksfromprojects/{id}/{includeproject}")]
    public async Task<ActionResult<List<TaskModel>>> GetTaskFromProject(int id,bool includeproject = false)
    {
        var Task = _repository.Task.FindByCondition(x=>x.TaskProjectId == id).ToList();
        if (includeproject)
        {
            Task = _repository.Task.Include(x => x.TaskProject).Where(x => x.TaskProjectId ==id).ToList();
        }
        if (Task != null)   return Ok(Task);
        return BadRequest(new {message = "Task not found"});
    }
    [Authorize]
    [HttpPut("updatestatus/{id}/{status}")]
    public async Task<ActionResult>  UpdateTaskStatus(int id, int status = 0)
    {
        var user = GetUserFromContext();
        var findTasks = _repository.Task.Include(x => x.TaskProject).Where(x => x.Id == id);
        if (!findTasks.Any())
        {
            return BadRequest(new {message = "Task not found"});
        }
        if (status < 0 || status > 3)
        {
            return BadRequest(new {message = "Invalid Status"});
        }
        var findTask = findTasks.First();
        if(findTask.TaskProject.OwnerId != user.Id) 
            return BadRequest(new {message = "Access denied"}); 
        
        findTask.TaskStatus = status;
        _repository.Task.Update(findTask);
        await _repository.Save();
        return Ok(new {message = "Success"});
    }

    [Authorize]
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteTask(int id)
    {
        var user = GetUserFromContext();
        var getTask = _repository.Task.Include(x => x.TaskProject).FirstOrDefault(x => x.Id == id);
        if (getTask != null && user != null)
        {
            if (getTask.TaskProject.OwnerId == user.Id)
            {
                _repository.Task.Delete(getTask);
                await _repository.Save();
                return Ok();
            }

            return BadRequest(new {message = "Access denied"});
        }

        return BadRequest(new {message = "Task not found"});
    }
}