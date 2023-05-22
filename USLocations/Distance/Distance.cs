using System;
using USLocations;
using Distance;

namespace Distance
{
    public class Distance
    {
        public static void Main(String[] args)
        {
            USLocations.USLocations usl = new USLocations.USLocations("zipcodes.tsv");
            string entry = Console.ReadLine();
            while (!entry.Equals("exit"))
            {
                string[] toks = entry.Split(" ");
                double miles = Math.Round(usl.Distance(int.Parse(toks[0]), int.Parse(toks[1])), 2);
                double km = Math.Round(miles * 1.609, 2);

                Console.WriteLine("The distance between {0} and {1} is {2} miles ({3} km)", toks[0], toks[1], miles, km);

                entry = Console.ReadLine();
            }
            Console.WriteLine("Goodbye!");
        }
    }
}

