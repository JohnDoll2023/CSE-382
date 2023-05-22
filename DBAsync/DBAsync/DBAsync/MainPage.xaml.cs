using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using SQLite;
using Xamarin.Essentials;
using System.Linq.Expressions;

namespace DBAsync
{
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        SQLiteAsyncConnection conn;
        public MainPage()
        {
            InitializeComponent();
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await Task.Delay(5000); // simulate long connect time
            string libFolder = FileSystem.AppDataDirectory;
            string fname = System.IO.Path.Combine(libFolder, "SSN.db");
            conn = new SQLiteAsyncConnection(fname);
            await conn.CreateTableAsync<Person>();
            lv.ItemsSource = await conn.Table<Person>().ToListAsync();
        }
        private async void OnClicked(object sender, EventArgs e)
        {
            Person person = new Person {
                Name = name.Text,
                SSN = ssn.Text,
                City = city.Text
            };
            Person lookup = conn.Table<Person>().Where(p => p.SSN.Equals(ssn.Text)).FirstOrDefaultAsync().Result;
            if (lookup == null) {   // new SSN
                await conn.InsertAsync(person);
            } else {                // Old SSN, assume updating information
                lookup.Name = person.Name;
                lookup.City = person.City;
                await conn.UpdateAsync(lookup);
            }
            lv.ItemsSource = conn.Table<Person>().ToListAsync().Result;
        }

        private void ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (lv.SelectedItem == null) {
                name.Text = city.Text = ssn.Text = "";
            } else {
                Person p = (Person)e.Item;
                name.Text = p.Name;
                city.Text = p.City;
                ssn.Text = p.SSN;
            }
        }
    }
}
