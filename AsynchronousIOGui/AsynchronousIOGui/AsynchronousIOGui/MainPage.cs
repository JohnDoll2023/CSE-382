using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

using Xamarin.Forms;

namespace AsynchronousIOGui
{
    class MainPage : ContentPage
    {
        EnglishDictionary words;
        Entry letters;
        Label hasBeenFound;
        public MainPage()
        {
            var assembly = IntrospectionExtensions.GetTypeInfo(typeof(MainPage)).Assembly;
            // The string to be passed into GetManifestResourceStream must include the namespace
            // followed by a "." and then the file name.
            Stream stream = assembly.GetManifestResourceStream("AsynchronousIOGui.words.txt");
            words = new EnglishDictionary(new StreamReader(stream));
            Debug.WriteLine("?");
            StackLayout panel = new StackLayout();

            letters = new Entry { Text = "apples" };
            hasBeenFound = new Label { Text = "-------" };
            Button b = new Button { Text = "Lookup" };
            b.Clicked += OnFindClicked;

            panel.Children.Add(letters);
            panel.Children.Add(hasBeenFound);
            panel.Children.Add(b);
            panel.Padding = 72;

            this.Content = panel;
        }

        private void OnFindClicked(object sender, EventArgs e)
        {
            hasBeenFound.Text = words.IsLegal(letters.Text).ToString();
        }
    }
}