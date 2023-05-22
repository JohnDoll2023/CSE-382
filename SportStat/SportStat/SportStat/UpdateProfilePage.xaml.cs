using System;
using Xamarin.Forms;
using SQLite;
using Xamarin.Essentials;

namespace SportStat
{
    public partial class UpdateProfilePage : ContentPage
    {
        SQLiteAsyncConnection conn;
        public UpdateProfilePage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            name.Text = Preferences.Get("Name", "");
            age.Text = Preferences.Get("Age", "");
            string gender = Preferences.Get("Gender", "Male");
            genderPicker.SelectedItem = gender;
            if (string.IsNullOrEmpty(gender))
            {
                genderPicker.SelectedItem = "Male";
            }
            
            home.Text = Preferences.Get("Home", "");
            string classStanding = Preferences.Get("Year", "Freshman");
            classPicker.SelectedItem = classStanding;
            if (string.IsNullOrEmpty(classStanding))
            {
                classPicker.SelectedItem = "Freshman";
            }
        }

        async void Button_Clicked(System.Object sender, System.EventArgs e)
        {
            Button button = (Button)sender;
            if (button.Text.ToString().Equals("Cancel"))
            {
                await Navigation.PopModalAsync();
            }
            else
            {
                if (string.IsNullOrEmpty(name.Text) || string.IsNullOrEmpty(age.Text) || string.IsNullOrEmpty(home.Text))
                {
                    await DisplayAlert("Empty fields", "You must fill out all the fields", "Ok");
                }
                else
                {
                    string profileName = name.Text.ToString();
                    int profileAge = (int)Double.Parse(age.Text.ToString());
                    string profileGender = genderPicker.SelectedItem.ToString();
                    string profileHome = home.Text.ToString();
                    string profileYear = classPicker.SelectedItem.ToString();

                    Preferences.Set("Name", profileName);
                    Preferences.Set("Age", profileAge);
                    Preferences.Set("Gender", profileGender);
                    Preferences.Set("Home", profileHome);
                    Preferences.Set("Year", profileYear);

                    await Navigation.PopModalAsync();
                }
            }
        }
    }
}