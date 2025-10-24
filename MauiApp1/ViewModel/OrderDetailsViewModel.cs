using CommunityToolkit.Mvvm.ComponentModel;
using MauiApp1.Model;
using MauiApp1.Models.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.ViewModel
{
    [QueryProperty("Orders", "Orders")]
    public partial class OrderDetailsViewModel : ObservableObject
    {
        [ObservableProperty]
        List<Product> products;

        [ObservableProperty]
        GetOrderResponse orders;

        [ObservableProperty]
        decimal totalAmount;

        [ObservableProperty]
        int orderId;

        partial void OnOrdersChanged(GetOrderResponse value)
        {
            OrderId = value.Id;
            Orders = value;
            TotalAmount = value.TotalAmount;
            Products = value.Products;
        }
    }
}
