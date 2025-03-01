using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Desafio.Api.Models;

namespace Desafio.Api.Repositories
{
    public interface IUserRepository
    {
        public Task AddUserAsync(User user);
        public Task UpdateUserAsync(User user);
        public Task<User> GetUserById(int id);
        public Task UpdateUsersAsync(List<User> users);
        public Task<bool> DocumentExists(string document);
        public Task<bool> EmailExists(string email);
    }
}