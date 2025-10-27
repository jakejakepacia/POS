using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.ViewModel
{
    public partial class SalesPageViewModel : ObservableObject
    {
        [ObservableProperty]
        decimal totalSales = 100;

        [ObservableProperty]
        int button1 = 1;

        [ObservableProperty]
        int button2 = 2;

        [ObservableProperty]
        int button3 = 3;

        [ObservableProperty]
        private int selectedButtonIndex;

        public string Button1Color => SelectedButtonIndex == 1 ? "CornflowerBlue" : "LightGray";
        public string Button2Color => SelectedButtonIndex == 2 ? "CornflowerBlue" : "LightGray";
        public string Button3Color => SelectedButtonIndex == 3 ? "CornflowerBlue" : "LightGray";


        public SalesPageViewModel()
        {
            SelectedButtonIndex = 1;

            // When SelectedButtonIndex changes, update color properties
            PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == nameof(SelectedButtonIndex))
                {
                    OnPropertyChanged(nameof(Button1Color));
                    OnPropertyChanged(nameof(Button2Color));
                    OnPropertyChanged(nameof(Button3Color));
                }
            };
        }


        [RelayCommand]
        private void SelectButton(int index)
        {
            SelectedButtonIndex = index;

            switch (index)
            {
                case 1:
                    TotalSales = 100;
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


    }
}
