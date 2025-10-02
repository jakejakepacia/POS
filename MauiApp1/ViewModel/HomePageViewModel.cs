using CommunityToolkit.Mvvm.ComponentModel;
using MauiApp1.Interface;
using MauiApp1.Model;
using MauiApp1.Models.Api;
using MauiApp1.Services;
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

        private readonly IOrderService _orderService;
        public HomePageViewModel(IOrderService orderService)
        {
            _orderService = orderService;
            PopulateStoreOrders();

            _orderService.NewOrderAdded += PopulateStoreOrders;
        }

        private async void PopulateStoreOrders()
        {
            StoreOrders = await _orderService.GetStoreOrders();
        }

        public void Dispose()
        {
            _orderService.NewOrderAdded -= PopulateStoreOrders;
        }
    }
}
