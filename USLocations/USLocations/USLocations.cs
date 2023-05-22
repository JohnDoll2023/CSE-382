namespace USLocations
{
    public class USLocations
    {
        private Task inputTask;         // Task for reading file
        private List<Location> locations;   // Read locations info here

        // This constructor will initiate the loading of the TSV file.
        // The constructor must return very quickly, perhaps before all 
        // of the zip code information has been completely loaded. Tasks
        // will be needed to do this.
        public USLocations(string fileName)
        {
            //inputTask = call async method for reading file
            inputTask = ReadFileAsync(fileName);
        }

        public async Task ReadFileAsync(string fileName)
        {      // Asynchronous method for reading file
            locations = new List<Location>();
            using (StreamReader input = new StreamReader(fileName))
            {
                input.ReadLine();
                while (!input.EndOfStream)
                {
                    string tok = await input.ReadLineAsync();
                    string[] toks = tok.Split("\t");
                    Location location;
                    if (toks[6].Length == 0 || toks[7].Length == 0)
                    {
                        location = new Location(int.Parse(toks[1]), toks[3], toks[4], 0, 0);
                    }
                    else
                    {
                        location = new Location(int.Parse(toks[1]), toks[3], toks[4], double.Parse(toks[6]), double.Parse(toks[7]));
                    }
                    locations.Add(location);
                }
            }
        }

        /**
         * Returns the city names that appear in both of the given states.
             * "OH" and "MI" would yield {OXFORD, FRANKLIN, ... }
         */
        public ISet<string> GetCommonCityNames(string state1, string state2)
        {
            // Cycle through locations to find cities common to both states.
            // Before doing this, you may need to wait for the reading to complete.
            ISet<string> set1 = new SortedSet<string>();
            ISet<string> set2 = new SortedSet<string>();
            inputTask.Wait();
            foreach (Location l in locations)
            {
                if (l.getState().Equals(state1)) {
                    set1.Add(l.getCity());
                }
                else if (l.getState().Equals(state2))
                {
                    set2.Add(l.getCity());
                }
            }

            ISet<string> set3 = new SortedSet<string>();
            foreach (string city in set1)
            {
                if (set2.Contains(city))
                {
                    set3.Add(city);
                }
            }

            return set3;
        }
        /**
            * Returns all zipcodes that are within a specified distance from a
            * particular zipcode.
            */
        // Do this by researching the "Haversine" formula to do this one.
        // The formula for converting from degrees to radians is:
        //     radians = degrees * Math.PI / 180.0;
        public double Distance(int zip1, int zip2)
        {
            // Use Haversine to compute distance between two locations.
            // Before doing this, you may need to wait for the reading to complete.

            // get locations
            Location l1 = null;
            Location l2 = null;

            inputTask.Wait();
            foreach(Location l in locations)
            {
                if (l1 == null && l.getZip() == zip1)
                {
                    l1 = l;
                    if (l2 != null)
                    {
                        break;
                    }
                }
                else if (l2 == null && l.getZip() == zip2)
                {
                    l2 = l;
                    if (l1 != null)
                    {
                        break;
                    }
                }
            }

            // get latitude and longitude
            double dLat = toRadian(l1.getLatitude() - l2.getLatitude());
            double dLon = toRadian(l1.getLongitude() - l2.getLongitude());

            double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                    Math.Cos(toRadian(l1.getLatitude())) *
                    Math.Cos(toRadian(l2.getLatitude())) *
                    Math.Sin(dLon / 2) * Math.Sin(dLon / 2);

            return 3960 * 2 * Math.Asin(Math.Min(1, Math.Sqrt(a)));
        }

        private double toRadian(double num)
        {
            return num * Math.PI / 180;
        }
    }

    public class Location
    {
        int zipcode;
        string cityName;
        string stateCode;
        double latitude;
        double longitude;

        public Location(int zipcode = 0, string cityName = "", string stateCode = "", double latitude = 0, double longitude = 0)
        {
            this.zipcode = zipcode;
            this.cityName = cityName;
            this.stateCode = stateCode;
            this.latitude = latitude;
            this.longitude = longitude;
        }

        public int getZip()
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
    }
}
