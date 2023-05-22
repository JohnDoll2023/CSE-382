using System;
using Xamarin.Forms;

namespace SecondApp
{
    class MainPage : ContentPage
    {
        Label additionLabel;
        Label firstLabel;
        Entry firstNumber;
        Label secondLabel;
        Entry secondNumber;
        Button update;
        Label dateLabel;
        Label switchLabel;
        Switch s;
        Label stepperLabel;
        Stepper stepper;
        Label sliderLabel;
        Slider slider;
        Label progressLabel;
        ProgressBar pb;
        public MainPage()
        {
            additionLabel = new Label();
            firstLabel = new Label() { Text = "First Number" };
            firstNumber = new Entry();
            secondLabel = new Label() { Text = "Second Number" };
            secondNumber = new Entry();
            update = new Button() { Text = "BUTTON TEXT" };
            update.BackgroundColor = Color.LightGray;

            dateLabel = new Label() { Text = "Date picker" };
            DatePicker datePicker = new DatePicker();
            datePicker.Date = new DateTime(1999, 12, 31);
            
            switchLabel = new Label() { Text = "Switch" };
            s = new Switch();
            s.IsToggled = true;
            
            
            s.HorizontalOptions = LayoutOptions.End;

            stepperLabel = new Label() { Text = "Stepper" };
            stepper = new Stepper();

            sliderLabel = new Label() { Text = "Slider" };
            slider = new Slider();

            progressLabel = new Label() { Text = "Progress bar" };
            pb = new ProgressBar();

            StackLayout panel = new StackLayout();
            panel.Children.Add(additionLabel);
            panel.Children.Add(firstLabel);
            panel.Children.Add(firstNumber);
            panel.Children.Add(secondLabel);
            panel.Children.Add(secondNumber);
            panel.Children.Add(update);
            panel.Children.Add(dateLabel);
            panel.Padding = new Thickness(0, 40, 0, 0);
            panel.Children.Add(datePicker);
            panel.Children.Add(switchLabel);
            panel.Children.Add(s);
            panel.Children.Add(stepperLabel);
            panel.Children.Add(stepper);
            panel.Children.Add(sliderLabel);
            panel.Children.Add(slider);
            panel.Children.Add(progressLabel);
            panel.Children.Add(pb);
            this.Content = panel;

            update.Clicked += OnClick;
        }
        private void OnClick(object sender, EventArgs e)
        {
            try
            {
                additionLabel.Text = (double.Parse(firstNumber.Text) + double.Parse(secondNumber.Text)).ToString();
            }
            catch
            {
                additionLabel.Text = "Must enter two numbers";
            }
        }
    }
}
