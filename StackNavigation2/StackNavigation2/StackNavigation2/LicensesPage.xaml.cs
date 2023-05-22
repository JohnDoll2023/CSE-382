using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace StackNavigation2
{
    public partial class LicensesPage : ContentPage
    {
        public LicensesPage()
        {
            InitializeComponent();
        }

        async void Button_Clicked(System.Object sender, System.EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}

