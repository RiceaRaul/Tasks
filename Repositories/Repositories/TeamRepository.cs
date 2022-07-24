using Domain;
using Domain.Models;
using Repositories.Repositories.Interfaces;

namespace Repositories.Repositories;

internal class TeamRepository : RepositoryBase<Team>,ITeamRepository
{
    public TeamRepository(AppDbContext dbContext) : base(dbContext) { }
}