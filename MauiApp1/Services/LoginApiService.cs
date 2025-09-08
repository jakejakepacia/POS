using MauiApp1.Interface;
using MauiApp1.Models.Api;
using MauiApp1.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MauiApp1.Services
{
    public class LoginApiService : ILoginApiService
    {
        private readonly HttpClient _httpClient;
        public LoginApiService()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://practiceapi-uchh.onrender.com/");
        }
        public async Task<LoginResponse> LoginAsync(LoginRequest request)
        {
            var url = "https://practiceapi-uchh.onrender.com/api/Users/login";
            var json = JsonSerializer.Serialize(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(url, content);

            if (!response.IsSuccessStatusCode)
            {
                return new LoginResponse();
            }

            var responseJson = await response.Content.ReadAsStringAsync();
            var loginResponse = JsonSerializer.Deserialize<LoginResponse>(responseJson);


            return loginResponse;
        }
    }
}
