using MauiApp1.ViewModel;

namespace MauiApp1;

public partial class ReceiptPage : ContentPage
{
	public ReceiptPage(ReceiptViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}