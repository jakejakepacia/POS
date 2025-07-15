using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiApp1.Model;
using MyApp.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace MauiApp1.ViewModel
{
    public partial class MainViewModel : ObservableObject
    {
        public ObservableCollection<Product> Products { get; }
        public ObservableCollection<Product> SelectedProducts { get; set; }
        private decimal totalPrice;
        public decimal TotalPrice
        {
            get => totalPrice;
            set
            {
                if (totalPrice != value)
                {
                    totalPrice = value;
                    OnPropertyChanged(nameof(TotalPrice));
                }
            }
        }

        public MainViewModel(ProductService productService)
        {
            Items = new ObservableCollection<string>();
            SelectedProducts = new ObservableCollection<Product>();
            Products = productService.Products;

        }

        [ObservableProperty]
        ObservableCollection<string> items;


        [RelayCommand]
        void Add(int id)
        {
            if (id == 0)
                return;

            var selectedProduct = Products.FirstOrDefault(p => p.Id == id);

            if (selectedProduct != null)
            {
                SelectedProducts.Add(selectedProduct);
                CalculateTotal();
            }
        }

        [RelayCommand]
        void Delete(int id)
        {
            if (id == 0)
                return;

            var selectedProduct = SelectedProducts.FirstOrDefault(p => p.Id == id);

            if (selectedProduct != null)
            {
                SelectedProducts.Remove(selectedProduct);
                CalculateTotal();
            }
        }

        private void CalculateTotal()
        {
            TotalPrice = SelectedProducts.Sum(p => p.Price);
        }


    }
}
