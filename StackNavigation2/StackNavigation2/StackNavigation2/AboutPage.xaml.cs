using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace StackNavigation2
{
    public partial class AboutPage : ContentPage
    {
        public AboutPage()
        {
            InitializeComponent();
        }

        XamarinPage xam;
        JavaPage java;

        async void XamarinClicked(System.Object sender, System.EventArgs e)
        {
            xam = new XamarinPage();
            await Navigation.PushAsync(xam, true);
        }

        async void JavaClicked(System.Object sender, System.EventArgs e)
        {
            java = new JavaPage();
            await Navigation.PushAsync(java, true);

        }
    }
}

