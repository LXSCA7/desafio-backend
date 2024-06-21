using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Desafio.Api.Models;

namespace Desafio.Api.Interfaces
{
    public interface ICpfService
    {
        bool CpfExists(User user);
    }
}