using System;
namespace RunningApp
{
    public class Totals
    {
        public DateTime DatePerformed { get; set; }
        public TimeSpan Duration { get; set; }
        public double Distance { get; set; }
        public int HeartRate { get; set; }
        public override string ToString()
        {
            if (SettingsPage.metric)
            {
                return string.Format("Week={0} Time={1} Distance={2} HR={3}", DatePerformed.ToShortDateString(), Duration, Distance * 1.609, HeartRate);
            }
            else
            {
                return string.Format("Week={0} Time={1} Distance={2} HR={3}", DatePerformed.ToShortDateString(), Duration, Distance, HeartRate);
            }
        }
    }
}