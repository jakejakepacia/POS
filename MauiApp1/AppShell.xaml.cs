using MauiApp1.Session;
using MauiApp1.ViewModel;

namespace MauiApp1
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(ConfirmOrderPage), typeof(ConfirmOrderPage));
            Routing.RegisterRoute(nameof(ReceiptPage), typeof(ReceiptPage));
            Routing.RegisterRoute(nameof(MenuPage), typeof(MenuPage));
            Routing.RegisterRoute(nameof(AddProduct), typeof(AddProduct));
        }

        private void OnLogoutClicked(object sender, EventArgs e)
        {
            SecureStorage.Remove("auth_token");
            SecureStorage.Remove("storeId");

            var loginPage = App.Current.Handler.MauiContext.Services.GetService<LoginPage>();
            Application.Current.MainPage = loginPage;
        }

        private async void OnTakeOrderClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(MenuPage));
            Shell.Current.FlyoutIsPresented = false;
        }

        private async void OnAddProductClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(AddProduct));
            Shell.Current.FlyoutIsPresented = false;
        }
    }
}
