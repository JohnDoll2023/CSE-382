using System;
namespace Common
{
    public class Common
    {
        public static void Main(String[] args)
        {
            USLocations.USLocations usl = new USLocations.USLocations("zipcodes.tsv");
            
            string entry = Console.ReadLine();
            while (!entry.Equals("exit"))
            {
                string[] toks = entry.Split(" ");
                ISet<string> common = usl.GetCommonCityNames(toks[0], toks[1]);

                foreach (string city in common)
                {
                    Console.WriteLine(city);
                }
                entry = Console.ReadLine();
            }
            Console.WriteLine("Goodbye!");
        }
    }
}

