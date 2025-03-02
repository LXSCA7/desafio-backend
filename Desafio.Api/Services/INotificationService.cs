using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Desafio.Api.Models;

namespace Desafio.Api.Services
{
    public interface INotificationService
    {
        public Task SendNotificationAsync(User user, string message);
    }
}