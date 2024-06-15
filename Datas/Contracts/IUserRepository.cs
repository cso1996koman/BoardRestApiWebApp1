﻿using System.Threading;
using System.Threading.Tasks;
using Entities;

namespace Datas.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetByUserAndPass(string username, string password, CancellationToken cancellationToken);
        Task AddAsync(User user, string password, CancellationToken cancellationToken);
        Task UpdateSecurityStampAsync(User user, CancellationToken cancellationToken);
        Task UpdateLastLoginDateAsync(User user, CancellationToken cancellationToken);
    }
}