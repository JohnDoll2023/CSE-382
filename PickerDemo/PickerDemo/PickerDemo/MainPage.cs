using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace PickerDemo {

	public class School
	{
		public String name;
		public String url;

		public School(String name, String url)
		{
			this.name = name;
			this.url = url;
		}

        public override string ToString()
        {
			return name;
        }
    }
	public class MainPage : ContentPage {
		Picker schoolPicker;
		WebView webView;
		public MainPage() {
			StackLayout panel = new StackLayout();

			List<School> schools = new List<School>();
			schools.Add(new School("Miami", "https:////www.miamioh.edu"));
			schools.Add(new School("OSU", "https:////www.osu.edu"));
			schools.Add(new School("UC", "https:////www.uc.edu"));
			schools.Add(new School("OU", "https:////www.ohio.edu"));
			schoolPicker = new Picker();
			schoolPicker.ItemsSource = schools;
			schoolPicker.SelectedIndex = 1;
			
			schoolPicker.SelectedIndexChanged += SchoolPicker_SelectedIndexChanged;

			webView = new WebView
			{
				Source = new UrlWebViewSource
				{
					Url = schools[1].url

				},
				VerticalOptions = LayoutOptions.FillAndExpand,
                HeightRequest = 300
			};
			panel.Children.Add(schoolPicker);
			panel.Children.Add(webView);
			Content = panel;
		

			//sizePicker = new Picker();
			//sizePicker.Items.Add("X Small");
			//sizePicker.Items.Add("Small");
			//sizePicker.Items.Add("Medium");
			//sizePicker.Items.Add("Large");
			//sizePicker.Items.Add("X Large");
			//sizePicker.SelectedIndex = 2;
			//sizePicker.SelectedIndexChanged += SizePicker_SelectedIndexChanged;

			//colorPicker = new Picker();
			//colorPicker.Items.Add("Red");
			//colorPicker.Items.Add("Green");
			//colorPicker.Items.Add("Blue");
			//colorPicker.SelectedIndex = 2;
			//colorPicker.SelectedIndexChanged += ColorPicker_SelectedIndexChanged;

			//studentPicker = new Picker();
			//List<Student> students = new List<Student>();
			//students.Add(new Student("Maria", 6));
			//students.Add(new Student("Alberto", 5));
			//students.Add(new Student("Greta", 3));
			//studentPicker.ItemsSource = students;
			//studentPicker.SelectedIndex = 0;
			//studentPicker.SelectedIndexChanged += StudentPicker_SelectedIndexChanged;

			//StackLayout sizePanel = new StackLayout();
			//sizePanel.Children.Add(sizeLabel);
			//sizePanel.Children.Add(sizePicker);

			//StackLayout colorPanel = new StackLayout();
			//colorPanel.Children.Add(colorLabel);
			//colorPanel.Children.Add(colorPicker);

			//StackLayout studentPanel = new StackLayout();
			//studentPanel.Children.Add(studentLabel);
			//studentPanel.Children.Add(studentPicker);

			//Frame sizeFrame = new Frame();
			//sizeFrame.Content = sizePanel;
			//sizeFrame.BorderColor = Color.Black;

			//Frame colorFrame = new Frame();
			//colorFrame.Content = colorPanel;
			//colorFrame.BorderColor = Color.Black;

			//Frame studentFrame = new Frame();
			//studentFrame.Content = studentPanel;
			//studentFrame.BorderColor = Color.Black;

			//panel.Children.Add(sizeFrame);
			//panel.Children.Add(colorFrame);
			//panel.Children.Add(studentFrame);
			//Content = panel;
		}

        private void SchoolPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            Picker picker = (Picker)sender;
			School whichSchool = (School)picker.SelectedItem;
			webView.Source = new UrlWebViewSource { Url = whichSchool.url };
			//	Student whichStudent = (Student)picker.SelectedItem;
			//studentLabel.Text = whichStudent.ToString();
		}

        //private void StudentPicker_SelectedIndexChanged(object sender, EventArgs e) {
        //	Picker picker = (Picker)sender;
        //	Student whichStudent = (Student)picker.SelectedItem;
        //	studentLabel.Text = whichStudent.ToString();
        //}
        //private void SizePicker_SelectedIndexChanged(object sender, EventArgs e) {
        //	sizeLabel.Text = (String)sizePicker.SelectedItem;
        //}
        //private void ColorPicker_SelectedIndexChanged(object sender, EventArgs e) {
        //	colorLabel.Text = (String)colorPicker.SelectedItem;
        //}
    }
}
