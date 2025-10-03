using MauiApp1.Helpers;
using MauiApp1.Session;
using MauiApp1.ViewModel;
using System.IdentityModel.Tokens.Jwt;
using static Android.Renderscripts.ScriptGroup;

namespace MauiApp1;

public partial class LoginPage : ContentPage
{
	private readonly UserSession _userSession;

	public LoginPage(LoginViewModel vm, UserSession userSession)
	{
		InitializeComponent();
		BindingContext = vm;
		_userSession = userSession;
		CheckAutoLogin();

    }

	private async void CheckAutoLogin()
	{
		try
		{
			var token = await SecureStorage.GetAsync("auth_token");
			var storeId = await SecureStorage.GetAsync("storeId");
			bool isExpired = JwtHelper.IsJwtExpired(token);

			if (!string.IsNullOrEmpty(storeId) && !string.IsNullOrEmpty(token) && !isExpired)
			{
				if (int.TryParse(storeId, out int result))
				{
					_userSession.SetStoreId(result);
				}

				// Navigate to AppShell after login
				Application.Current.MainPage = new AppShell();
			}
		}
		catch (Exception ex)
		{
		}
	}
}