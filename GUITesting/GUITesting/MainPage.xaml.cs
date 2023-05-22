using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace GUITesting
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        void Button_Clicked(System.Object sender, System.EventArgs e)
        {
            int v1, v2;
            bool ok1 = Int32.TryParse(entry1.Text, out v1);
            if (!ok1) v1 = 0;
            bool ok2 = Int32.TryParse(entry2.Text, out v2);
            if (!ok2) v2 = 0;
            sumLabel.Text = (v1 + v2).ToString();
        }
    }
}
