using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace StackNavigation2
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            //NavigationPage.SetBackButtonTitle(this)
        }

        PrivacyPage priv;
        UnitsPage units = new UnitsPage();
        AboutPage about;
        LicensesPage lic;


        async void PrivacyClicked(System.Object sender, System.EventArgs e)
        {
            priv = new PrivacyPage();
            await Navigation.PushAsync(priv, true);
        }

        async void UnitsClicked(System.Object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(units, true);
        }

        async void AboutClicked(System.Object sender, System.EventArgs e)
        {
            about = new AboutPage();
            await Navigation.PushAsync(about, true);
        }

        async void LicensesClicked(System.Object sender, System.EventArgs e)
        {
            lic = new LicensesPage();
            await Navigation.PushModalAsync(lic, true);
        }
    }
}

