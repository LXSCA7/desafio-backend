using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using Desafio.Api.Context;
using Desafio.Api.Models;
using Desafio.Api.Repositories;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Desafio.Api.Services
{
    public class UserService(BankContext _context, 
                             IUserRepository _userRepository) : IUserService
    {
        public bool CpfExists(User user) => _context.Users.Any(u => u.Document == user.Document);
        public bool EmailExists(User user) => _context.Users.Any(u => u.Email == user.Email);

        public async Task ValidateTransfer(Transfer transfer)
        {
            if (transfer.SenderId == transfer.ReceiverId)
                throw new InvalidOperationException("Não é possível fazer uma transferência para você mesmo.");

            if (transfer.Value <= 0)
                throw new InvalidOperationException("Valor de transferência deve ser maior que 0.");

            User sender = await _userRepository.GetUserById(transfer.SenderId); 
            if (sender == new User())
                throw new Exception("Usuário não encontrado.");

            if (sender.Balance < transfer.Value)
                throw new InvalidOperationException("Saldo insuficiente.");
            
            if (sender.UserType == Enums.UserType.RETAILER)
                throw new InvalidOperationException("Lojistas não podem realizar transferências.");

            User receiver = await _userRepository.GetUserById(transfer.ReceiverId);
            if (receiver == new User())
                throw new Exception("Usuário não encontrado.");
        }

    }
}