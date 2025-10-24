using MauiApp1.ViewModel;

namespace MauiApp1;

public partial class HomePage : ContentPage
{
    private readonly HomePageViewModel _viewModel;

    public HomePage(HomePageViewModel vm)
	{
		InitializeComponent();
        BindingContext = _viewModel = vm;
    }

    private void OnScrollViewScrolled(object sender, ScrolledEventArgs e)
    {
        // Just inform the ViewModel when scrolled
        _viewModel.IsScrolledDown = e.ScrollY > 50;
    }
}