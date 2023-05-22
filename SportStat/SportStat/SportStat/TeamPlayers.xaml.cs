using System.Linq;
using Xamarin.Forms;
using SQLite;
using Xamarin.Essentials;

namespace SportStat
{
    public partial class TeamPlayers : ContentPage
    {
        SQLiteAsyncConnection conn;
        Team selectedTeam;
        DataTemplate template;
        bool isSoftball = false;

        public TeamPlayers(Team team)
        {
            InitializeComponent();
            selectedTeam = team;
            NavigationPage.SetBackButtonTitle(this, selectedTeam.Name);
            Title = selectedTeam.Name;
            playersLV.ItemTapped += PlayersLV_ItemTapped;
            this.BackgroundColor = Color.AliceBlue;
            playersLV.BackgroundColor = Color.AliceBlue;
        }

        PlayerPage pp;

        private async void PlayersLV_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            Player player = (Player)e.Item;
            pp = new PlayerPage(player);
            await Navigation.PushAsync(pp, true);
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            string libFolder = FileSystem.AppDataDirectory;
            string fname = System.IO.Path.Combine(libFolder, "sports.db");
            conn = new SQLiteAsyncConnection(fname);
            await conn.CreateTableAsync<Player>();
            Player[] playerArray = await conn.Table<Player>().ToArrayAsync();
            playersLV.ItemsSource = playerArray.Where(s => s.Team.Equals(selectedTeam.Name));
            template = CreateTemplate();
            playersLV.ItemTemplate = template;
            if (selectedTeam.Sport.Equals("Softball"))
            {
                isSoftball = true;
                goalsLabel.Text = "Runs";
                assistsLabel.Text = "RBI";
            }
        }

        DataTemplate CreateTemplate()
        {
            DataTemplate r = new DataTemplate(() =>
            {
                Label nameLabel = new Label();
                nameLabel.SetBinding(Label.TextProperty, "Name");
                nameLabel.FontSize = 20;
                nameLabel.VerticalOptions = LayoutOptions.CenterAndExpand;

                Label ageLabel = new Label();
                ageLabel.SetBinding(Label.TextProperty, "Age");
                ageLabel.FontSize = 20;
                ageLabel.VerticalOptions = LayoutOptions.CenterAndExpand;

                Label gamesLabel = new Label();
                gamesLabel.SetBinding(Label.TextProperty, "GamesPlayed");
                gamesLabel.FontSize = 20;
                gamesLabel.VerticalOptions = LayoutOptions.CenterAndExpand;

                Label stat1 = new Label();
                stat1.FontSize = 20;
                stat1.VerticalOptions = LayoutOptions.CenterAndExpand;
                Label stat2 = new Label();
                stat2.FontSize = 20;
                stat2.VerticalOptions = LayoutOptions.CenterAndExpand;

                if (!isSoftball)
                {
                    stat1.SetBinding(Label.TextProperty, "Goals");
                    stat2.SetBinding(Label.TextProperty, "Assists");  
                } else
                {
                    stat1.SetBinding(Label.TextProperty, "Runs");
                    stat2.SetBinding(Label.TextProperty, "RBI");
                }

                Grid grid = new Grid();
                grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) });
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(155, GridUnitType.Absolute) });
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(70, GridUnitType.Absolute) });
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(50, GridUnitType.Absolute) });
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(50, GridUnitType.Absolute) });
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(50, GridUnitType.Absolute) });
                grid.Children.Add(nameLabel, 0, 0);
                grid.Children.Add(ageLabel, 1, 0);
                grid.Children.Add(gamesLabel, 2, 0);
                grid.Children.Add(stat1, 3, 0);
                grid.Children.Add(stat2, 4, 0);
                grid.BackgroundColor = Color.Orange;

                return new ViewCell
                {
                    View = grid
                };
            });

            return r;
        }

        async void Button_Clicked(System.Object sender, System.EventArgs e)
        {
            Button button = (Button)sender;
            if (button.Text.ToString().Equals("Add Player"))
            {
                var addPlayer = new AddPlayerPage(selectedTeam);
                await Navigation.PushModalAsync(addPlayer);
                playersLV.ItemsSource = await conn.Table<Player>().ToListAsync();
            } else if (button.Text.ToString().Equals("Update Team"))
            {
                var updateTeamPage = new UpdateTeamPage(selectedTeam);
                await Navigation.PushModalAsync(updateTeamPage);
            } else
            {
                string result = await DisplayActionSheet("Are you sure you want to remove this team?", "Yes", "Cancel");
                if (result.Equals("Yes"))
                {
                    await conn.DeleteAsync(selectedTeam);
                    await Navigation.PopAsync();
                }
            }
        }
    }
}