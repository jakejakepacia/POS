using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.Models.Api
{
    public class ProductRequest
    {

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public int StoreId { get; set; }
        public int EmployeeId { get; set; }
        public bool IsAddedByOwner { get; set; }
    }
}
