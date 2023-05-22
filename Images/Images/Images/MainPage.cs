using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using Xamarin.Forms;

namespace Images {
    public class MainPage : ContentPage {
        Image imTop;
        Image imMiddle;
        Image imBottom;
        ImageButton button;
        public static IPlatformSpecificFunctions platformSpecfics;
        public MainPage() {
            if (Device.RuntimePlatform == Device.iOS)
                this.Padding = new Thickness(0, 21, 0, 0);

            imTop = new Image { HeightRequest = 50 };
            imTop.Source = "thisos.jpg";
            imMiddle = new Image { HeightRequest = 100 };
            if (Device.RuntimePlatform == Device.iOS)
                imMiddle.Source = "iosbook.jpg";
            else if (Device.RuntimePlatform == Device.Android)
                imMiddle.Source = "androidbook.jpg";
            else 
                imMiddle.Source = "windowsbook.jpg";

            imBottom = new Image {
                HeightRequest = 100,
                Source = "mobile.jpg"
            };

            Label label = new Label();

            Stream creditsStream = platformSpecfics.GetStreamForFilename("credits.txt");
            StreamReader reader = new StreamReader(creditsStream);
            label.Text = reader.ReadLine();
            label.FontSize = 14;
            creditsStream.Close();

            StackLayout panel = new StackLayout();
            panel.Children.Add(imTop);
            panel.Children.Add(imMiddle);
            panel.Children.Add(imBottom);
            panel.Children.Add(label);
            this.Content = panel;
        }
    }
}
