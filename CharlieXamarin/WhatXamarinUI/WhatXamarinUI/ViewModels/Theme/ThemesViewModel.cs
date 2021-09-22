using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WhatXamarinUI.HttpUtils;
using WhatXamarinUI.Models;
using WhatXamarinUI.Views;
using Xamarin.Forms;

namespace WhatXamarinUI.ViewModels
{
    public class ThemesViewModel : BaseViewModel
    {
        private Theme _selectedTheme;
        private HttpUtil _httpUtil;

        public ObservableCollection<Theme> Themes { get; }
        public Command LoadThemesCommand { get; }
        public Command AddThemeCommand { get; }
        public Command<Theme> ThemeTapped { get; }

        public ThemesViewModel()
        {
            Title = "Themes";
            Themes = new ObservableCollection<Theme>();
            LoadThemesCommand = new Command(async () => await ExecuteLoadItemsCommand());

            _httpUtil = HttpUtil.GetInstance();

            ThemeTapped = new Command<Theme>(OnThemeSelected);

            AddThemeCommand = new Command(OnAddTheme);
        }

        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                Themes.Clear();
                var items = await GetThemes();
                foreach (var item in items)
                {
                    Themes.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public void OnAppearing()
        {
            IsBusy = true;
            SelectedTheme = null;
        }

        public Theme SelectedTheme
        {
            get => _selectedTheme;
            set
            {
                _selectedTheme = value;
                OnThemeSelected(value);
            }
        }

        private async void OnAddTheme(object obj)
        {
            await Shell.Current.GoToAsync(nameof(NewThemePage));
        }

        async void OnThemeSelected(Theme theme)
        {
            if (theme == null)
                return;

            await Shell.Current.GoToAsync($"{nameof(ThemeDetailPage)}?{nameof(ThemeDetailViewModel.ItemId)}={theme.Id}");
        }

        private async Task<IEnumerable<Theme>> GetThemes()
        {
            var response = await _httpUtil.GetAsync("https://whatbackendapihosting.azurewebsites.net/api/themes");

            if (!_httpUtil.IsSuccessfulStatusCode(response))
            {
                await Application.Current.MainPage.DisplayAlert("GetThemes", response.StatusCode.ToString(), "Ok");

                return null;
            }

            return  JsonConvert.DeserializeObject<List<Theme>>(response.Content.ReadAsStringAsync().Result).OrderByDescending(x => x.Id);
        }
    }
}