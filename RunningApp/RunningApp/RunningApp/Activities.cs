using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace RunningApp
{

    [Table("activity")]
    public class Activity
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public DateTime DatePerformed { get; set; }
        public TimeSpan Duration { get; set; }
        public double Distance { get; set; }
        public int HeartRate { get; set; }
        public override string ToString()
        {
            if (SettingsPage.metric)
            {
                return string.Format("Distance={0} Time={1} HR={2} Date={3}", Distance * 1.609, Duration, HeartRate, DatePerformed.ToString("MM/dd/yyyy"));
            } else
            {
                return string.Format("Distance={0} Time={1} HR={2} Date={3}", Distance, Duration, HeartRate, DatePerformed.ToString("MM/dd/yyyy"));
            }
            
        }
        public static Activity ParseCSV(string line)
        {
            string[] toks = line.Split(',');
            Activity activity = new Activity
            {
                Distance = Double.Parse(toks[0]),
                Duration = TimeSpan.Parse(toks[1]),
                HeartRate = Int32.Parse(toks[2]),
                DatePerformed = DateTime.Parse(toks[3])
            };
            return activity;
        }
    }
}