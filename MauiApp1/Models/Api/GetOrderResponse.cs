using MauiApp1.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.Models.Api
{
    public class GetOrderResponse
    {
        public int Id { get; set; }
        public List<Product> Products { get; set; }
        public decimal TotalAmount { get; set; }
        public int StoreId { get; set; }
        public int EmployeeId { get; set; }
        public bool IsProcessedByOwner { get; set; }
        public DateTime OrderDateTime { get; set; } 
    }
}
