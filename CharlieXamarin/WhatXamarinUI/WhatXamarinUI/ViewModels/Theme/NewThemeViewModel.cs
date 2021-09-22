using System;
using System.Collections.Generic;
using System.Text;
using WhatXamarinUI.HttpUtils;
using WhatXamarinUI.Models;
using Xamarin.Forms;

namespace WhatXamarinUI.ViewModels
{
    public class NewThemeViewModel : BaseViewModel
    {
        private HttpUtil _httpUtil;
        private string _name;

        public Command CancelCommand { get; }

        public Command SaveCommand { get; }

        public string Name
        {
            get => _name;
            set 
            { 
                _name = value;
                OnPropertyChanged();
            }
        }

        public NewThemeViewModel()
        {
            Title = "Add new theme";
            CancelCommand = new Command(OnCancel);
            SaveCommand = new Command(ExecuteSave);
            _httpUtil = HttpUtil.GetInstance();
        }

        private async void OnCancel()
        {
            await Shell.Current.GoToAsync("..");
        }

        private async void ExecuteSave()
        {
            var url = "https://whatbackendapihosting.azurewebsites.net/api/themes";

            var response = await _httpUtil.PostJsonAsync(url, new Theme { Name = Name });

            if (_httpUtil.IsSuccessfulStatusCode(response))
            {
                await Application.Current.MainPage.DisplayAlert("PostTheme", "Theme successfully added", "Ok");
                await Shell.Current.GoToAsync("..");
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("PostTheme", "Something went wrong", "Ok");
            }
        }

    }
}
