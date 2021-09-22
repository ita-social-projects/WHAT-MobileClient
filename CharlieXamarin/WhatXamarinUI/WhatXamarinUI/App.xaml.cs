using System;
using WhatXamarinUI.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WhatXamarinUI
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
            Shell.Current.GoToAsync("//LoginPage");
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
