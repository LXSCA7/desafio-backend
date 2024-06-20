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

        [HttpPost("create-clientes")]
        public IActionResult CreateCliente(User cliente)
        {
            cliente.UserType = "Cliente";
            _context.Users.Add(cliente);
            _context.SaveChanges();

            return Ok();
        }

        [HttpPost("create-lojistas")]
        public IActionResult CreateLojista(User lojista)
        {
            lojista.UserType = "Lojista";
            _context.Users.Add(lojista);
            _context.SaveChanges();

            return Ok();
        }
    
        [HttpPost("transferir")]
        public IActionResult Transferir([FromBody]Transfer transfer)
        {
            var envia = _context.Users.SingleOrDefault(c => c.Id == transfer.IdEnvia);
            var recebe = _context.Users.SingleOrDefault(c => c.Id == transfer.IdRecebe);

            if (envia == null || recebe == null)
                return NotFound("Um dos usuários não foi encontrado.");

            if (envia.UserType == "Lojista")
                return BadRequest("Lojistas não podem realizar transferências.");

            if (envia == recebe)
                return BadRequest("Não é possível realizar uma transferência para você mesmo.");

            
            if (envia.Saldo < transfer.Valor)
                return BadRequest("Saldo insuficiente.");

            return Ok("Transferência realizada com sucesso.");
        }
        
    }
}