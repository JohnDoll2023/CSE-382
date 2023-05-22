using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Essentials;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using System.Diagnostics;

namespace Graphics {
	// Learn more about making custom code visible in the Xamarin.Forms previewer
	// by visiting https://aka.ms/xamarinforms-previewer
	[DesignTimeVisible(false)]
	public partial class MainPage : ContentPage {
		Stopwatch stopwatch = new Stopwatch();
		bool pageIsActive;
		Color color = Color.Red;
		Color color2 = Color.Blue;
		public MainPage() {
			InitializeComponent();
		}
		protected async override void OnAppearing() {
			base.OnAppearing();
			pageIsActive = true;
		}
		protected override void OnDisappearing() {
			base.OnDisappearing();
			pageIsActive = false;
		}
		private static float PointsPerInch = Device.RuntimePlatform == "UWP" ? 96 : 160;
		private static float PPI = (float)DeviceDisplay.MainDisplayInfo.Density * PointsPerInch;
		private float PixelsToInches(float pixs) {
			return pixs / PPI;
		}
		private float InchesToPixels(float inches) {
			return inches * PPI;
		}

        void view1_PaintSurface(System.Object sender, SkiaSharp.Views.Forms.SKPaintSurfaceEventArgs e)
        {
            SKSurface surface = e.Surface;
            SKCanvas canvas = surface.Canvas;

            canvas.Clear();
            SKPaint paint = new SKPaint
            {
                Style = SKPaintStyle.Fill,
                Color = color.ToSKColor(),
            };

			SKPaint paint2 = new SKPaint
			{
				Style = SKPaintStyle.Fill,
				Color = color2.ToSKColor()
			};
			double first = 0;
			double.TryParse(topEntry.Text, out first);
			double second = 0;
			double.TryParse(bottomEntry.Text, out second);

            if (first >= second)
            {
				double multiplier = second / first;
				double height = -700 * multiplier;
                canvas.DrawRect(600, 700, 80, (int) height, paint2);
                canvas.DrawRect(300, 700, 80, -700, paint);
            } else if (first < second) {
				double multiplier = first / second;
                double height = -700 * multiplier;
                canvas.DrawRect(600, 700, 80, -700, paint2);
                canvas.DrawRect(300, 700, 80, (int)height, paint);
            }
        }

        void topEntry_TextChanged(System.Object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
			if (topEntry.Text.Length > 0 && bottomEntry.Text.Length > 0)
			{
				view1.InvalidateSurface();
			}
        }
    }
}