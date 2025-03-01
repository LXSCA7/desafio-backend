using System.ComponentModel.DataAnnotations;
using Desafio.Api.Enums;
using Microsoft.EntityFrameworkCore;

namespace Desafio.Api.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set ; }

        [Required]
        public string LastName { get; set ; }

        [Required]        
        public decimal Balance { get; set; }
       
        [Required]
        public string Document { get; set; }
        
        [Required]
        public string Email { get; set ; }
        
        [Required]
        public UserType UserType { get; set; }

        [Required]
        public string Password { get; set; }
    }
}