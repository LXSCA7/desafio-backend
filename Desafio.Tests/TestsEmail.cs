using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Desafio.Api.Models;

namespace Desafio.Tests
{
    public class TestsEmail
    {
        [Fact]
        public void ValidEmail_InvalidEmail_ReturnFalse()
        {
            string email = "email.com";

            bool result = Email.VerifyEmail(email);

            Assert.False(result);
        }

        [Fact]
        public void ValidEmail_ValidEmail_ReturnTrue()
        {
            string email = "email@email.com";

            bool result = Email.VerifyEmail(email);

            Assert.True(result);
        }
    }
}