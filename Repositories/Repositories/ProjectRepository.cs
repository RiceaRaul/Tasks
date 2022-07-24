using Domain;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repositories.Repositories.Interfaces;
namespace Repositories.Repositories
{
    internal class ProjectRepository : RepositoryBase<Project>,IProjectRepository
    {
        public ProjectRepository(AppDbContext dbContext) : base(dbContext) { }
    }
}
