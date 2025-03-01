using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Desafio.Api.Models;

namespace Desafio.Api.Services
{
    public interface ITransferService
    {
        public Task<bool> AuthorizeTransfer();
        public Task Transfer(Transfer transfer);
        public Task Notify(User sender, User receiver, Transfer transfer);
    }
}