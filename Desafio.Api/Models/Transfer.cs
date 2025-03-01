using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Desafio.Api.Models
{
    public class Transfer
    {
        public decimal Value { get; set; }
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
    }
}