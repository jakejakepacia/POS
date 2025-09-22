using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Java.Lang.Annotation;
using MauiApp1.Model;
using MauiApp1.Session;
using MyApp.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace MauiApp1.ViewModel
{
    public partial class MainViewModel : ObservableObject
    {
        private readonly ProductService _productService;
        private readonly UserSession _userSession;

        public MainViewModel(ProductService productService, UserSession userSession)
        {
            _productService = productService;
            _userSession = userSession;

            SelectedProducts = new ObservableCollection<Product>();
            ButtonClickedCommand = new RelayCommand(OnButtonClicked);
            IsActive = true;

            if (_userSession.IsLoggedIn)
            {
                LoadProducts();
            }
        }

        [ObservableProperty]
        public ObservableCollection<Product> products;

        public ObservableCollection<Product> SelectedProducts { get; set; }

        [ObservableProperty]
        public bool isActive;

        [ObservableProperty]
        public bool isLoading;

        public async Task LoadProducts()
        {
            if (!_userSession.UserId.HasValue) 
                return;

            IsLoading = true;
            Products = await _productService.GetProducts(_userSession.UserId.Value);
            IsLoading = false;
        }


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
