using SQLite;
using Xamarin.Essentials;

namespace SportStat
{
    public class DB
	{
        private static string DBName = "sports.db";
        public static SQLiteConnection conn;
        public static void OpenConnection()
        {
            string libFolder = FileSystem.AppDataDirectory;
            string fname = System.IO.Path.Combine(libFolder, DBName);
            //File.Delete(fname);
            conn = new SQLiteConnection(fname);
            conn.CreateTable<Team>();
            conn.CreateTable<Player>();
            //DeleteTableContents("players");
            //DeleteTableContents("teams");
        }

        public static void DeleteTableContents(string tableName)
        {
            int v = conn.Execute("DELETE FROM " + tableName);
        }
    }
}