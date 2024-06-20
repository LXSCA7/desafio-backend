using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BCrypt.Net;

namespace Desafio.Api.Models
{
    public  class Pessoa
    {
        public int Id { get; set; }
        public string NomeCompleto { get; set ; }
        public string CPF { get; set; }
        public string Email { get; set ; }
        
        private string _senhaHash;
        public string Senha
        {
            set
            {
                _senhaHash = BCrypt.Net.BCrypt.HashPassword(value);
            }
        }
    }
}