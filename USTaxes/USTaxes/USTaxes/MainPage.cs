using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Xamarin.Forms;

namespace USTaxes
{
    public class MainPage : ContentPage
    {
        // Create visual elements
        USTaxes taxes;
        Button option;
        Entry dollarAmount;
        Entry state;
        Entry city;
        Button submit;
        StackLayout resultsPanel;
        Label zipLabel;
        Label cityLabel;
        Label stateLabel;
        Label taxReturnLabel;

        // Modify and set visual elements
        public MainPage(string fileName = "zipcodes.tsv")
        {
            var assembly = IntrospectionExtensions.GetTypeInfo(typeof(Location)).Assembly;
            string namespaceName = "USTaxes";
            Stream stream = assembly.GetManifestResourceStream(namespaceName + "." + fileName);
            taxes = new USTaxes(new StreamReader(stream));

            option = new Button { Text = "Find city/state" };
            option.Clicked += Option_Clicked;

            dollarAmount = new Entry { Text = "Enter Dollar Amount"};
            dollarAmount.Focused += DollarAmount_Focused;

            state = new Entry { Text = "Enter State" };
            state.IsVisible = false;
            state.Focused += State_Focused;

            city = new Entry { Text = "Enter City" };
            city.Focused += City_Focused;
            city.IsVisible = false;

            submit = new Button { Text = "Submit" };
            submit.Clicked += Submit_Clicked;

            StackLayout panel = new StackLayout();
            panel.Children.Add(option);
            panel.Children.Add(dollarAmount);
            panel.Children.Add(state);
            panel.Children.Add(city);
            panel.Children.Add(submit);

            StackLayout titles = new StackLayout { Orientation = StackOrientation.Horizontal };
            zipLabel = new Label { Text = "Zip" };
            cityLabel = new Label { Text = "City" };
            stateLabel = new Label { Text = "State" };
            taxReturnLabel = new Label { Text = "Avg Tax Return" };
            titles.Children.Add(zipLabel);
            titles.Children.Add(cityLabel);
            titles.Children.Add(stateLabel);
            titles.Children.Add(taxReturnLabel);
            titles.Spacing = 40;
            panel.Children.Add(titles);

            panel.Padding = 40;

            resultsPanel = new StackLayout();

            ScrollView results = new ScrollView { VerticalScrollBarVisibility = ScrollBarVisibility.Always };
            results.Content = resultsPanel;
            panel.Children.Add(results);
            this.Content = panel;
        }

        // Clear text box if dollar amount clicked on
        private void DollarAmount_Focused(object sender, FocusEventArgs e)
        {
            if (dollarAmount.Text.Equals("Enter Dollar Amount"))
            {
                dollarAmount.Text = "";
            }
        }

        // Clear text box if state entry clicked on
        private void State_Focused(object sender, FocusEventArgs e)
        {
            if (state.Text.Equals("Enter State"))
            {
                state.Text = "";
            }
        }

        // Clear text box if city entry clicked on
        private void City_Focused(object sender, FocusEventArgs e)
        {
            if (city.Text.Equals("Enter City"))
            {
                city.Text = "";
            }
        }

