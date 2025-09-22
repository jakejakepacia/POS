using MauiApp1.Interface;
using MauiApp1.Models.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MauiApp1.Services
{
    public class OrderService : IOrderService
    {

        private readonly HttpClient _httpClient;
        public OrderService()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://jakeposapi.onrender.com");
        }

        public async Task<int> CheckOutOrder(OrderRequest orderRequest)
        {
            string token = await SecureStorage.GetAsync("auth_token");
            var storeId = await SecureStorage.GetAsync("storeId");

            if (string.IsNullOrEmpty(storeId) || string.IsNullOrEmpty(token))
            {
                return 0;
            }

            orderRequest.StoreId = int.Parse(storeId);


            var url = "https://jakeposapi.onrender.com/api/Order/checkout";
            var json = JsonSerializer.Serialize(orderRequest);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.PostAsync(url, content);

            if (!response.IsSuccessStatusCode)
            {
                return 0;
            }

            var responseJson = await response.Content.ReadAsStringAsync();
            var loginResponse = JsonSerializer.Deserialize<int>(responseJson);


            return loginResponse;
        }
    }
}
