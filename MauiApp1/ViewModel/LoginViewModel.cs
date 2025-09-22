using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Java.Lang.Annotation;
using MauiApp1.Interface;
using MauiApp1.Models.Api;
using MauiApp1.Services;
using MauiApp1.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MauiApp1.ViewModel
{
    public partial class LoginViewModel : ObservableObject
    {

        [ObservableProperty]
        string username;

        [ObservableProperty]
        string password;

        [ObservableProperty]
        bool isLoading;

        private readonly IDialogService _dialogService;
        private readonly ILoginApiService _apiService;
        private readonly UserSession _userSession;

        public LoginViewModel(ILoginApiService apiService, IDialogService dialogService, UserSession userSession)
        {
            _dialogService = dialogService;
            _apiService = apiService;
            _userSession = userSession;
        }

        [RelayCommand]
        private async Task LoginAsync()
        {
            if(string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password))
            {
                await _dialogService.ShowAlertAsync("Login Failed", "Please enter username and password", "Okay");
            }
            else
            {
                IsLoading = true;
                var request = new LoginRequest
                {
                    Username = Username,
                    Password = Password
                };

                var result = await _apiService.LoginAsync(request);

                if (!string.IsNullOrEmpty(result?.accessToken))
                {
                    IsLoading = false;
                    await SecureStorage.SetAsync("auth_token", result.accessToken);
                   await SecureStorage.SetAsync("storeId", result.id.ToString());

                    _userSession.SetStoreId(result.id);
                    // Navigate to AppShell after login
                    Application.Current.MainPage = new AppShell();
                }
                else
                {
                    await _dialogService.ShowAlertAsync("Login Failed", "Username or password is incorrect!", "OK");
                    IsLoading = false;
                }
            }

        }
    }
}
