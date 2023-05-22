using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ResponsiveLayoutsExercise
{
    public partial class MainPage : ContentPage
    {
        private bool currentOrientationLandscape;
        public MainPage()
        {
            InitializeComponent();
        }
        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);
            bool isNowLandscape = width > height;
            if (isNowLandscape != currentOrientationLandscape)
            {
                top.Orientation = isNowLandscape ? StackOrientation.Horizontal : StackOrientation.Vertical;
                middle.Orientation = isNowLandscape ? StackOrientation.Horizontal : StackOrientation.Vertical;
                bottom.Orientation = isNowLandscape ? StackOrientation.Horizontal : StackOrientation.Vertical;
                currentOrientationLandscape = isNowLandscape;
            }
        }
    }
}

