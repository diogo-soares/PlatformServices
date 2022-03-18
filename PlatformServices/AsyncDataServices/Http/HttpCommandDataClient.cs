using System.Threading.Tasks;
using PlatformServices.Dtos;
using System.Net.Http;
using Microsoft.Extensions.Configuration;
using System.Text.Json;
using System.Text;
using System;

namespace PlatformServices.SyncDataServices.Http
{
    public class HttpCommandDataClient : ICommandDataCliente
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public HttpCommandDataClient(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task SendPlatformToCommand(PlatformReadDto plat)
        {
            var httpContent = new StringContent(
                JsonSerializer.Serialize(plat),
                Encoding.UTF8,
                "application/json");

            var response = await _httpClient.PostAsync($"{_configuration["CommandServices"]}", httpContent);

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Sync Post to CommandService Ok");
            }
            else
            {
                Console.WriteLine("Sync Post to CommandService was Not Ok!");
            }
        }
    }
}