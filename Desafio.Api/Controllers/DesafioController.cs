using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Desafio.Api.Context;
using Desafio.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Desafio.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DesafioController : ControllerBase
    {
        private readonly DesafioContext _context;
        public DesafioController(DesafioContext context)
        {
            _context = context;
        }

        // endpoints

        [HttpPost]
        public IActionResult Create(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();

            return Ok();
        }
    }
}