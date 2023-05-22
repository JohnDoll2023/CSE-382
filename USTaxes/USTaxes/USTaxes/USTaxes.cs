using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace USTaxes
{
    public class USTaxes : ContentPage
    {
        // create task and locations variables
        Task task = null;
        List<Location> locations;

        // USTaxes constructor to hold locations and track task
        public USTaxes(StreamReader stream)
        {
            locations = new List<Location>();
            task = GetFileContentsAsync(stream);
        }

        // read the contents of the given file
        private async Task GetFileContentsAsync(StreamReader dictReader)
        {
            dictReader.ReadLine();
            while (!dictReader.EndOfStream)
            {
                string tok = await dictReader.ReadLineAsync().ConfigureAwait(false);
                string[] toks = tok.Split('\t');
                Location location;
                if (toks[6].Length == 0 || toks[7].Length == 0 || toks[16].Length == 0 || toks[18].Length == 0)
                {
                    location = new Location(toks[1], toks[3], toks[4], 0, 0, 1, 0);
                }
                else
                {
                    location = new Location(toks[1], toks[3], toks[4], double.Parse(toks[6]), double.Parse(toks[7]), int.Parse(toks[16]), long.Parse(toks[18]));
                }
                locations.Add(location);
            }
        }

        // get close tax returns from user given dollar amount
        public SortedSet<Location> getTR(double userEntry) 
        {
            if (task != null && !task.IsCompleted)
            {
                Debug.WriteLine("Need to wait");
                task.Wait();
            }
            SortedSet<Location> ret = new SortedSet<Location>();
            foreach(Location l in locations)
            {
                if (Math.Abs(l.getAvgTaxReturn() - userEntry) < 100)
                {
                    ret.Add(l);
                }
            }
            return ret;
        }

        // get matching city and state tax returns
        public SortedSet<Location> getCS(string userState, string userCity)
        {
            if (task != null && !task.IsCompleted)
            {
                Debug.WriteLine("Need to wait");
                task.Wait();
            }
            SortedSet<Location> ret = new SortedSet<Location>();
            foreach (Location l in locations)
            {
                if (l.getState().Equals(userState) && l.getCity().Equals(userCity.ToUpper()))
                {
                    ret.Add(l);
                }
            }
            return ret;
        }
    }

    // Location class
    public class Location : IComparable
    {
        string zipcode;
        string cityName;
        string stateCode;
        double latitude;
        double longitude;
        int taxReturns;
        long totalWages;
        double avgTaxReturn;

        public int CompareTo(object obj)
        {
            Location l = obj as Location;
            return this.getZip().CompareTo(l.getZip());
        }

        public Location(string zipcode = "00000", string cityName = "", string stateCode = "", double latitude = 0, double longitude = 0, int taxReturns = 1, long totalWages = 0)
        {
            if (zipcode.Length < 5)
            {
                while (zipcode.Length < 5)
                {
                    zipcode = "0" + zipcode;
                }
            }
            this.zipcode = zipcode;
            this.cityName = cityName;
            this.stateCode = stateCode;
            this.latitude = latitude;
            this.longitude = longitude;
            this.taxReturns = taxReturns;
            this.totalWages = totalWages;
            this.avgTaxReturn = totalWages / taxReturns;
        }

        public string getZip()
        {
            return zipcode;
        }

        public string getCity()
        {
            return cityName;
        }

        public string getState()
        {
            return stateCode;
        }

        public double getLatitude()
        {
            return latitude;
        }

        public double getLongitude()
        {
            return longitude;
        }

        public int getTaxReturns()
        {
            return taxReturns;
        }

        public long getTotalWages()
        {
            return totalWages;
        }

        public double getAvgTaxReturn()
        {
            return avgTaxReturn;
        }
    }
}