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

		builder.Services.AddSingleton<MainPage>();
		builder.Services.AddSingleton<MainViewModel>();

        builder.Services.AddSingleton<ProductService>();

        builder.Services.AddSingleton<AddProduct>();
        builder.Services.AddSingleton<AddProductViewModel>();

		builder.Services.AddTransient<ConfirmOrderPage>();
		builder.Services.AddTransient<ConfirmOrderViewModel>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
