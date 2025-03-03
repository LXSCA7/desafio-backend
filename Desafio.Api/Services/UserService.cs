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
    public class UserService(IUserRepository _userRepository) : IUserService
    {
        public async Task AddNewUser(User user)
        {
            user.Document = Document.RemoveDocumentDigits(user.Document);
            if (string.IsNullOrEmpty(user.Document))
                throw new InvalidOperationException("Documento não pode ser vazio.");

            if (await _userRepository.DocumentExists(user.Document))
                throw new InvalidOperationException("Documento já cadastrado.");

            if (await _userRepository.EmailExists(user.Email))
                throw new InvalidOperationException("Email já cadastrado.");

            await _userRepository.AddUserAsync(user);
        }

        public async Task ValidateTransfer(Transfer transfer)
        {
            if (transfer.SenderId == transfer.ReceiverId)
                throw new InvalidOperationException("Não é possível fazer uma transferência para você mesmo.");

            if (transfer.Value <= 0)
                throw new InvalidOperationException("Valor de transferência deve ser maior que 0.");

            User sender = await _userRepository.GetUserById(transfer.SenderId); 
            if (sender == null || sender == new User())
                throw new Exception("Usuário não encontrado.");

            if (sender.Balance < transfer.Value)
                throw new InvalidOperationException("Saldo insuficiente.");
            
            if (sender.UserType == Enums.UserType.RETAILER)
                throw new InvalidOperationException("Lojistas não podem realizar transferências.");

            User receiver = await _userRepository.GetUserById(transfer.ReceiverId);
            if (receiver == null || receiver == new User())
                throw new Exception("Usuário não encontrado.");
        }
    }
}