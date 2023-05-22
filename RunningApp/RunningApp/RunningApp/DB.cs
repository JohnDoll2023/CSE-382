using System;
using SQLite;
using System.IO;
using System.Reflection;
using Xamarin.Essentials;

namespace RunningApp
{
    public class DB
    {
        private static string DBName = "log.db";
        public static SQLiteConnection conn;
        public static void OpenConnection()
        {
            string libFolder = FileSystem.AppDataDirectory;
            string fname = System.IO.Path.Combine(libFolder, DBName);
            //File.Delete(fname);
            conn = new SQLiteConnection(fname);
            conn.CreateTable<Activity>();
        }

        public static void DeleteTableContents(string tableName)
        {
            int v = conn.Execute("DELETE FROM " + tableName);
        }

        public static void RepopulateTables()
        {
            LoadActivitiesAsync();
        }

        public static async void LoadActivitiesAsync()
        {
            try
            {
                DeleteTableContents("activity");
                var assembly = IntrospectionExtensions.GetTypeInfo(typeof(MainPage)).Assembly;
                Stream stream = assembly.GetManifestResourceStream("TrainingDB.log.txt");
                StreamReader input = new StreamReader(stream);
                while (!input.EndOfStream)
                {
                    string line = await input.ReadLineAsync();
                    Activity activity = Activity.ParseCSV(line);
                    conn.Insert(activity);
                }
            }
            catch (Exception e)
            {
            }
        }
    }
}