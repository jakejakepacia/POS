using MauiApp1.Models.Api;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.Interface
{
    public interface IOrderService
    {
        Task<int> CheckOutOrder(OrderRequest orderRequest);
        Task<ObservableCollection<GetOrderResponse>> GetStoreOrders();
        Task<ObservableCollection<GetOrderResponse>> GetStoreOrders(DateTime dateTime);

        event Action NewOrderAdded;
    }
}
