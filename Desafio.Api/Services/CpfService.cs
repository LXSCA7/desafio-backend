using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Desafio.Api.Context;
using Desafio.Api.Interfaces;
using Desafio.Api.Models;

namespace Desafio.Api.Services
{
    public class CpfService : ICpfService
    {
        private readonly DesafioContext _context;
        public CpfService(DesafioContext context)
        {
            _context = context;
        }

        public bool CpfExists(User user)
        {
            return _context.Users.Any(u => u.CPF == user.CPF);
        }
    }
}