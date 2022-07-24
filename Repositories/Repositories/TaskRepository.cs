using Domain;
using Domain.Models;
using Repositories.Repositories.Interfaces;

namespace Repositories.Repositories;

internal class TaskRepository : RepositoryBase<TaskModel>,ITaskRepository
{
    public TaskRepository(AppDbContext dbContext) : base(dbContext) { }
}