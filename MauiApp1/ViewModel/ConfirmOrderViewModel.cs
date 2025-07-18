using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiApp1.Model;
using System.Collections.ObjectModel;

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

    }
}
