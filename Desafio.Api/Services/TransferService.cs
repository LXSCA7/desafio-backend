using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Desafio.Api.Models;
using Desafio.Api.Repositories;
using Microsoft.AspNetCore.Http.HttpResults;
using Newtonsoft.Json;

namespace Desafio.Api.Services
{
    public class TransferService(IUserRepository _userRepository, 
                                 IUserService _userService, 
                                 ILogger<IUserService> _logger, 
                                 INotificationService _notificationService, 
                                 HttpClient _httpClient,
                                 IConfiguration _configuration) : ITransferService
    {
        private readonly string _baseUrl = _configuration["Requests:AuthorizeTransferUrl"];
        public async Task Transfer(Transfer transfer)
        {
            try
            {
                User sender = await _userRepository.GetUserById(transfer.SenderId);
                User receiver = await _userRepository.GetUserById(transfer.ReceiverId);
                await _userService.ValidateTransfer(transfer);

                if (!await AuthorizeTransfer())
                    throw new InvalidOperationException("Transação não autorizada.");

                sender.Balance -= transfer.Value;
                receiver.Balance += transfer.Value;
                
                List<User> users = [sender, receiver];

                await _notificationService.SendNotificationAsync(sender, $"Transferência realizada com sucesso.");
                await _notificationService.SendNotificationAsync(receiver, $"Você recebeu uma transferência no valor de R${transfer.Value}");

                await _userRepository.UpdateUsersAsync(users);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }
        public async Task<bool> AuthorizeTransfer()
        {
            var response = await _httpClient.GetAsync(_baseUrl);
            if (!response.IsSuccessStatusCode)
                return false;

            var content = await response.Content.ReadAsStringAsync();
            var jsonResponse = JsonConvert.DeserializeObject<ApiResponse>(content);

            return jsonResponse.Data.Authorization && jsonResponse.Status == "success";
        }
    }
    class ApiResponse
    {
        public string Status { get; set; }
        public DataContent Data { get; set; }
    }

    class DataContent
    {
        public bool Authorization { get; set; }
    }
}