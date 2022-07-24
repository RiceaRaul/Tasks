using Domain;
using Repositories.Interfaces;
using Repositories.Repositories.Interfaces;
using Repositories.Repositories;
// ReSharper disable All

namespace Repositories
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private AppDbContext _dbContext;

        private IUserRepository _user;

        private IProjectRepository _project;

        private ITaskRepository _task;
        
        private ITeamRepository _team;
        
        private IUserTeamRepository _userteam;
        
        public RepositoryWrapper(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IUserRepository User
        {
            get
            {
                if (_user == null)
                    _user = new UserRepository(_dbContext);

                return _user;
            }
        }

        public IProjectRepository Project
        {
            get
            {
                if (_project == null)
                    _project = new ProjectRepository(_dbContext);

                return _project;
            }
        }
        
        public ITaskRepository Task
        {
            get
            {
                if (_task == null)
                    _task = new TaskRepository(_dbContext);

                return _task;
            }
        }
        
        public ITeamRepository Team
        {
            get
            {
                if (_team == null)
                    _team = new TeamRepository(_dbContext);

                return _team;
            }
        }
        
        public IUserTeamRepository UserTeam
        {
            get
            {
                if (_userteam == null)
                    _userteam = new UserTeamRepository(_dbContext);

                return _userteam;
            }
        }

        public async Task Save()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
