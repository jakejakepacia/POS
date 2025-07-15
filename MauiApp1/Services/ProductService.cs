using MauiApp1.Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MyApp.Services
{
    public class ProductService
    {
        private int _nextId = 1;
        public ObservableCollection<Product> Products { get; private set; } = new();

        public void AddProduct(Product product)
        {
            product.Id = _nextId++;
            Products.Add(product);
        }


    }
}
