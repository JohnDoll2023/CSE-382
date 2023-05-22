using Xamarin.Forms;
using Xamarin.Essentials;

namespace SportStat
{
    public partial class Profile : ContentPage
    {
        public Profile()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            if (!Preferences.ContainsKey("Name"))
            {
                initialSetup();
            }
            //if (!Preferences.ContainsKey("Age"))
            //{
            //    Preferences.Set("Age", "");
            //}
            //if (!Preferences.ContainsKey("Gender"))
            //{
            //    Preferences.Set("Gender", "");
            //}
            //if (!Preferences.ContainsKey("Home"))
            //{
            //    Preferences.Set("Home", "");
            //}
            //if (!Preferences.ContainsKey("Year"))
            //{
            //    Preferences.Set("Year", "");
            //}
            pageTitle.Text = Preferences.Get("Name", "");
            ageLabel.Text = Preferences.Get("Age", "");
            genderLabel.Text = Preferences.Get("Gender", "");
            fromLabel.Text = Preferences.Get("Home", "");
            yearLabel.Text = Preferences.Get("Year", "");
        }

        async void UpdateProfile(System.Object sender, System.EventArgs e)
        {
            var updatePage = new UpdateProfilePage();
            await Navigation.PushModalAsync(updatePage);
        }

        async void initialSetup()
        {
            var updatePage = new UpdateProfilePage();
            await Navigation.PushModalAsync(updatePage);
        }
    }
}