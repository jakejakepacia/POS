using MauiApp1.Model;
using MauiApp1.Session;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace MyApp.Services
{
    public class ProductService
    {
        private readonly HttpClient _httpClient;
        private int _nextId = 1;
        public ObservableCollection<Product> Products { get; private set; } = new();

        public ProductService()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://practiceapi-uchh.onrender.com/");
            
        }

        public void AddProduct(Product product)
        {
            product.Id = _nextId++;
            Products.Add(product);
        }

        public async Task<List<Product>> GetProducts(int userId)
        {
           
            try
            {
                var url = $"https://jakeposapi.onrender.com/api/Product/{userId}";
                string token = await SecureStorage.GetAsync("auth_token");
                _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);

                var response = await _httpClient.GetAsync(url); 

                if (!response.IsSuccessStatusCode)
                {
                    return new List<Product>(); // or handle/log the error
                }

                var responseJson = await response.Content.ReadAsStringAsync();


                var products = JsonSerializer.Deserialize<List<Product>>(responseJson, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return products ?? new List<Product>();
            }
            catch (JsonException e)
            {
                Console.WriteLine(e.Message);
                // Log exception or return fallback
                return new List<Product>();
            }
        }


    }
}