        // logic for submit button
        public void Submit_Clicked(object sender, EventArgs e)
        {
            // clear previous panel results
            resultsPanel.Children.Clear();

            // logic for if dollar amount is selected by user
            if (option.Text.Equals("Find city/state"))
            {
                if (dollarAmount.Text.Length == 0)
                {
                    resultsPanel.Children.Add(new Label { Text = "You must enter a dollar amount!" });
                }
                else
                {
                    double userEntry;

                    // try to parse dollar amount
                    try
                    {
                        userEntry = double.Parse(dollarAmount.Text);
                        SortedSet<Location> closeTR = taxes.getTR(userEntry);
                        if (closeTR.Count == 0)
                        {
                            resultsPanel.Children.Add(new Label { Text = "No matches found" });
                        }
                        else
                        {
                            foreach (Location l in closeTR)
                            {
                                Label zipMatch;
                                Label cityMatch;
                                Label stateMatch;
                                Label taxReturnMatch;
                                StackLayout match = new StackLayout { Orientation = StackOrientation.Horizontal };

                                // if 0, then mark as red
                                if (l.getAvgTaxReturn() == 0.0)
                                {
                                    //match.TextColor = System.Drawing.Color.Red;
                                    zipMatch = new Label { Text = l.getZip().ToString() };
                                    zipMatch.TextColor = System.Drawing.Color.Red;
                                    cityMatch = new Label { Text = l.getCity() };
                                    cityMatch.TextColor = System.Drawing.Color.Red;
                                    stateMatch = new Label { Text = l.getState() };
                                    stateMatch.TextColor = System.Drawing.Color.Red;
                                    taxReturnMatch = new Label { Text = l.getAvgTaxReturn().ToString() };
                                    taxReturnMatch.TextColor = System.Drawing.Color.Red;
                                    match.Children.Add(zipMatch);
                                    match.Children.Add(cityMatch);
                                    match.Children.Add(stateMatch);
                                    match.Children.Add(taxReturnMatch);
                                    match.Spacing = 20;
                                }
                                else
                                {
                                    zipMatch = new Label { Text = l.getZip().ToString() };
                                    cityMatch = new Label { Text = l.getCity() };
                                    stateMatch = new Label { Text = l.getState() };
                                    taxReturnMatch = new Label { Text = l.getAvgTaxReturn().ToString() };
                                    match.Children.Add(zipMatch);
                                    match.Children.Add(cityMatch);
                                    match.Children.Add(stateMatch);
                                    match.Children.Add(taxReturnMatch);
                                    match.Spacing = 20;
                                }
                                resultsPanel.Children.Add(match);
                            }
                        }
                    }
                    catch
                    {
                        resultsPanel.Children.Add(new Label { Text = "Invalid dollar amount" });
                    }
                }
            }

            // do city/state lookup
            else
            {
                string userState = state.Text;
                string userCity = city.Text;
                if (userState.Length == 0 || userCity.Length == 0)
                {
                    resultsPanel.Children.Add(new Label { Text = "You must enter a city and state!" });
                }
                else if (userState.Length != 2)
                {
                    resultsPanel.Children.Add(new Label { Text = "Invalid State" });
                }
                else {
                    SortedSet<Location> matchCityState = taxes.getCS(userState, userCity);
                    if (matchCityState.Count == 0)
                    {
                        resultsPanel.Children.Add(new Label { Text = "No matches found, make sure your entries are valid" });
                    } else
                    {
                        foreach (Location l in matchCityState)
                        {
                            Label zipMatch;
                            Label cityMatch;
                            Label stateMatch;
                            Label taxReturnMatch;
                            StackLayout match = new StackLayout { Orientation = StackOrientation.Horizontal };

                            // if tax return is 0, then mark as red
                            if (l.getAvgTaxReturn() == 0.0)
                            {
                                zipMatch = new Label { Text = l.getZip().ToString() };
                                zipMatch.TextColor = System.Drawing.Color.Red;
                                cityMatch = new Label { Text = l.getCity() };
                                cityMatch.TextColor = System.Drawing.Color.Red;
                                stateMatch = new Label { Text = l.getState() };
                                stateMatch.TextColor = System.Drawing.Color.Red;
                                taxReturnMatch = new Label { Text = l.getAvgTaxReturn().ToString() };
                                taxReturnMatch.TextColor = System.Drawing.Color.Red;
                                match.Children.Add(zipMatch);
                                match.Children.Add(cityMatch);
                                match.Children.Add(stateMatch);
                                match.Children.Add(taxReturnMatch);
                                match.Spacing = 20;
                            }
                            else
                            {
                                zipMatch = new Label { Text = l.getZip().ToString() };
                                cityMatch = new Label { Text = l.getCity() };
                                stateMatch = new Label { Text = l.getState() };
                                taxReturnMatch = new Label { Text = l.getAvgTaxReturn().ToString() };
                                match.Children.Add(zipMatch);
                                match.Children.Add(cityMatch);
                                match.Children.Add(stateMatch);
                                match.Children.Add(taxReturnMatch);
                                match.Spacing = 20;
                            }
                            resultsPanel.Children.Add(match);
                        }
                    }
                }
            }
        }

        // switch between dollar amount search and city/state search
        public void Option_Clicked(object sender, EventArgs e)
        {
            if (option.Text.Equals("Find city/state"))
            {
                option.Text = "Calculate Tax";
                state.IsVisible = true;
                city.IsVisible = true;
                dollarAmount.IsVisible = false;
            }
            else
            {
                option.Text = "Find city/state";
                state.IsVisible = false;
                city.IsVisible = false;
                dollarAmount.IsVisible = true;
            }
        }
    }
}