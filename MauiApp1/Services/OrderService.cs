using MauiApp1.Helpers;
using MauiApp1.Interface;
using MauiApp1.Models.Api;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MauiApp1.Services
{
    public class OrderService : IOrderService
    {

        private readonly HttpClient _httpClient;
        public ObservableCollection<GetOrderResponse> StoreOrders { get; private set; } = new ();

        public event Action NewOrderAdded;

        public OrderService()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(ApiConstants.BaseUrl);
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


            var url = $"{ApiConstants.BaseUrl}/api/Order/checkout";
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

            NewOrderAdded?.Invoke();
            return loginResponse;
        }

        public async Task<ObservableCollection<GetOrderResponse>> GetStoreOrders()
        {
            string token = await SecureStorage.GetAsync("auth_token");
            var storeId = await SecureStorage.GetAsync("storeId");

            if (string.IsNullOrEmpty(storeId) || string.IsNullOrEmpty(token))
            {
                return new ObservableCollection<GetOrderResponse>();
            }

            var url = $"{ApiConstants.BaseUrl}/api/Order/{storeId}";
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                return new ObservableCollection<GetOrderResponse>(); // or handle/log the error
            }

            var responseJson = await response.Content.ReadAsStringAsync();


            var storeOrders = JsonSerializer.Deserialize<ObservableCollection<GetOrderResponse>>(responseJson, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            StoreOrders = storeOrders ?? new ObservableCollection<GetOrderResponse>();

            return StoreOrders; 

        }

        public async Task<ObservableCollection<GetOrderResponse>> GetStoreOrders(DateTime dateTime)
        {
            string token = await SecureStorage.GetAsync("auth_token");
            var storeId = await SecureStorage.GetAsync("storeId");

            if (string.IsNullOrEmpty(storeId) || string.IsNullOrEmpty(token))
            {
                return new ObservableCollection<GetOrderResponse>();
            }

            string dateString = dateTime.ToString("yyyy-MM-ddTHH:mm:ss");

            var url = $"{ApiConstants.BaseUrl}/api/Order/{storeId}/{dateString}";
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                return new ObservableCollection<GetOrderResponse>(); // or handle/log the error
            }

            var responseJson = await response.Content.ReadAsStringAsync();


            var storeOrders = JsonSerializer.Deserialize<ObservableCollection<GetOrderResponse>>(responseJson, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            StoreOrders = storeOrders ?? new ObservableCollection<GetOrderResponse>();

            return StoreOrders;


            }
        }
    }
