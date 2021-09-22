using WhatXamarinUI.ViewModels;
using Xamarin.Forms;

namespace WhatXamarinUI.Views
{
    //[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ThemeDetailPage : ContentPage
    {
        public ThemeDetailPage()
        {
            InitializeComponent();
            BindingContext = new ThemeDetailViewModel();
        }
    }
}