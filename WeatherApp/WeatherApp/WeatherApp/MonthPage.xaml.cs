using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using WeatherData;

namespace WeatherApp
{
    public partial class MonthPage : ContentPage
    {
        DataTemplate template;
        public MonthPage(IEnumerable<WeatherInfo> yearData)
        {
            InitializeComponent();
            int year = yearData.ElementAt(0).Date.Year;
            int days = year % 4 == 0 ? 366 : 365;
            Month[] months = new Month[12]; 

            for (int month = 1; month <= 12; month++)
            {
                double avg = 0;
                var selectedMonth = yearData.AsEnumerable().Where(s => s.Date.Month == month).ToList();

                foreach (WeatherInfo day in selectedMonth)
                {
                    avg += day.Temperature;
                }
                
                string monthName = "Jan";
                int daysInMonth = 31;
                switch (month)
                {
                    case 1:
                        monthName = "Jan";
                        break;
                    case 2:
                        monthName = "Feb";
                        daysInMonth = days == 366 ? 29 : 28;
                        break;
                    case 3:
                        monthName = "Mar";
                        break;
                    case 4:
                        monthName = "Apr";
                        daysInMonth = 30;
                        break;
                    case 5:
                        monthName = "May";
                        break;
                    case 6:
                        monthName = "Jun";
                        daysInMonth = 30;
                        break;
                    case 7:
                        monthName = "Jul";
                        daysInMonth = 30;
                        break;
                    case 8:
                        monthName = "Aug";
                        break;
                    case 9:
                        monthName = "Sep";
                        daysInMonth = 30;
                        break;
                    case 10:
                        monthName = "Oct";
                        break;
                    case 11:
                        monthName = "Nov";
                        daysInMonth = 30;
                        break;
                    default:
                        monthName = "Dec";
                        break;
                }
                
                double roundedTemp = Math.Round(avg / daysInMonth, 2);
                string tempImage = "avg.png";
                if (roundedTemp < 40)
                {
                    tempImage = "cold.png";
                } else if (roundedTemp > 80)
                {
                    tempImage = "hot.png";
                }
                Month newMonth = new Month
                {
                    temperature = roundedTemp,
                    monthName = monthName,
                    image = tempImage
                };
                months[month - 1] = newMonth;
            }
            monthLV.ItemsSource = months;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            template = CreateTemplate();
            monthLV.ItemTemplate = template;
        }

        DataTemplate CreateTemplate()
        {
            DataTemplate r = new DataTemplate(() => {
                Label nameLabel = new Label();
                nameLabel.SetBinding(Label.TextProperty, "monthName");
                nameLabel.FontSize = 20;
                nameLabel.TextColor = Color.Blue;

                Image imageView = new Image();
                imageView.SetBinding(Image.SourceProperty, "image");
                imageView.HeightRequest = 50;

                Label tempLabel = new Label();
                tempLabel.SetBinding(Label.TextProperty, "temperature");
                tempLabel.FontSize = 20;
                tempLabel.TextColor = Color.Blue;

                Grid grid = new Grid();
                grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) });
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(40, GridUnitType.Absolute) });
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(280, GridUnitType.Absolute) });
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(60, GridUnitType.Absolute) });
                grid.Children.Add(imageView, 0, 0);
                grid.Children.Add(nameLabel, 1, 0);
                grid.Children.Add(tempLabel, 2, 0);

                return new ViewCell
                {
                    View = grid
                };
            });
            return r;
        }
    }
}