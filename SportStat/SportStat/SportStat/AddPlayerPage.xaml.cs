using System;
using Xamarin.Forms;
using SQLite;
using Xamarin.Essentials;

namespace SportStat
{
    public partial class AddPlayerPage : ContentPage
    {
        SQLiteAsyncConnection conn;
        Team selectedTeam;
        bool isSoftball = false;
        public AddPlayerPage(Team team)
        {
            InitializeComponent();
            selectedTeam = team;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            string libFolder = FileSystem.AppDataDirectory;
            string fname = System.IO.Path.Combine(libFolder, "sports.db");
            conn = new SQLiteAsyncConnection(fname);
            await conn.CreateTableAsync<Player>();
            if (selectedTeam.Sport.Equals("Softball"))
            {
                isSoftball = true;
            }
            goalsLabel.IsVisible = !isSoftball;
            assistsLabel.IsVisible = !isSoftball;
            runsLabel.IsVisible = isSoftball;
            rbiLabel.IsVisible = isSoftball;
            goals.IsVisible = !isSoftball;
            assists.IsVisible = !isSoftball;
            runs.IsVisible = isSoftball;
            rbi.IsVisible = isSoftball;
            genderPicker.SelectedIndex = 0;
            classPicker.SelectedIndex = 0;
        }

        async void AddPlayer(System.Object sender, System.EventArgs e)
        {
            Button button = (Button)sender;
            if (button.Text.ToString().Equals("Cancel"))
            {
                await Navigation.PopModalAsync();
            } else
            {
                int runs = 0;
                int rbi = 0;
                int goals = 0;
                int assists = 0;
                bool emptyField = false;
                if (isSoftball)
                {
                    if (string.IsNullOrEmpty(runsLabel.Text) || string.IsNullOrEmpty(rbiLabel.Text) || string.IsNullOrEmpty(nameLabel.Text) ||
                        string.IsNullOrEmpty(gamesPlayedLabel.Text) || string.IsNullOrEmpty(ageLabel.Text))
                    {
                        emptyField = true;
                    }
                    else
                    {
                        runs = (int)Double.Parse(runsLabel.Text.ToString());
                        rbi = (int)Double.Parse(rbiLabel.Text.ToString());
                        goals = 0;
                        assists = 0;
                    }
                } else
                {
                    if (string.IsNullOrEmpty(goalsLabel.Text) || string.IsNullOrEmpty(assistsLabel.Text) || string.IsNullOrEmpty(nameLabel.Text) ||
                        string.IsNullOrEmpty(gamesPlayedLabel.Text) || string.IsNullOrEmpty(ageLabel.Text))
                    {
                        emptyField = true;

                    }
                    else
                    {
                        goals = (int)Double.Parse(goalsLabel.Text.ToString());
                        assists = (int)Double.Parse(assistsLabel.Text.ToString());
                        runs = 0;
                        rbi = 0;
                    }
                }

                if (emptyField)
                {
                    await DisplayAlert("Empty fields", "You must fill out all the fields", "Ok");
                }
                else
                {

                    string name = nameLabel.Text.ToString();
                    int gamesPlayed = (int)Double.Parse(gamesPlayedLabel.Text.ToString());
                    string gender = genderPicker.SelectedItem.ToString();
                    string classStanding = classPicker.SelectedItem.ToString();
                    int age = (int)Double.Parse(ageLabel.Text.ToString());

                    Player player = new Player
                    {
                        Name = name,
                        Goals = goals,
                        Assists = assists,
                        Runs = runs,
                        RBI = rbi,
                        GamesPlayed = gamesPlayed,
                        Age = age,
                        Gender = gender,
                        ClassStanding = classStanding,
                        Team = selectedTeam.Name,
                        Sport = selectedTeam.Sport
                    };

                    await conn.InsertAsync(player);
                    await Navigation.PopModalAsync();
                }
            }
        }
    }
}