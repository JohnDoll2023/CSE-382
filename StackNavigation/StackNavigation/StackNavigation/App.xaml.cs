﻿using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StackNavigation {
    public partial class App : Application {
        public App() {
            InitializeComponent();
            Page mainPage = new MainPage();
            MainPage = new NavigationPage(mainPage);
        }
        protected override void OnStart() {
            // Handle when your app starts
        }
        protected override void OnSleep() {
            // Handle when your app sleeps
        }
        protected override void OnResume() {
            // Handle when your app resumes
        }
    }
}
