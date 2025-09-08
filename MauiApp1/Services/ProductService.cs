using MauiApp1.Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
            var url = $"https://practiceapi-uchh.onrender.com/api/Product/{userId}";

            var response = await _httpClient.GetAsync(url); // ✅ Use GET for retrieval

            if (!response.IsSuccessStatusCode)
            {
                return new List<Product>(); // or handle/log the error
            }

            var responseJson = await response.Content.ReadAsStringAsync();

            try
            {
                var products = JsonSerializer.Deserialize<List<Product>>(responseJson, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return products ?? new List<Product>();
            }
            catch (JsonException)
            {
                // Log exception or return fallback
                return new List<Product>();
            }
        }


    }
}
