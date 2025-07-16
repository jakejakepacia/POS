using MauiApp1.ViewModel;

namespace MauiApp1;

public partial class ConfirmOrderPage : ContentPage
{
	public ConfirmOrderPage(ConfirmOrderViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}