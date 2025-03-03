using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Desafio.Api.Models;

namespace Desafio.Api.Services
{
    public class NotificationService(HttpClient _httpClient, IConfiguration _configuration) : INotificationService
    {
        private readonly string _baseUrl = _configuration["Requests:NotificationUrl"];
        public async Task SendNotificationAsync(User user, string message)
        {
            Notification notification = new()
            {
                Email = user.Email,
                Message = message
            };
            var response = await _httpClient.PostAsJsonAsync<Notification>(_baseUrl, notification);
            if (!response.IsSuccessStatusCode)
                throw new InvalidOperationException("Serviço de notificação está fora do ar e a transferência será revertida. Tente novamente mais tarde.");
        }
    }
    
    class Notification {
        public string Email { get; set; }
        public string Message { get; set; }
    }
}