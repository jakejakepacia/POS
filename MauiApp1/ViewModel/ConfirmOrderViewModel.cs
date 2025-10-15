using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiApp1.Interface;
using MauiApp1.Model;
using MauiApp1.Models.Api;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace MauiApp1.ViewModel
{
    [QueryProperty("Orders", "Orders")]
    public partial class ConfirmOrderViewModel : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<OrderItem> orders;

        [ObservableProperty]
        decimal totalPrice;


        // MVVM Toolkit auto-generates this call when 'Orders' is set
        partial void OnOrdersChanged(ObservableCollection<OrderItem> value)
        {
            UpdateTotal();
        }

        private void UpdateTotal()
        {
            TotalPrice = Orders?.Sum(o => o.SubTotal) ?? 0;
        }

        [RelayCommand]
        void Delete(int id)
        {
            //if (id == 0)
            //    return;

            //var selectedProduct = SelectedProducts.FirstOrDefault(p => p.Id == id);

            //if (selectedProduct != null)
            //{
            //    SelectedProducts.Remove(selectedProduct);
            //   // CalculateTotal();
            //}
        }

        private readonly IDialogService _dialogService;
        private readonly IOrderService _orderService;

        public ICommand ShowAlertCommand { get; }

        public ConfirmOrderViewModel(IDialogService dialogService, IOrderService orderService)
        {
            _dialogService = dialogService;
            _orderService = orderService;
            ShowAlertCommand = new Command(async () => await ShowAlertAsync());
        }

        private async Task ShowAlertAsync()
        {
            bool isConfirmed = await _dialogService.ShowConfirmationAsync("Confirm", "Confirm orders", "Confirm", "Cancel");

            if (isConfirmed)
            {
                var productIds = Orders.Where(item => item.Product != null)
                    .Select(item => item.Product.Id)
                    .ToList();
                var totalAmont = Orders.Sum(item => item.SubTotal);

                var newOrder = new OrderRequest {
                    ProductIds = productIds,
                    TotalAmount = totalAmont,
                };

                var newOrderId = await _orderService.CheckOutOrder(newOrder);

                if (newOrderId > 0){
                    await _dialogService.ShowAlertAsync($"Order #{newOrderId} confirmed successfully.", "Order Confirmed", "Confirm");
                
                    await Shell.Current.GoToAsync("//HomePage/ReceiptPage");
                }

            }
        }

    }
}
