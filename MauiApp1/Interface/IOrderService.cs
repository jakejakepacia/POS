using MauiApp1.Models.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.Interface
{
    public interface IOrderService
    {
        Task<int> CheckOutOrder(OrderRequest orderRequest);
    }
}
