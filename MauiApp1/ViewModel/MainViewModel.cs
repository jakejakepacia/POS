using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Java.Lang.Annotation;
using MauiApp1.Model;
using MyApp.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace MauiApp1.ViewModel
{
    public partial class MainViewModel : ObservableObject
    {
        public MainViewModel(ProductService productService)
        {
            SelectedProducts = new ObservableCollection<Product>();
            Products = productService.Products;
            ButtonClickedCommand = new RelayCommand(OnButtonClicked);
            IsActive = true;
        }

        public ObservableCollection<Product> Products { get; }
        public ObservableCollection<Product> SelectedProducts { get; set; }

        [ObservableProperty]
        public bool isActive;


        [RelayCommand]
        void Add(int id)
        {
            if (id == 0)
                return;

            var selectedProduct = Products.FirstOrDefault(p => p.Id == id);

            if (selectedProduct != null)
            {
                SelectedProducts.Add(selectedProduct);
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
            }
        }

        [RelayCommand]
        async Task Tap()
        {
            await Shell.Current.GoToAsync(nameof(ConfirmOrderPage));
        }

        public ICommand ButtonClickedCommand { get; }

        private async void OnButtonClicked()
        {
            IsActive = false;
            var orders = ConvertToOrderItems();

            await Shell.Current.GoToAsync(nameof(ConfirmOrderPage), new Dictionary<string, object>
            {
                { "Orders", orders }
            });

            IsActive = true;

        }

        public ObservableCollection<OrderItem> ConvertToOrderItems()
        {
            var orderItems = SelectedProducts
                .GroupBy(p => p.Id) // group by product ID
                .Select(g => new OrderItem
                {
                    Product = g.First(),    // use the first product instance
                    Quantity = g.Count(),  // quantity equals number of times product appears
                    SubTotal = g.First().Price * g.Count()
                });

            return new ObservableCollection<OrderItem>(orderItems);
        }

    }
}
