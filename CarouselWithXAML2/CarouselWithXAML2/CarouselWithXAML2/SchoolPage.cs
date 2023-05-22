using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace CarouselWithXAML2 {
	public class SchoolPage : ContentPage {
		public SchoolPage(string url) {
			WebView wv = new WebView { Source = url };
			Content = wv;
		}
	}
}
