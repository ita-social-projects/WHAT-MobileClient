using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatXamarinUI.Models;
using WhatXamarinUI.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WhatXamarinUI.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewThemePage : ContentPage
    {
        public Theme Theme {get;set;}

        public NewThemePage()
        {
            InitializeComponent();
            BindingContext = new NewThemeViewModel();
        }
    }
}