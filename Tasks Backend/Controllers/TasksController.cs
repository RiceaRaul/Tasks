using Microsoft.AspNetCore.Mvc;
using Repositories.Interfaces;
using Tasks_Backend.Models.Requests;
using Domain.Models;

namespace Tasks_Backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TasksController : Controller
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
}