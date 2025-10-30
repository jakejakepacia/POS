using MauiApp1.ViewModel;

namespace MauiApp1.Views;

public partial class SalesPage : ContentPage
{
	public SalesPage(SalesPageViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
    }
}