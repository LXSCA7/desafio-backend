using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;


namespace Desafio.Api.Models
{
    public class Email
    {
        public static bool VerifyEmail(string email)
        {
            var emailChecker = new System.ComponentModel.DataAnnotations.EmailAddressAttribute();
            return emailChecker.IsValid(email);
        }
    }
}