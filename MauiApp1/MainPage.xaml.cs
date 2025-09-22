using MauiApp1.ViewModel;

namespace MauiApp1
{
    public partial class MenuPage : ContentPage
    {
        int count = 0;

        public MenuPage(MainViewModel vm)
        {
            InitializeComponent();
            BindingContext = vm; 
        }



    }

}
