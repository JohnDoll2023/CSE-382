using Xamarin.Forms;
using SQLite;
using Xamarin.Essentials;

namespace SportStat
{
    public partial class Teams : ContentPage
    {
        SQLiteAsyncConnection conn;
        DataTemplate template;
        public Teams()
        {
            InitializeComponent();
            teamsLV.ItemTapped += TeamsLV_ItemTapped;
        }

        TeamPlayers tp;

        async void TeamsLV_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            Team team = (Team)e.Item;
            tp = new TeamPlayers(team);
            await Navigation.PushAsync(tp, true);
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            string libFolder = FileSystem.AppDataDirectory;
            string fname = System.IO.Path.Combine(libFolder, "sports.db");
            conn = new SQLiteAsyncConnection(fname);
            await conn.CreateTableAsync<Team>();
            teamsLV.ItemsSource = await conn.Table<Team>().ToListAsync();
            template = CreateTemplate();
            teamsLV.ItemTemplate = template;
            this.BackgroundColor = Color.AliceBlue;
            teamsLV.BackgroundColor = Color.AliceBlue;
        }

        DataTemplate CreateTemplate()
        {
            DataTemplate r = new DataTemplate(() =>
            {
                
                Label teamNameLabel = new Label();
                teamNameLabel.SetBinding(Label.TextProperty, "Name");
                teamNameLabel.FontSize = 20;
                teamNameLabel.VerticalOptions = LayoutOptions.CenterAndExpand;

                Image imageView = new Image();
                imageView.SetBinding(Image.SourceProperty, "Image");
                imageView.HeightRequest = 40;

                Label teamRecordLabel = new Label();
                teamRecordLabel.SetBinding(Label.TextProperty, "Record");
                teamRecordLabel.FontSize = 20;
                teamRecordLabel.VerticalOptions = LayoutOptions.CenterAndExpand;

                Grid grid = new Grid();
                grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) });
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(50, GridUnitType.Absolute) });
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(280, GridUnitType.Absolute) });
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(60, GridUnitType.Absolute) });
                grid.Children.Add(teamNameLabel, 1, 0);
                grid.Children.Add(imageView, 0, 0);
                grid.Children.Add(teamRecordLabel, 2, 0);
                grid.BackgroundColor = Color.Orange;

                return new ViewCell
                {
                    View = grid
                };
            });

            return r;
        }

        async void AddTeam(System.Object sender, System.EventArgs e)
        {
            var addTeam = new AddTeamPage();
            await Navigation.PushModalAsync(addTeam);
            teamsLV.ItemsSource = await conn.Table<Team>().ToListAsync();
        }
    }
}