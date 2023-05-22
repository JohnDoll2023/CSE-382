using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using WeatherData;

namespace WeatherApp
{
    public partial class MainPage : ContentPage
    {
        MonthPage mp;
        List<IEnumerable<WeatherInfo>> years = new List<IEnumerable<WeatherInfo>>();
        DataTemplate template;
        public MainPage()
        {
            InitializeComponent();
            
            Tuple<int, double>[] tempAvg = new Tuple<int, double>[7];
            for (int y = 2015; y <= 2021; y++)
            {
                double avg = 0;
                var year = DB.conn.Table<WeatherInfo>().AsEnumerable().Where(s => s.Date.Year == y).ToList();
                
                int days = y % 4 == 0 ? 366 : 365;
                for (int day = 0; day < days; day++)
                {
                    avg += year.ElementAt(day).Temperature;
                }
                double roundedTemp = Math.Round(avg / days, 2);
                tempAvg[y - 2015] = new Tuple<int, double> (y, roundedTemp);
                years.Add(year);
            }
            lv.ItemsSource = tempAvg;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            template = CreateTemplate();
            lv.ItemTemplate = template;
        }

        DataTemplate CreateTemplate()
        {
            DataTemplate r = new DataTemplate(() => {
                Label nameLabel = new Label();
                nameLabel.SetBinding(Label.TextProperty, "Item1");
                nameLabel.FontSize = 20;
                nameLabel.TextColor = Color.Blue;

                Label tempLabel = new Label();
                tempLabel.SetBinding(Label.TextProperty, "Item2");
                tempLabel.FontSize = 20;
                tempLabel.TextColor = Color.Blue;

                Grid grid = new Grid();
                grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) });
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(10, GridUnitType.Absolute) });
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(300, GridUnitType.Absolute) });
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(60, GridUnitType.Absolute) });
                grid.Children.Add(nameLabel, 1, 0);
                grid.Children.Add(tempLabel, 2, 0);
                return new ViewCell
                {
                    View = grid
                };
            });
            return r;
        }

        async void lv_ItemSelected(System.Object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {
            Tuple<int, double> yearSelection = (Tuple<int, double>)lv.SelectedItem;
            int year = yearSelection.Item1;
            IEnumerable<WeatherInfo> yearData = years.ElementAt(year - 2015);
            double temp = yearData.ElementAt(0).Temperature;
            mp = new MonthPage(yearData);
            await Navigation.PushAsync(mp, true);
        }
    }
}