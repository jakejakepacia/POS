using MauiApp1.ViewModel;

namespace MauiApp1.Views;

public partial class OrderDetailsPage : ContentPage
{
	public OrderDetailsPage(OrderDetailsViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}