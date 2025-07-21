using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Java.Lang.Annotation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MauiApp1.ViewModel
{
    public partial class LoginViewModel : ObservableObject
    {

        [RelayCommand]
        private async Task LoginAsync()
        {
            // Replace MainPage with AppShell after login
            Application.Current.MainPage = new AppShell();
        }
    }
}
