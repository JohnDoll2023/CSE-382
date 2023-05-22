using System;using Xamarin.Forms;
using SQLite;
using Xamarin.Essentials;

namespace SportStat
{
    public partial class UpdatePlayerPage : ContentPage
    {
        Player selectedPlayer;
        SQLiteAsyncConnection conn;
        bool isSoftball = false;
        public UpdatePlayerPage(Player player)
        {
            InitializeComponent();
            selectedPlayer = player;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            string libFolder = FileSystem.AppDataDirectory;
            string fname = System.IO.Path.Combine(libFolder, "sports.db");
            conn = new SQLiteAsyncConnection(fname);
            await conn.CreateTableAsync<Player>();
            nameLabel.Text = selectedPlayer.Name.ToString();
            ageLabel.Text = selectedPlayer.Age.ToString();
            goalLabel.Text = selectedPlayer.Goals.ToString();
            assistsLabel.Text = selectedPlayer.Assists.ToString();
            genderPicker.SelectedItem = selectedPlayer.Gender;
            gpLabel.Text = selectedPlayer.GamesPlayed.ToString();
            classPicker.SelectedItem = selectedPlayer.ClassStanding;
            if (selectedPlayer.Sport.Equals("Softball")) {
                isSoftball = true;
            }
            goalLabel.IsVisible = !isSoftball;
            assistsLabel.IsVisible = !isSoftball;
            runsLabel.IsVisible = isSoftball;
            rbiLabel.IsVisible = isSoftball;
            goals.IsVisible = !isSoftball;
            assists.IsVisible = !isSoftball;
            runs.IsVisible = isSoftball;
            rbi.IsVisible = isSoftball;
        }

        async void Button_Clicked(System.Object sender, System.EventArgs e)
        {
            Button button = (Button)sender;
            if (button.Text.ToString().Equals("Cancel"))
            {
                await Navigation.PopModalAsync();
            } else
            {
                Player oldPlayer = selectedPlayer;

                int runs = 0;
                int rbi = 0;
                int goals = 0;
                int assists = 0;
                bool emptyField = false;
                if (isSoftball)
                {
                    if (string.IsNullOrEmpty(runsLabel.Text) || string.IsNullOrEmpty(rbiLabel.Text) || string.IsNullOrEmpty(nameLabel.Text) ||
                        string.IsNullOrEmpty(gpLabel.Text) || string.IsNullOrEmpty(ageLabel.Text))
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
                }
                else
                {
                    if (string.IsNullOrEmpty(goalLabel.Text) || string.IsNullOrEmpty(assistsLabel.Text) || string.IsNullOrEmpty(nameLabel.Text) ||
                        string.IsNullOrEmpty(gpLabel.Text) || string.IsNullOrEmpty(ageLabel.Text))
                    {
                        emptyField = true;
                    }
                    else
                    {
                        goals = (int)Double.Parse(goalLabel.Text.ToString());
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
                    int gamesPlayed = (int)Double.Parse(gpLabel.Text.ToString());
                    string gender = genderPicker.SelectedItem.ToString();
                    string classStanding = classPicker.SelectedItem.ToString();
                    int age = (int)Double.Parse(ageLabel.Text.ToString());

                    Player newPlayer = new Player
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
                        Team = selectedPlayer.Team,
                        Sport = selectedPlayer.Sport
                    };

                    newPlayer.Id = oldPlayer.Id;

                    await conn.UpdateAsync(newPlayer);
                    await Navigation.PopModalAsync(true);
                }
            }
        }
    }
}