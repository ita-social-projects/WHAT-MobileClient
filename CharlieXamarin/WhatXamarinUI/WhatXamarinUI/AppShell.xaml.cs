using System;
using System.Collections.Generic;
using WhatXamarinUI.HttpUtils;
using WhatXamarinUI.ViewModels;
using WhatXamarinUI.Views;
using Xamarin.Forms;

namespace WhatXamarinUI
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ThemesPage), typeof(ThemesPage));
            Routing.RegisterRoute(nameof(ThemeDetailPage), typeof(ThemeDetailPage));
            Routing.RegisterRoute(nameof(NewThemePage), typeof(NewThemePage));
        }

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            var httpContext = HttpUtil.GetInstance();

            httpContext.Logout();

            await Shell.Current.GoToAsync("//LoginPage");
        }
    }
}
