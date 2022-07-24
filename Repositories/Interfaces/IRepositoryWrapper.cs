﻿using Repositories.Repositories.Interfaces;

namespace Repositories.Interfaces
{
    public interface IRepositoryWrapper
    {
        IUserRepository User { get; }

        IProjectRepository Project { get; }
        
        ITaskRepository Task { get; }
        Task Save();
    }
}
