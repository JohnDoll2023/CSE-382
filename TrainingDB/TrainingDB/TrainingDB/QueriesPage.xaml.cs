using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TrainingDB {
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class QueriesPage : ContentPage {
		public QueriesPage() {
			InitializeComponent();
		}
		private void OnClicked(object sender, EventArgs e) {
			TimeSpan hour = new TimeSpan(1, 0, 0);
            var theTable = DB.conn.Table<Activity>();
            Button b = (Button)sender;
            if (b == HrRunAll)
            {
                lv.ItemsSource = theTable.Where(s => s.Sport.Equals("Running") && s.Duration >= hour).ToList();
            }
            else if (b == HrRunDates)
            {
                lv.ItemsSource = theTable.Where(s => s.Sport.Equals("Running") && s.Duration >= hour).Select(s => s.DatePerformed).ToList();
            }
            else if (b == HrRunDateDuration)
            {
                lv.ItemsSource = theTable.Where(s => s.Sport.Equals("Running") && s.Duration >= hour).Select(s => new Tuple<DateTime, TimeSpan>(s.DatePerformed, s.Duration)).ToList();
            }
            else if (b == LRunAll)
            {
                var table2 = DB.conn.Table<LongActivityDefinition>();
                TimeSpan ts = table2.Where(s => s.Sport.Equals("Running")).Select(s => s.Duration).FirstOrDefault();
                lv.ItemsSource = theTable.Where(s => s.Sport.Equals("Running") && s.Duration >= ts).ToList();
            }
            else if (b == Calories)
            {
                lv.ItemsSource = theTable.AsEnumerable().Where(s => s.GetCalories() > 500).ToList();
            }
        }
	}
}