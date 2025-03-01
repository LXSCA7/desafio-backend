using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Desafio.Api.Models;

namespace Desafio.Api.Services
{
    public interface IUserService
    {
        bool CpfExists(User user);

        bool EmailExists(User user);
        public Task ValidateTransfer(Transfer transfer);
    }
}