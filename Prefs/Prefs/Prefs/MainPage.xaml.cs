using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Essentials;

namespace Prefs {
	// Learn more about making custom code visible in the Xamarin.Forms previewer
	// by visiting https://aka.ms/xamarinforms-previewer
	[DesignTimeVisible(false)]
	public partial class MainPage : ContentPage {
		public MainPage() {
			InitializeComponent();
			if (!Preferences.ContainsKey("MetricMode"))
			{
				Preferences.Set("MetricMode", false);
			}
			metricSwitch.IsToggled = Preferences.Get("MetricMode", false);
			if (!Preferences.ContainsKey("Date"))
            {
				Preferences.Set("Date", new DateTime(2000, 1, 1));
            }
			dp.Date = Preferences.Get("Date", new DateTime(2000, 1, 1));
			if (!Preferences.ContainsKey("slider"))
            {
				Preferences.Set("slider", 0.5);
            }
			slider.Value = Preferences.Get("slider", 0.5);
		}
		private void MetricSwitchToggled(object sender, ToggledEventArgs e) {
			Preferences.Set("MetricMode", metricSwitch.IsToggled);
		}

		private void dp_DateSelected(System.Object sender, Xamarin.Forms.DateChangedEventArgs e)
        {
			Preferences.Set("Date", dp.Date);
        }

        void slider_ValueChanged(System.Object sender, Xamarin.Forms.ValueChangedEventArgs e)
        {
			Preferences.Set("slider", slider.Value);
        }
    }
}