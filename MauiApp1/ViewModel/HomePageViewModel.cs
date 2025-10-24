using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiApp1.Interface;
using MauiApp1.Model;
using MauiApp1.Models.Api;
using MauiApp1.Services;
using MauiApp1.Views;
using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.ViewModel
{
    public partial class HomePageViewModel : ObservableObject, IDisposable
    {
        
        [ObservableProperty]
        public ObservableCollection<GetOrderResponse> storeOrders;

        [ObservableProperty]
        decimal salesToday;

        [ObservableProperty]
        bool isLoading;


        [RelayCommand]
        async Task OrderTap(GetOrderResponse order)
        {
            await Shell.Current.GoToAsync(nameof(OrderDetailsPage), new Dictionary<string, object>
            {
                { "Orders", order }
            });
        }

        private readonly IOrderService _orderService;
        public HomePageViewModel(IOrderService orderService)
        {
            _orderService = orderService;
            PopulateStoreOrders();

          _orderService.NewOrderAdded += PopulateStoreOrders;
        }

        private async void PopulateStoreOrders()
        {
            IsLoading = true;

            StoreOrders = await _orderService.GetStoreOrders();

            
           SalesToday = StoreOrders.Sum(o => o.TotalAmount);
           IsLoading = false;
        }

        public void Dispose()
        {
            _orderService.NewOrderAdded -= PopulateStoreOrders;
        }
    }
}
