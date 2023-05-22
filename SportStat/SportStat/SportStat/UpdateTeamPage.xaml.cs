using System;
using Xamarin.Forms;
using SQLite;
using Plugin.SimpleAudioPlayer;
using Xamarin.Essentials;
using System.IO;
using System.Reflection;

namespace SportStat
{
    public partial class UpdateTeamPage : ContentPage
    {
        Team selectedTeam;
        SQLiteAsyncConnection conn;
        ISimpleAudioPlayer player = CrossSimpleAudioPlayer.CreateSimpleAudioPlayer();
        public UpdateTeamPage(Team team)
        {
            InitializeComponent();
            selectedTeam = team;
        }

        private void Load(string file)
        {
            var assembly = typeof(App).GetTypeInfo().Assembly;
            String ns = "SportStat";
            Stream audioStream = assembly.GetManifestResourceStream(ns + "." + file);
            player.Load(audioStream);
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            string libFolder = FileSystem.AppDataDirectory;
            string fname = System.IO.Path.Combine(libFolder, "sports.db");
            conn = new SQLiteAsyncConnection(fname);
            await conn.CreateTableAsync<Team>();
            teamName.Text = selectedTeam.Name;
            teamWins.Text = selectedTeam.Wins.ToString();
            teamLosses.Text = selectedTeam.Losses.ToString();
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
                if (string.IsNullOrEmpty(teamName.Text) || string.IsNullOrEmpty(teamWins.Text) || string.IsNullOrEmpty(teamLosses.Text))
                {
                    await DisplayAlert("Empty fields", "You must fill out all the fields", "Ok");
                }
                else
                {
                    Team oldTeam = selectedTeam;
                    string name = teamName.Text.ToString();
                    int wins = (int)Double.Parse(teamWins.Text.ToString());
                    int losses = (int)Double.Parse(teamLosses.Text.ToString());

                    Team newTeam = new Team
                    {
                        Name = name,
                        Wins = wins,
                        Losses = losses,
                        Sport = selectedTeam.Sport,
                        Image = selectedTeam.Image
                    };

                    if (wins - losses < oldTeam.Wins - oldTeam.Losses)
                    {
                        Load("boo.mp3");
                        player.Play();
                    } else
                    {
                        Load("cheer.mp3");
                        player.Play();
                    }

                    newTeam.Id = oldTeam.Id;
                    await conn.UpdateAsync(newTeam);
                    await Navigation.PopModalAsync();
                }
            }
        }
    }
}