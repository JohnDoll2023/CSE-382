using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Essentials;

namespace Passwords {
	public partial class MainPage : ContentPage {
		public MainPage() {
			InitializeComponent();
		}
		private async void UpdateLabels() {
			try {
				string uname = await SecureStorage.GetAsync("username");
				string pw = await SecureStorage.GetAsync("password");
				ssUsername.Text = uname;
				ssPassword.Text = pw;
				pwPlainText.Text = password.Text;
			}
			catch (Exception e) {
				await DisplayAlert("Secure Storage", e.ToString(), "Cancel");
			}
		}
		protected override void OnAppearing() {
			base.OnAppearing();
			UpdateLabels();
		}
		private async void LoginClicked(object sender, EventArgs e) {
			try {
				string pw = password.Text;
				string uname = username.Text;
				string ssuname = await SecureStorage.GetAsync("username");
				string sspw = await SecureStorage.GetAsync("password");
				if (ssuname == uname) {
					if (sspw != pw)
						await SecureStorage.SetAsync("password", pw);
				} else {
					await SecureStorage.SetAsync("username", uname);
					await SecureStorage.SetAsync("password", pw);
				}
				UpdateLabels();
				await DisplayAlert("Logging on", uname + " " + pw, "OK");
			}
			catch (Exception) { // Possible that device doesn't support secure storage on device.
				await DisplayAlert("Secure Storage", e.ToString(), "Cancel");
			}
		}
		private void PWTextChanged(object sender, TextChangedEventArgs e) {
			UpdateLabels();
		}
		private async void UsernameUnfocused(object sender, FocusEventArgs e) {
			try {
				string uname = await SecureStorage.GetAsync("username");
				if (uname == username.Text) {
					string pw = await SecureStorage.GetAsync("password");
					password.Text = pw;
					UpdateLabels();
				} else {
					password.Text = "";
					UpdateLabels();
				}
			}
			catch (Exception) {// Possible that device doesn't support secure storage on device.
				await DisplayAlert("Secure Storage", e.ToString(), "Cancel");
			}
		}
	}
}
