
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiApp1.Model;
using MyApp.Services;

namespace MauiApp1.ViewModel
{

    public partial class AddProductViewModel : ObservableObject
    {

        private readonly ProductService _productService;

        [ObservableProperty]
        string name;

        [ObservableProperty]
        decimal price;

        [ObservableProperty]
        string details;


        public AddProductViewModel(ProductService productService)
        {
            _productService = productService;
        }

        [RelayCommand]
        void Add()
        {
            var product = new Product {Id = 0, Name = Name, Price = Price, Description = Details };
            _productService.AddProduct(product);
            Name = string.Empty;
            Details = string.Empty;
            Price = 0;
        }
    }
}
