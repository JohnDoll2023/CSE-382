using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using SQLite;
using Xamarin.Essentials;
using System.Linq.Expressions;

using Xamarin.Forms;

namespace SportStat
{
    public partial class AddTeamPage : ContentPage
    {
        SQLiteAsyncConnection conn;
        public AddTeamPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            string libFolder = FileSystem.AppDataDirectory;
            string fname = System.IO.Path.Combine(libFolder, "sports.db");
            conn = new SQLiteAsyncConnection(fname);
            await conn.CreateTableAsync<Team>();
            sport.SelectedIndex = 0;
        }

        async void Button_Clicked(System.Object sender, System.EventArgs e)
        {
            Button button = (Button)sender;
            
            if (button.Text.ToString().Equals("Cancel"))
            {
                await Navigation.PopModalAsync();
            } else
            {
                if (string.IsNullOrEmpty(name.Text) || string.IsNullOrEmpty(wins.Text) || string.IsNullOrEmpty(losses.Text))
                {
                    await DisplayAlert("Empty fields", "You must fill out all the fields", "Ok");
                }
                else
                {
                    string teamName = name.Text.ToString();
                    int teamWins = (int)Double.Parse(wins.Text.ToString());
                    int teamLosses = (int)Double.Parse(losses.Text.ToString());
                    string teamSport = sport.SelectedItem.ToString();
                    string sportImage = "broomball.jpeg";
                    if (teamSport.Equals("Softball"))
                    {
                        sportImage = "softball.jpeg";
                    }
                    else if (teamSport.Equals("Soccer"))
                    {
                        sportImage = "soccer.jpg";
                    }

                    Team team = new Team
                    {
                        Name = teamName,
                        Wins = teamWins,
                        Losses = teamLosses,
                        Sport = teamSport,
                        Image = sportImage
                    };

                    await conn.InsertAsync(team);
                    await Navigation.PopModalAsync();
                }
            }
        }
    }
}