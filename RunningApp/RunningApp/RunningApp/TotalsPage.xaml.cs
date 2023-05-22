using System;
using System.Collections.Generic;
using System.Globalization;
using Xamarin.Forms;

namespace RunningApp
{
    public partial class TotalsPage : ContentPage
    {
        List<Totals> totalsList;
        public TotalsPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            List<Activity> activities = DB.conn.Table<Activity>().ToList();
            totalsList = new List<Totals>();

            if (activities.Count > 0)
            {
                // total duration of activities for a week
                TimeSpan duration = new TimeSpan(0, 0, 0);
                // average heartrate added up
                int hRate = 0;
                // total distance run
                double distance = 0;
                // num of activities for the week
                int count = 1;
                // the previous week num to check if same week
                int prevWeek = 0;
                DateTime dt = new DateTime(2000, 1, 1);
                DateTime prevDt = new DateTime(2000, 1, 1);
                for (int i = 0; i < activities.Count; i++)
                {
                    
                    // get date of the activity performed
                    dt = activities[i].DatePerformed;
                    // get the weeknum
                    int weekNum = CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(
                        activities[i].DatePerformed,
                        CalendarWeekRule.FirstFourDayWeek,
                        DayOfWeek.Monday);

                    // creates the correct date for display in the totalsList
                    DayOfWeek day = CultureInfo.CurrentCulture.Calendar.GetDayOfWeek(activities[i].DatePerformed);
                    switch (day)
                    {
                        case DayOfWeek.Sunday:
                            dt = dt.AddDays(-6);
                            break;
                        case DayOfWeek.Monday:
                            break;
                        case DayOfWeek.Tuesday:
                            dt = dt.AddDays(-1);
                            break;
                        case DayOfWeek.Wednesday:
                            dt = dt.AddDays(-2);
                            break;
                        case DayOfWeek.Thursday:
                            dt = dt.AddDays(-3);
                            break;
                        case DayOfWeek.Friday:
                            dt = dt.AddDays(-4);
                            break;
                        case DayOfWeek.Saturday:
                            dt = dt.AddDays(-5);
                            break;
                        default:
                            break;
                    }

                    // check if in the same week
                    if (weekNum == prevWeek || i == 0)
                    {
                        // not adding new entry, totaling up dates
                        prevDt = dt;
                        duration += activities[i].Duration;
                        double mins = activities[i].Duration.TotalMinutes;
                        hRate += (int)(activities[i].HeartRate * mins);
                        distance += activities[i].Distance;
                        // increment num of activities for the week
                        count++;
                    }
                    else
                    {
                        // creating total object for the week
                        Totals total = new Totals
                        {
                            DatePerformed = prevDt,
                            Duration = new TimeSpan(0, (int)duration.TotalMinutes, 0),
                            HeartRate = (hRate / (int)duration.TotalMinutes),
                            Distance = distance
                        };
                        bool found = false;

                        // checking to see if this week already exists in the totalsList
                        for (int j = 0; j < totalsList.Count; j++)
                        {
                            if (totalsList[j].DatePerformed == prevDt)
                            {
                                // if the week exists, then edit that week
                                totalsList[j].Distance += total.Distance;
                                double totalMins = totalsList[j].Duration.TotalMinutes;
                                totalsList[j].Duration += total.Duration;
                                double allMins = totalMins + duration.TotalMinutes;
                                int totalHeartRate = (int)(((totalsList[j].HeartRate * totalMins) + hRate)/allMins);
                                totalsList[j].HeartRate = totalHeartRate;
                                found = true;
                                break;
                            }
                        }

                        // if the week didn't exist, then add it to the list
                        if (!found)
                        {
                            totalsList.Add(total);
                        }

                        // add the new data to start a new week
                        duration = activities[i].Duration;
                        double mins = duration.TotalMinutes;
                        distance = activities[i].Distance;
                        hRate = (int)(activities[i].HeartRate * mins);
                        count = 1;
                        prevDt = dt;
                    }
                    prevWeek = weekNum;
                }

                // outside of for loop that loops thru all activities, if there is still
                // data left, add it in
                if (distance > 0)
                {
                    // create new totals object
                    Totals total = new Totals
                    {
                        DatePerformed = prevDt,
                        Duration = new TimeSpan(0, (int)duration.TotalMinutes, 0),
                        HeartRate = (hRate / (int)duration.TotalMinutes),
                        Distance = distance
                    };

                    bool found = false;
                    // loop thru to see if this week already exists
                    for (int j = 0; j < totalsList.Count; j++)
                    {
                        if (totalsList[j].DatePerformed == dt)
                        {
                            // if the week exists, then edit that week
                            totalsList[j].Distance += total.Distance;
                            double totalMins = totalsList[j].Duration.TotalMinutes;
                            totalsList[j].Duration += total.Duration;
                            double allMins = totalMins + duration.TotalMinutes;
                            int totalHeartRate = (int)(((totalsList[j].HeartRate * totalMins) + hRate) / allMins);
                            totalsList[j].HeartRate = totalHeartRate;
                            found = true;
                            break;
                        }
                    }

                    // if the week doesnt already exist, then add it to totalsList
                    if (!found)
                    {
                        totalsList.Add(total);
                    }
                }

                // sort the totalsList
                totalsList.Sort(delegate (Totals x, Totals y)
                {
                    return x.DatePerformed.CompareTo(y.DatePerformed);
                });

                // set the item source to the totalsList
                ResetListViewSources();
            }
        }

        private void ResetListViewSources()
        {
            totals.ItemsSource = totalsList;
        }
    }
}