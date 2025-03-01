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
    public class TransferService(HttpClient _httpClient, IUserRepository _userRepository, IUserService _userService, ILogger<IUserService> _logger) : ITransferService
    {
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

                await Notify(receiver, $"Você recebeu uma transferência no valor de R${transfer.Value}");
                await Notify(sender, $"Transferência realizada com sucesso.");

                await _userRepository.UpdateUsersAsync(users);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.Message} A transferência será revertida.");
                throw;
            }
        }
        public async Task<bool> AuthorizeTransfer()
        {
            var response = await _httpClient.GetAsync("https://util.devi.tools/api/v2/authorize");
            if (!response.IsSuccessStatusCode)
                return false;

            var content = await response.Content.ReadAsStringAsync();
            var jsonResponse = JsonConvert.DeserializeObject<ApiResponse>(content);

            return jsonResponse.Data.Authorization && jsonResponse.Status == "success";
        }

        public async Task Notify(User user, string message)
        {
            Notification notification = new()
            {
                Email = user.Email,
                Message = message
            };
            var response = await _httpClient.PostAsJsonAsync<Notification>("https://util.devi.tools/api/v1/notify", notification);
            if (!response.IsSuccessStatusCode)
                throw new InvalidOperationException("Serviço de notificação está fora do ar. Tente novamente mais tarde.");
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

    class Notification {
        public string Email { get; set; }
        public string Message { get; set; }
    }
}