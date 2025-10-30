using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiApp1.Interface;
using MauiApp1.Models.Api;
using Microcharts;
using SkiaSharp;
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

        [ObservableProperty]
        public ObservableCollection<GridLength> firstGridRowHeights;

        [ObservableProperty]
        public ObservableCollection<GridLength> secondGridRowHeights;

        [ObservableProperty]
        public ObservableCollection<GridLength> thirdGridRowHeights;

        [ObservableProperty]
        public ObservableCollection<GridLength> fourthGridRowHeights;

        public Chart SalesChart { get; set; }

        public string Button1Color => SelectedButtonIndex == 1 ? "CornflowerBlue" : "LightGray";
        public string Button2Color => SelectedButtonIndex == 2 ? "CornflowerBlue" : "LightGray";
        public string Button3Color => SelectedButtonIndex == 3 ? "CornflowerBlue" : "LightGray";

        public string Bar1Color => SelectedBarIndex == 1 ? "CornflowerBlue" : "LightGray";
        public string Bar2Color => SelectedBarIndex == 2 ? "CornflowerBlue" : "LightGray";
        public string Bar3Color => SelectedBarIndex == 3 ? "CornflowerBlue" : "LightGray";
        public string Bar4Color => SelectedBarIndex == 4 ? "CornflowerBlue" : "LightGray";

        private readonly IOrderService _orderService;
        private readonly ISalesService _salesService;

        public SalesPageViewModel(IOrderService orderService, ISalesService salesService)
        {
            _orderService = orderService;
            _salesService = salesService;

            RefreshSalesBar();


            _orderService.NewOrderAdded += RefreshSalesBar;

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

        private void CreateMicroChart()
        {
            SalesChart = new BarChart
            {
                Entries = new[]
           {
                     new ChartEntry(100) { Label = "Bar 1", ValueLabel = "100", Color = SKColor.Parse("#266489") },
                     new ChartEntry(200) { Label = "Bar 2", ValueLabel = "200", Color = SKColor.Parse("#68B9C0") },
                     new ChartEntry(300) { Label = "Bar 3", ValueLabel = "300", Color = SKColor.Parse("#90D585") },
                     new ChartEntry(400) { Label = "Bar 4", ValueLabel = "400", Color = SKColor.Parse("#F3C151") },
                 }
            };
        }

        private void RefreshSalesBar()
        {
            SelectedBarIndex = 4;
            PopulateSalesBarValues();
            PopulateStoreOrders(DateTime.Now);
        }

        private async void PopulateSalesBarValues()
        {
            FirstBarLabel = DateTime.Today.AddDays(-3).ToString("MMM dd");
            SecondBarLabel = DateTime.Today.AddDays(-2).ToString("MMM dd");
            ThirdBarLabel = DateTime.Today.AddDays(-1).ToString("MMM dd");
            FourthBarLabel = "Today";

            decimal firstBarSales = await _salesService.GetSalesByDate(DateTime.Today.AddDays(-3));
            decimal secondBarSales = await _salesService.GetSalesByDate(DateTime.Today.AddDays(-2));
            decimal thirdBarSales = await _salesService.GetSalesByDate(DateTime.Today.AddDays(-1));
            decimal fourthBarSales = await _salesService.GetSalesByDate(DateTime.Today);

            decimal totalSales = firstBarSales + secondBarSales + thirdBarSales + fourthBarSales;
            double firstRatio = (double)(firstBarSales / totalSales);
            double secondRatio = (double)(secondBarSales / totalSales);
            double thirdRatio = (double)(thirdBarSales / totalSales);
            double fourthRatio = (double)(fourthBarSales / totalSales);

            //first bar value
            FirstGridRowHeights = new ObservableCollection<GridLength>
            {
                new GridLength(1 - firstRatio, GridUnitType.Star),
                new GridLength(firstRatio, GridUnitType.Star)
            };

            SecondGridRowHeights = new ObservableCollection<GridLength>
            {
                 new GridLength(1 - secondRatio, GridUnitType.Star),
                new GridLength(secondRatio, GridUnitType.Star)
            };

            ThirdGridRowHeights = new ObservableCollection<GridLength>
            {
                 new GridLength(1 - thirdRatio, GridUnitType.Star),
                new GridLength(thirdRatio, GridUnitType.Star)
            };

            FourthGridRowHeights = new ObservableCollection<GridLength>
            {
                 new GridLength(1 - fourthRatio, GridUnitType.Star),
                new GridLength(fourthRatio, GridUnitType.Star)
            };
        }

        private void ResetBarValues()
        {
            //first bar value
            FirstGridRowHeights = new ObservableCollection<GridLength>
            {
                new GridLength(1, GridUnitType.Star),
                new GridLength(0, GridUnitType.Star)
            };

            SecondGridRowHeights = new ObservableCollection<GridLength>
            {
                new GridLength(1, GridUnitType.Star),
                new GridLength(0, GridUnitType.Star)
            };

            ThirdGridRowHeights = new ObservableCollection<GridLength>
            {
                  new GridLength(1, GridUnitType.Star),
                new GridLength(0, GridUnitType.Star)
            };

            FourthGridRowHeights = new ObservableCollection<GridLength>
            {
                 new GridLength(1, GridUnitType.Star),
                new GridLength(0, GridUnitType.Star)
            };
        }

        private async void PopulateStoreOrders(DateTime dateTime)
        {

             PopulateSalesBarValues();

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
                    RefreshSalesBar();
                    break;
                case 2:
                    ResetBarValues();
                    TotalSales = 0;
                    break;
                case 3:
                    ResetBarValues();
                    TotalSales = 0;
                    break;
                default:
                    ResetBarValues();
                    TotalSales = 0;
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
                        PopulateStoreOrders(DateTime.Now.AddDays(-2));
                        break;
                    case 3:
                        PopulateStoreOrders(DateTime.Now.AddDays(-1));
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
