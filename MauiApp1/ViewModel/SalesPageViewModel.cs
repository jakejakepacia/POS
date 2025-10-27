using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiApp1.Interface;
using MauiApp1.Models.Api;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.ViewModel
{
    public partial class SalesPageViewModel : ObservableObject
    {
        [ObservableProperty]
        public ObservableCollection<GetOrderResponse> storeOrders;

        [ObservableProperty]
        decimal totalSales = 100;

        [ObservableProperty]
        int button1 = 1;

        [ObservableProperty]
        int button2 = 2;

        [ObservableProperty]
        int button3 = 3;

        [ObservableProperty]
        int barIndex1 = 1;

        [ObservableProperty]
        int barIndex2 = 2;

        [ObservableProperty]
        int barIndex3 = 3;

        [ObservableProperty]
        int barIndex4 = 4;

        [ObservableProperty]
        private int selectedBarIndex = 4;

        [ObservableProperty]
        string firstBarLabel;

        [ObservableProperty]
        string secondBarLabel;

        [ObservableProperty]
        string thirdBarLabel;

        [ObservableProperty]
        string fourthBarLabel;

        [ObservableProperty]
        private int selectedButtonIndex = 1;

        public string Button1Color => SelectedButtonIndex == 1 ? "CornflowerBlue" : "LightGray";
        public string Button2Color => SelectedButtonIndex == 2 ? "CornflowerBlue" : "LightGray";
        public string Button3Color => SelectedButtonIndex == 3 ? "CornflowerBlue" : "LightGray";

        public string Bar1Color => SelectedBarIndex == 1 ? "CornflowerBlue" : "LightGray";
        public string Bar2Color => SelectedBarIndex == 2 ? "CornflowerBlue" : "LightGray";
        public string Bar3Color => SelectedBarIndex == 3 ? "CornflowerBlue" : "LightGray";
        public string Bar4Color => SelectedBarIndex == 4 ? "CornflowerBlue" : "LightGray";

        private readonly IOrderService _orderService;

        public SalesPageViewModel(IOrderService orderService)
        {
            _orderService = orderService;

            PopulateStoreOrders(DateTime.Now);
            // When SelectedButtonIndex changes, update color properties
            PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == nameof(SelectedButtonIndex))
                {
                    OnPropertyChanged(nameof(Button1Color));
                    OnPropertyChanged(nameof(Button2Color));
                    OnPropertyChanged(nameof(Button3Color));
                }

                if (e.PropertyName == nameof(SelectedBarIndex))
                {
                    OnPropertyChanged(nameof(Bar1Color));
                    OnPropertyChanged(nameof(Bar2Color));
                    OnPropertyChanged(nameof(Bar3Color));
                    OnPropertyChanged(nameof(Bar4Color));
                }
            };
        }


        private void PopulateDailySalesDate()
        {
            FirstBarLabel = DateTime.Today.AddDays(-3).ToString("MMM dd");
            SecondBarLabel = DateTime.Today.AddDays(-2).ToString("MMM dd");
            ThirdBarLabel = DateTime.Today.AddDays(-1).ToString("MMM dd");
            FourthBarLabel = "Today";
        }

        private async void PopulateStoreOrders(DateTime dateTime)
        {
           StoreOrders = await _orderService.GetStoreOrders(dateTime);

            TotalSales = StoreOrders.Sum(o => o.TotalAmount);
        }


        [RelayCommand]
        private void SelectButton(int index)
        {
            SelectedButtonIndex = index;

            switch (index)
            {
                case 1:
                    PopulateDailySalesDate();
                    break;
                case 2:
                    TotalSales = 500;
                    break;
                case 3:
                    TotalSales = 1000;
                    break;
                default:
                     TotalSales = 100;
                    break;
            }
        }

        [RelayCommand]
        private void SelectBar(int index)
        {
            SelectedBarIndex = index;

            if (SelectedButtonIndex == 1)
            {
                switch (index)
                {
                    case 1:
                        PopulateStoreOrders(DateTime.Now.AddDays(-3));
                        break;
                    case 2:
                        TotalSales = 500;
                        PopulateStoreOrders(DateTime.Now.AddDays(-2));
                        break;
                    case 3:
                        PopulateStoreOrders(DateTime.Now.AddDays(-1));
                        TotalSales = 1000;
                        break;
                    case 4:
                        PopulateStoreOrders(DateTime.Now);
                        break;
                    default:
                        PopulateStoreOrders(DateTime.Now);
                        break;
                }
            }
          
        }


    }
}
