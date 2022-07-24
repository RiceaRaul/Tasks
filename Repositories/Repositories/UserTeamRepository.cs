using Domain;
using Domain.Models;
using Repositories.Repositories.Interfaces;

namespace Repositories.Repositories;

internal class UserTeamRepository : RepositoryBase<UserTeam>,IUserTeamRepository
{
    public UserTeamRepository(AppDbContext dbContext) : base(dbContext) { }
}