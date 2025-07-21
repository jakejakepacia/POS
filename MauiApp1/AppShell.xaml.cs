namespace MauiApp1
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(ConfirmOrderPage), typeof(ConfirmOrderPage));
            Routing.RegisterRoute(nameof(ReceiptPage), typeof(ReceiptPage));
        }
    }
}
