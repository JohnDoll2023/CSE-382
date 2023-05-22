using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using Xamarin.Forms;

namespace timezonedb
{
    public partial class MainPage : ContentPage
    {
        RestService _restService;

        public MainPage()
        {
            InitializeComponent();
            _restService = new RestService();
        }

        async void go_Clicked(System.Object sender, System.EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(cityName.Text))
            {
                string uriRequest = GenerateRequestUri(Constants.TimeZoneEndpoint, cityName.Text);
                TimeZoneData tz = await _restService.GetTZData(uriRequest);
                location.Text = tz.zone;
                letters.Text = tz.abv;
                numbers.Text = tz.gmt;
            }
        }

        string GenerateRequestUri(string endpoint, string city)
        {
            string requestUri = endpoint;
            requestUri += $"?key={Constants.TimeZoneAPIKey}";
            requestUri += $"&format=json&&by=zone&zone=America/";
            requestUri += city;
            return requestUri;
        }
    }
}