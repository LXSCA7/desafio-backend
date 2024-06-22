using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Threading.Tasks;
using Desafio.Api.Context;
using Desafio.Api.Interfaces;
using Desafio.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Newtonsoft.Json;

namespace Desafio.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DesafioController : ControllerBase
    {
        private readonly DesafioContext _context;
        private readonly HttpClient _httpClient;
        private readonly ICpfService _cpfService;
        public DesafioController(DesafioContext context, IHttpClientFactory httpClientFactory, ICpfService cpfService)
        {
            _context = context;
            _httpClient = httpClientFactory.CreateClient();
            _cpfService = cpfService;
        }

        // endpoints
        [HttpPost("create-cliente")]
        public IActionResult CreateCliente([FromBody]UserBase user)
        {
            Cliente cliente = new() {
                NomeCompleto = user.FullName,
                CPF = user.CPF,
                Saldo = user.Balance,
                Senha = user.Password,
                Email = user.Email,
            };
            cliente.UserType = "Cliente";
            cliente.CPF = CPFs.RemoveDigitsCPF(cliente.CPF);
            if(!CPFs.ValidCPF(cliente.CPF))
                return BadRequest("CPF inválido");

            cliente.CPF = CPFs.FormatCPF(cliente.CPF);

            if (_cpfService.CpfExists(cliente))
                return BadRequest("O CPF deve ser único no sistema");

            _context.Users.Add(cliente);
            _context.SaveChanges();

            return Ok();
        }

        [HttpPost("create-lojista")]
        public IActionResult CreateLojista([FromBody]UserBase user)
        {
            Cliente lojista = new() {
                NomeCompleto = user.FullName,
                CPF = user.CPF,
                Saldo = user.Balance,
                Senha = user.Password,
                Email = user.Email,
                UserType = "Lojista"
            };

            if (_cpfService.CpfExists(lojista))
                return BadRequest("O CPF deve ser único no sistema");

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