using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.Models.Api
{
    public class OrderRequest
    {
        public required List<int> ProductIds { get; set; }
        public decimal TotalAmount { get; set; }
        public int StoreId { get; set; }
        public int EmployeeId { get; set; }
    }
}
