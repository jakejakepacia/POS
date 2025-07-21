using MauiApp1.ViewModel;

namespace MauiApp1
{
    public partial class App : Application
    {
        public App(LoginPage loginPage)
        {
            InitializeComponent();
            MainPage = loginPage;
        }

     
    }
}