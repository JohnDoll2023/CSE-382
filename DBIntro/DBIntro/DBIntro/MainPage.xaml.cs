using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace DBIntro
{
    public partial class MainPage : ContentPage
    {
        SQLiteConnection conn;
        public MainPage()
        {
            InitializeComponent();
            string libFolder = FileSystem.AppDataDirectory;
            string fname = System.IO.Path.Combine(libFolder, "Personnel.db");
            //File.Delete(fname);
            conn = new SQLiteConnection(fname);
            conn.CreateTable<User>();
            conn.CreateTable<Person>();
            //conn.DeleteAll<User>();
            //conn.DeleteAll<Person>();
            //conn.Execute("DELETE FROM User");
            //conn.Execute("DELETE FROM Person");
            lv.ItemsSource = conn.Table<User>().ToList();
            personLV.ItemsSource = conn.Table<Person>().ToList();
        }
        private void Button_Clicked(object sender, EventArgs e)
        {
            User newUser = new User { Username = name.Text };
            conn.Insert(newUser);
            lv.ItemsSource = conn.Table<User>().ToList();
        }

        void Button_Clicked_1(System.Object sender, System.EventArgs e)
        {
            Person newPerson = new Person { name = person.Text, date = date.Date, Ssn = ssn.Text };
            conn.Insert(newPerson);
            personLV.ItemsSource = conn.Table<Person>().ToList();
        }
    }
}