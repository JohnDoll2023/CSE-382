using System;
using System.Collections.Generic;
using System.Reflection;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace RunningApp
{
    public partial class SettingsPage : ContentPage
    {
        public static bool metric;
        public SettingsPage()
        {
            InitializeComponent();
            if (!Preferences.ContainsKey("miles"))
            {
                Preferences.Set("miles", false);
            }
            measurementSwitch.IsToggled = Preferences.Get("miles", false);
            if (!Preferences.ContainsKey("dob"))
            {
                Preferences.Set("dob", new DateTime(2000, 1, 1));
            }
            dob.Date = Preferences.Get("dob", new DateTime(2000, 1, 1));
            if (!Preferences.ContainsKey("gender"))
            {
                Preferences.Set("gender", "Male");
            }
            gender.SelectedItem = Preferences.Get("gender", "Male");
        }

        void Switch_Toggled(System.Object sender, Xamarin.Forms.ToggledEventArgs e)
        {
            Preferences.Set("miles", measurementSwitch.IsToggled);
            metric = measurementSwitch.IsToggled;
            if (metric)
            {
                measurementLabel.Text = "Metric";
            }
            else
            {
                measurementLabel.Text = "Miles";
            }
        }

        async void Button_Clicked(System.Object sender, System.EventArgs e)
        {
            var uri = "https://miamioh.edu";
            await Browser.OpenAsync(uri, BrowserLaunchMode.SystemPreferred);
        }

        void dob_DateSelected(System.Object sender, Xamarin.Forms.DateChangedEventArgs e)
        {
            Preferences.Set("dob", dob.Date);
        }

        void gender_SelectedIndexChanged(System.Object sender, System.EventArgs e)
        {
            String s = gender.SelectedItem.ToString();
            Preferences.Set("gender", s);
        }
    }
}