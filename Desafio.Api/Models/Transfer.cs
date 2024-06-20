using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Desafio.Api.Models
{
    public class Transfer
    {
        public decimal Valor { get; set; }
        public int IdEnvia { get; set; }
        public int IdRecebe { get; set; }
    }
}