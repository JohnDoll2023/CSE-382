using Xamarin.Forms;
using SQLite;
using Xamarin.Essentials;

namespace SportStat
{
    public partial class PlayerPage : ContentPage
    {
        SQLiteAsyncConnection conn;
        Player selectedPlayer;
        static bool isSoftball = false;

        public PlayerPage(Player player)
        {
            InitializeComponent();
            selectedPlayer = player;
            Title = selectedPlayer.Name;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            string libFolder = FileSystem.AppDataDirectory;
            string fname = System.IO.Path.Combine(libFolder, "sports.db");
            conn = new SQLiteAsyncConnection(fname);
            await conn.CreateTableAsync<Player>();
            
            ageLabel.Text = selectedPlayer.Age.ToString();
            genderLabel.Text = selectedPlayer.Gender;
            gpLabel.Text = selectedPlayer.GamesPlayed.ToString();
            csLabel.Text = selectedPlayer.ClassStanding;
            goalLabel.Text = selectedPlayer.Goals.ToString();
            assistsLabel.Text = selectedPlayer.Assists.ToString();
            runsLabel.Text = selectedPlayer.Runs.ToString();
            rbiLabel.Text = selectedPlayer.RBI.ToString();
            if (selectedPlayer.Sport.Equals("Softball"))
            {
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
            if (button.Text.ToString().Equals("Update Player"))
            {
                UpdatePlayerPage upp = new UpdatePlayerPage(selectedPlayer);
                await Navigation.PushModalAsync(upp);
                await Navigation.PopAsync();

            } else
            {
                string result = await DisplayActionSheet("Are you sure you want to remove this player?", "Yes", "Cancel");
                if (result.Equals("Yes"))
                {
                    await conn.DeleteAsync(selectedPlayer);
                    await Navigation.PopAsync();
                }
            }
        }
    }
}