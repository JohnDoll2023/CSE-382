using System;

using Xamarin.Forms;

namespace ControlsandEvents
{
    public class MainPage : ContentPage
    {
        Entry e;
        Label eLabel;
        Button b1;
        Label b1Label;
        int count = 0;
        Switch s;
        Label sLabel;
        CheckBox cb;
        Label cbLabel;
        Stepper st;
        Label stLabel;
        Slider sl;
        Label slLabel;
        public MainPage()
        {
            StackLayout topLevel = new StackLayout();

            e = new Entry();
            //e.Width = 100;
            e.WidthRequest = 100;
            eLabel = new Label { Text = "0" };
            StackLayout firstPanel = new StackLayout { Orientation = StackOrientation.Horizontal };
            firstPanel.Children.Add(e);
            firstPanel.Children.Add(eLabel);
            e.TextChanged += E_TextChanged;

            b1 = new Button { Text = "Click me" };
            b1Label = new Label { Text = "0" };
            StackLayout secondPanel = new StackLayout { Orientation = StackOrientation.Horizontal };
            secondPanel.Children.Add(b1);
            secondPanel.Children.Add(b1Label);
            b1.Clicked += B1_Clicked;

            s = new Switch();
            s.IsToggled = true;
            sLabel = new Label { Text = "on" };
            StackLayout thirdPanel = new StackLayout { Orientation = StackOrientation.Horizontal };
            thirdPanel.Children.Add(s);
            thirdPanel.Children.Add(sLabel);
            s.Toggled += S_Toggled;

            cb = new CheckBox { IsChecked = true };
            cbLabel = new Label { Text = "checked" };
            StackLayout fourthPanel = new StackLayout { Orientation = StackOrientation.Horizontal };
            fourthPanel.Children.Add(cb);
            fourthPanel.Children.Add(cbLabel);
            cb.CheckedChanged += Cb_CheckedChanged;

            st = new Stepper();
            st.Increment = 0.1;
            st.Value = 0.5;
            st.Maximum = 1;
            st.Minimum = 0;
            stLabel = new Label { Text = "0.5" };
            StackLayout fifthPanel = new StackLayout { Orientation = StackOrientation.Horizontal };
            fifthPanel.Children.Add(st);
            fifthPanel.Children.Add(stLabel);
            st.ValueChanged += St_ValueChanged;

            sl = new Slider();
            sl.Value = 0.5;
            sl.Maximum = 1;
            sl.Minimum = 0;
            slLabel = new Label { Text = "0.5" };
            StackLayout sixthPanel = new StackLayout { Orientation = StackOrientation.Horizontal };
            sixthPanel.Children.Add(sl);
            sixthPanel.Children.Add(slLabel);
            sl.ValueChanged += Sl_ValueChanged;

            topLevel.Children.Add(firstPanel);
            topLevel.Children.Add(secondPanel);
            topLevel.Children.Add(thirdPanel);
            topLevel.Children.Add(fourthPanel);
            topLevel.Children.Add(fifthPanel);
            topLevel.Children.Add(sixthPanel);
            topLevel.Padding = new Thickness(0, 40, 0, 0);

            Content = topLevel;
        }

        private void Sl_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            double x = sl.Value;
            slLabel.Text = x.ToString();
        }

        private void B1_Clicked(object sender, EventArgs e)
        {
            count++;
            b1Label.Text = count.ToString();
        }

        private void St_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            stLabel.Text = st.Value.ToString();
        }

        private void E_TextChanged(object sender, TextChangedEventArgs e)
        {
            eLabel.Text = e.NewTextValue.Length.ToString();
        }

        private void S_Toggled(object sender, ToggledEventArgs e)
        {
            sLabel.Text = s.IsToggled ? "on" : "off";
        }

        private void Cb_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            cbLabel.Text = cb.IsChecked ? "checked" : "unchecked";
        }
    }
}


