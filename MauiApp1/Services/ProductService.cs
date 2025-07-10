using MauiApp1.Model;
using System.Collections.Generic;

namespace MyApp.Services
{
    public static class ProductService
    {
        public static List<Product> GetSampleProducts()
        {
            return new List<Product>
            {
                new Product
                {
                    Id = 1,
                    Name = "Kimchi Rice",
                    Description = "Eco-friendly baby spoon made from bamboo.",
                    Price = 129.99M
                },
                new Product
                {
                    Id = 2,
                    Name = "Samgy Set",
                    Description = "Silicone training fork for toddlers.",
                    Price = 99.50M
                },
                new Product
                {
                    Id = 3,
                    Name = "Kimbop",
                    Description = "Silicone training fork for toddlers.",
                    Price = 99.50M
                },
                new Product
                {
                    Id = 4,
                    Name = "Iced Tea",
                    Description = "Silicone training fork for toddlers.",
                    Price = 99.50M
                },
                new Product
                {
                    Id = 5,
                    Name = "Extra rice",
                    Description = "Silicone training fork for toddlers.",
                    Price = 99.50M
                }

            };
        }
    }
}
