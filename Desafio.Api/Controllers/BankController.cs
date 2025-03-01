using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Threading.Tasks;
using Desafio.Api.Context;
using Desafio.Api.Dtos;
using Desafio.Api.Enums;
using Desafio.Api.Models;
using Desafio.Api.Repositories;
using Desafio.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Newtonsoft.Json;

namespace Desafio.Api.Controllers
{
    [ApiController]
    [Route("api/")]
    public class DesafioController(ITransferService _transferService, IUserRepository _userRepository, BankContext _bankContext) : ControllerBase
    {

        // endpoints
        [HttpPost("create-costumer")]
        public async Task<IActionResult> CreateCostumer([FromBody]UserDTO userDto)
        {
            User user = userDto.ToUser(UserType.COSTUMER);
            // user.Document = CPFs.RemoveDigitsCPF(user.Document);
            // user.Id = 0;
            await _userRepository.AddUserAsync(user);
            // await _bankContext.Users.AddAsync(user);
            // await _bankContext.SaveChangesAsync();

            return Ok("Cliente registrado com sucesso.");
        }

        [HttpPost("create-retailer")]
        public async Task<IActionResult> CreateRetailer([FromBody]UserDTO userDto)
        {
            User user = userDto.ToUser(UserType.RETAILER);
            // user.Document = CPFs.RemoveDigitsCPF(user.Document);
            await _userRepository.AddUserAsync(user);

            return Ok("Lojista registrado com sucesso.");
        }
    
        [HttpPost("transfer")]
        public async Task<IActionResult> Transferir(Transfer transfer)
        {
            try 
            {
                await _transferService.Transfer(transfer);
                return Ok("Transferência realizada com sucesso.");
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        
    }
}