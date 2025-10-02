using MauiApp1.ViewModel;

namespace MauiApp1;

public partial class AddProduct : ContentPage
{
	public AddProduct(AddProductViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}