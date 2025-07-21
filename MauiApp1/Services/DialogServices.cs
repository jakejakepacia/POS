using MauiApp1.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.Services
{
    public class DialogService : IDialogService
    {
        private Page _currentPage => Application.Current.MainPage?.Navigation?.NavigationStack.LastOrDefault();

        public async Task ShowAlertAsync(string title, string message, string cancel)
        {
            if (_currentPage != null)
                await _currentPage.DisplayAlert(title, message, cancel);
        }

        public async Task<bool> ShowConfirmationAsync(string title, string message, string accept, string cancel)
        {
            if (_currentPage != null)
                return await _currentPage.DisplayAlert(title, message, accept, cancel);

            return false;
        }
    }

}
