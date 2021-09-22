using System;
using System.Collections.Generic;
using System.Text;
using WhatXamarinUI.HttpUtils;
using WhatXamarinUI.Models;
using WhatXamarinUI.Views;
using Xamarin.Forms;

namespace WhatXamarinUI.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private HttpUtil _httpUtil;
        private string _email;
        private string _password;

        public Command LoginCommand { get; }

        public LoginViewModel()
        {
            LoginCommand = new Command(OnLoginClicked);
            _httpUtil = HttpUtil.GetInstance();
        }

        public string Password
        {
            get => _password;
            set
            {
                _password = value;

                OnPropertyChanged();
            }
        }

        public string Email
        {
            get => _email;
            set
            {
                _email = value;

                OnPropertyChanged();
            }
        }

        private async void OnLoginClicked(object obj)
        {
            var postData = new AuthModel { Email = _email, Password = _password };
            
            if (await _httpUtil.Authorize(postData, ""))
            {
                await Shell.Current.GoToAsync($"//{nameof(AboutPage)}");
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Login", "Incorrect Credentinals", "Ok");
            }
            
        }
    }
}
