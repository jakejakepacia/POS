using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.Model
{
    public class OrderHistory
    {
        public int Id { get; set; }

        public DateTime dateTime { get; set; }
        public List<OrderItem> OrderItems { get; set; } = new();

        public decimal Total => OrderItems.Sum(i => i.Product.Price * i.Quantity);

    }
}
