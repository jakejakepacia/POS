using MauiApp1.Helpers;
using MauiApp1.Model;
using MauiApp1.Models.Api;
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
        public ObservableCollection<Product> Products { get; private set; } = new();

        public ProductService()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(ApiConstants.BaseUrl);
            
        }

        public async Task<Boolean> AddProduct(Product product)
        {
            Products.Add(product);

            try
            {
                var url = $"{ApiConstants.BaseUrl}/api/Product/AddProduct";
                string token = await SecureStorage.GetAsync("auth_token");
                var storeId = await SecureStorage.GetAsync("storeId");


                if (!string.IsNullOrEmpty(storeId) && !string.IsNullOrEmpty(token))
                {

                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                    if (int.TryParse(storeId, out int result))
                    {
                        var newProduct = new ProductRequest
                        {
                            Name = product.Name,
                            Description = product.Description,
                            Price = product.Price,
                            StoreId = result,
                            IsAddedByOwner = true,
                            EmployeeId = 0
                        };


                        var json = JsonSerializer.Serialize(newProduct);
                        var content = new StringContent(json, Encoding.UTF8, "application/json");

                        var response = await _httpClient.PostAsync(url, content);

                        return response.IsSuccessStatusCode;

                    }
                }

                return false;   
           
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public async Task<ObservableCollection<Product>> GetProducts(int userId)
        {
           
            try
            {
                var url = $"{ApiConstants.BaseUrl}/api/Product/{userId}";
                string token = await SecureStorage.GetAsync("auth_token");
                _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);

                var response = await _httpClient.GetAsync(url); 

                if (!response.IsSuccessStatusCode)
                {
                    return new ObservableCollection<Product>(); // or handle/log the error
                }

                var responseJson = await response.Content.ReadAsStringAsync();


                var products = JsonSerializer.Deserialize<ObservableCollection<Product>>(responseJson, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                Products = products ?? new ObservableCollection<Product>(); ;

                return Products;
            }
            catch (JsonException e)
            {
                Console.WriteLine(e.Message);
                // Log exception or return fallback
                return new ObservableCollection<Product>();
            }
        }


    }
}
