using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Desafio.Api.Enums;
using Desafio.Api.Models;

namespace Desafio.Api.Dtos
{
    public class UserDTO
    {
        public string FirstName { get; set; }
        public string LastName  { get; set; }
        public decimal Balance  { get; set; }       
        public string Document  { get; set; }
        public string Email     { get; set; }
        public string Password  { get; set; }
    
        public User ToUser(UserType userType) {
            User user = new() {
                FirstName = FirstName,
                LastName  = LastName,
                Balance   = Balance,
                Document = Document,
                Email     = Email,
                Password  = HashPassword(Password),
                UserType  = userType,
            };

            return user;
        }

        public string HashPassword(string password) => BCrypt.Net.BCrypt.HashPassword(password);
    }
}