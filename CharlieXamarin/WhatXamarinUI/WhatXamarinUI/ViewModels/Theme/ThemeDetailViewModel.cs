using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WhatXamarinUI.HttpUtils;
using WhatXamarinUI.Models;
using Xamarin.Forms;
using System.Linq;

namespace WhatXamarinUI.ViewModels
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public class ThemeDetailViewModel : BaseViewModel
    {
        private HttpUtil _httpUtil;
        private string _itemId;
        private string _name;
        private bool _isEditable = false;

        public bool IsEditable
        {
            get
            {
                return _isEditable;
            }
            set
            {
                _isEditable = value;
                OnPropertyChanged();
            }
        }

        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }

        public string ItemId
        {
            get
            {
                return _itemId;
            }
            set
            {
                if (_itemId != value)
                {
                    LoadThemeId(value);
                }
                _itemId = value;
                OnPropertyChanged();
            }
        }

        public Command ChangeEditability { get; }

        public Command EditTheme { get; }

        public Command DeleteTheme { get; }

        public ThemeDetailViewModel()
        {
            Title = "Themes";
            _httpUtil = HttpUtil.GetInstance();

            ChangeEditability = new Command(() => ExecuteChangeEditability());
            EditTheme = new Command(async () => await ExecuteEditThemeAsync(ItemId));
            DeleteTheme = new Command(async () => await ExecuteDeleteThemeAsync(ItemId));
        }

        private void ExecuteChangeEditability()
        {
            IsEditable = !IsEditable;
        }

        private async Task ExecuteEditThemeAsync(string ItemId)
        {
            var url = string.Format("https://whatbackendapihosting.azurewebsites.net/api/themes/{0}", ItemId);

            var response = await _httpUtil.PutJsonAsync(url, new Theme { Name = Name});

            if (_httpUtil.IsSuccessfulStatusCode(response))
            {
                Name = JsonConvert.DeserializeObject<Theme>(response.Content.ReadAsStringAsync().Result).Name;
                await Application.Current.MainPage.DisplayAlert("GetTheme", "Successfully changed", "Ok");
                return;
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("GetTheme", "Something went wrong changed", "Ok");
            }
        }

        private async Task ExecuteDeleteThemeAsync(string ItemId)
        {
            var url = string.Format("https://whatbackendapihosting.azurewebsites.net/api/themes/{0}", ItemId);

            var response = await _httpUtil.DeleteAsync(url);

            if (_httpUtil.IsSuccessfulStatusCode(response))
            {
                await Application.Current.MainPage.DisplayAlert("GetTheme", "Successfully Deleted", "Ok");
                await Shell.Current.GoToAsync("..");
                return;
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("GetTheme", "Something went wrong changed", "Ok");
            }
        }

        public async void LoadThemeId(string itemId)
        {
            var theme = await GetThemeById(Convert.ToInt64(itemId));

            ItemId = theme.Id.ToString();
            Name = theme.Name;
        }

        private async Task<Theme> GetThemeById(long id)
        {
            var response = await _httpUtil.GetAsync("https://whatbackendapihosting.azurewebsites.net/api/themes");

            if (!_httpUtil.IsSuccessfulStatusCode(response))
            {
                await Application.Current.MainPage.DisplayAlert("GetTheme", response.StatusCode.ToString(), "Ok");

                return null;
            }

            var resultThemeCollection = JsonConvert.DeserializeObject<List<Theme>>(response.Content.ReadAsStringAsync().Result);

            var resultTheme = resultThemeCollection.FirstOrDefault(x => x.Id == id);

            return resultTheme;
        }
    }
}
