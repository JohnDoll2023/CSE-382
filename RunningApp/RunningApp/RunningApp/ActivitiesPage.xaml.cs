using System;
using System.IO;
using System.Reflection;
using System.Diagnostics;
using System.Collections.Generic;
using SQLite;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;

namespace RunningApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ActivitiesPage : ContentPage
    {
        public ActivitiesPage()
        {
            InitializeComponent();
            for (int h = 0; h < 12; h++)
                hours.Items.Add(h.ToString());
            hours.SelectedIndex = 0;
            for (int m = 0; m < 60; m++)
                mins.Items.Add(m.ToString());
            mins.SelectedIndex = 21;
            for (int i = 0; i < 99; i++)
            {
                measurement1.Items.Add(i.ToString());
            }
            measurement1.SelectedIndex = 0;
            for (int i = 0; i < 10; i++)
            {
                measurement2.Items.Add(i.ToString());
            }
            measurement2.SelectedIndex = 0;
            for (int i = 0; i < 400; i++)
            {
                heartrate.Items.Add(i.ToString());
            }
            heartrate.SelectedIndex = 100;
            lvActivities.ItemsSource = DB.conn.Table<Activity>().ToList();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ResetListViewSources();
            if (SettingsPage.metric)
            {
                measurementLabel.Text = "Distance KM";
            }
            else
            {
                measurementLabel.Text = "Distance Miles";
            }
        }

        private void ResetListViewSources()
        {
            lvActivities.ItemsSource = DB.conn.Table<Activity>().ToList();
        }

        async void Button_Clicked(System.Object sender, System.EventArgs e)
        {
            Button button = (Button)sender;
            if (button.Text.Equals("Add"))
            {
                double distance = double.Parse((string)measurement1.SelectedItem);
                distance += (double.Parse((string)measurement2.SelectedItem) / 10);

                TimeSpan ts = new TimeSpan(Int32.Parse((string)hours.SelectedItem),
                                            Int32.Parse((string)mins.SelectedItem), 0);

                if (distance == 0 || ts.TotalMinutes == 0)
                {
                    string action = await DisplayActionSheet("Invalid input", "Ok", null, "Distance and Duration must be greater than 0");
                }
                else
                {
                    if (SettingsPage.metric)
                    {
                        distance *= .621;
                    }
                    Activity activity = new Activity
                    {
                        DatePerformed = dp.Date,
                        Distance = distance,
                        Duration = ts,
                        HeartRate = Int32.Parse((string)heartrate.SelectedItem)
                    };
                    DB.conn.Insert(activity);
                    ResetListViewSources();
                }
            }
            else if (button.Text.Equals("Update"))
            {
                Activity oldActivity = lvActivities.SelectedItem as Activity;
                double distance = double.Parse((string)measurement1.SelectedItem);
                distance += (double.Parse((string)measurement2.SelectedItem) / 10);
                TimeSpan ts = new TimeSpan(Int32.Parse((string)hours.SelectedItem),
                                            Int32.Parse((string)mins.SelectedItem), 0);

                if (distance == 0 || ts.TotalMinutes == 0)
                {
                    string action = await DisplayActionSheet("Invalid input", "Ok", null, "Distance and Duration must be greater than 0");
                }
                else
                {

                    if (SettingsPage.metric)
                    {
                        distance *= 1.609;
                    }
                    Activity newActivity = new Activity
                    {
                        DatePerformed = dp.Date,
                        Distance = distance,
                        Duration = ts,
                        HeartRate = Int32.Parse((string)heartrate.SelectedItem)
                    };
                    newActivity.Id = oldActivity.Id;
                    DB.conn.Update(newActivity);
                    ResetListViewSources();
                }
            }
            else
            {
                Activity activity = lvActivities.SelectedItem as Activity;
                if (activity != null)
                {
                    int v = DB.conn.Delete(activity);
                    if (v > 0)
                    {
                        lvActivities.SelectedItem = null;
                        ResetListViewSources();
                    }
                }
            }
        }

        private void ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Activity activity = lvActivities.SelectedItem as Activity;
            if (activity != null)
            {
                dp.Date = activity.DatePerformed;
                hours.SelectedItem = activity.Duration.Hours.ToString();
                mins.SelectedItem = activity.Duration.Minutes.ToString();
                heartrate.SelectedItem = activity.HeartRate.ToString();
                Double dis = activity.Distance;
                int firstValue = (int)dis;
                double secondValue = ((dis - firstValue) * 10);
                measurement1.SelectedItem = firstValue.ToString();
                measurement2.SelectedItem = secondValue.ToString();
            }
        }
    }
}