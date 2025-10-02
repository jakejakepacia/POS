using MauiApp1.ViewModel;

namespace MauiApp1;

public partial class HomePage : ContentPage
{
	public HomePage(HomePageViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}