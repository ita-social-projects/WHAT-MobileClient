using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatXamarinUI.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WhatXamarinUI.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ThemesPage : ContentPage
    {
        ThemesViewModel _viewModel;

        public ThemesPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new ThemesViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}