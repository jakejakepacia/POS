using MauiApp1.Interface;
using MauiApp1.Services;
using MauiApp1.Session;
using MauiApp1.ViewModel;
using Microsoft.Extensions.Logging;
using MyApp.Services;

namespace MauiApp1;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

		builder.Services.AddSingleton<MenuPage>();
		builder.Services.AddSingleton<MainViewModel>();

        builder.Services.AddSingleton<ProductService>();

        builder.Services.AddSingleton<AddProduct>();
        builder.Services.AddSingleton<AddProductViewModel>();

        builder.Services.AddSingleton<ReceiptPage>();
        builder.Services.AddSingleton<ReceiptViewModel>();

        builder.Services.AddTransient<ConfirmOrderPage>();
		builder.Services.AddTransient<ConfirmOrderViewModel>();

        builder.Services.AddTransient<LoginPage>();
        builder.Services.AddTransient<LoginViewModel>();

        builder.Services.AddSingleton<HomePage>();
        builder.Services.AddSingleton<HomePageViewModel>();

        builder.Services.AddSingleton<IDialogService, DialogService>();

        builder.Services.AddSingleton<ILoginApiService, LoginApiService>();
        builder.Services.AddSingleton<IOrderService, OrderService>();

		builder.Services.AddSingleton<UserSession>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
