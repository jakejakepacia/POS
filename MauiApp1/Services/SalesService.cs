using MauiApp1.Helpers;
using MauiApp1.Interface;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xamarin.Google.Crypto.Tink.Shaded.Protobuf;

namespace MauiApp1.Services
{
    public class SalesService : ISalesService
    {
        private readonly HttpClient _httpClient;

        public SalesService()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(ApiConstants.BaseUrl);
        }
        public async Task<decimal> GetSalesByDate(DateTime date)
        {
            string token = await SecureStorage.GetAsync("auth_token");
            var storeId = await SecureStorage.GetAsync("storeId");

            if (string.IsNullOrEmpty(storeId) || string.IsNullOrEmpty(token))
            {
                return 0.0m;
            }

            string dateString = date.ToString("yyyy-MM-ddTHH:mm:ss");
            var url = $"{ApiConstants.BaseUrl}/api/Sales/{storeId}/{dateString}";
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                return 0.0m; // or handle/log the error
            }

            var responseJson = await response.Content.ReadAsStringAsync();


            var sales = JsonSerializer.Deserialize<decimal>(responseJson, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return sales;
        }
    }
}
