using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using BCrypt.Net;

namespace Desafio.Api.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        public string NomeCompleto { get; set ; }

        [Required]        
        public decimal Saldo { get; set; }
       
        [Required]
        public string CPF { get; set; }
        
        [Required]
        public string Email { get; set ; }
        
        [Required]
        [RegularExpression("Cliente|Lojista", ErrorMessage = "Usertype deve ser 'Cliente' ou 'Lojista'")]
        public string UserType { get; set; }

        private string _senhaHash;
        
        [Required]
        public string Senha
        {
            set
            {
                _senhaHash = BCrypt.Net.BCrypt.HashPassword(value);
            }
        }
    }
}