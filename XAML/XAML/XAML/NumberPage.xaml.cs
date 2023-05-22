using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace XAML
{
    public partial class NumberPage : ContentPage
    {
        public NumberPage()
        {
            InitializeComponent();
        }

        void Button_Clicked(System.Object sender, System.EventArgs e)
        {
            Button b = (Button)sender;
            if (b.Text == "Clear")
            {
                nLabel.Text = "0";
            }
            else if (nLabel.Text == "0")
            {
                nLabel.Text = b.Text;
            }
            else
            {
                nLabel.Text += b.Text;
            }
        }
    }
}

