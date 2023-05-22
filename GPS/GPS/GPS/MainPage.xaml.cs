using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Essentials;

namespace GPS {
	[DesignTimeVisible(false)]
	public partial class MainPage : ContentPage {
		public MainPage() {
			InitializeComponent();
		}
		private async void GPSClicked(object sender, EventArgs e) {
			try {
				GeolocationRequest request = new GeolocationRequest(GeolocationAccuracy.Lowest);
				var task = await Geolocation.GetLocationAsync(request);
				Location location = task;
				if (location != null)
					gpsLabel.Text = ($"Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}");
				else
					gpsLabel.Text = "No response";
			}
			catch (FeatureNotSupportedException fnsEx) {
				gpsLabel.Text = fnsEx.ToString();
			}
			catch (FeatureNotEnabledException fneEx) {
				gpsLabel.Text = fneEx.ToString();
			}
			catch (PermissionException pEx) {
				gpsLabel.Text = pEx.ToString();
			}
			catch (Exception ex) {
				gpsLabel.Text = ex.ToString();
			}
		}
	}
}
