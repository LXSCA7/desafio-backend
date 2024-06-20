using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Desafio.Api.Context;
using Desafio.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Desafio.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DesafioController : ControllerBase
    {
        private readonly DesafioContext _context;
        private readonly HttpClient _httpClient;
        public DesafioController(DesafioContext context, IHttpClientFactory httpClientFactory)
        {
            _context = context;
            _httpClient = httpClientFactory.CreateClient();
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
        public async Task<IActionResult> Transferir(Transfer transfer)
        {
            var envia = _context.Users.SingleOrDefault(c => c.Id == transfer.IdEnvia);
            var recebe = _context.Users.SingleOrDefault(c => c.Id == transfer.IdRecebe);

            var response = await _httpClient.GetAsync("https://util.devi.tools/api/v2/authorize");

            if (!response.IsSuccessStatusCode)
                return BadRequest("Transferência não autorizada.");

            var content = await response.Content.ReadAsStringAsync();
            var responseObject = JsonConvert.DeserializeObject<dynamic>(content);

            var status = responseObject.status;
            if (status != "success")
                return BadRequest("Transferência não autorizada.");

            if (envia == null || recebe == null)
                return NotFound("Um dos usuários não foi encontrado.");

            if (envia.UserType == "Lojista")
                return BadRequest("Lojistas não podem realizar transferências.");

            if (envia == recebe)
                return BadRequest("Não é possível realizar uma transferência para você mesmo.");

            
            if (envia.Saldo < transfer.Valor)
                return BadRequest("Saldo insuficiente.");

            envia.Saldo -= transfer.Valor;
            recebe.Saldo += transfer.Valor;
            _context.Update(envia);
            _context.Update(recebe);
            _context.SaveChanges();
            return Ok("Transferência realizada com sucesso.");
        }
        
    }
}