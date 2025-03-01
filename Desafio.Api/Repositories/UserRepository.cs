using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using Desafio.Api.Context;
using Desafio.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Desafio.Api.Repositories
{
    public class UserRepository(BankContext _context) : IUserRepository
    {
        public async Task AddUserAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateUserAsync(User user)
        {
            _context.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task<User> GetUserById(int id) => await _context.Users.FindAsync(id);
    
        public async Task UpdateUsersAsync(List<User> users)
        {
            _context.UpdateRange(users);
            await _context.SaveChangesAsync();
        }
        public async Task<bool> DocumentExists(string document) => await _context.Users.AnyAsync(u => u.Document == document);
        public async Task<bool> EmailExists(string email) => await _context.Users.AnyAsync(u => u.Email == email);
    }
    
}