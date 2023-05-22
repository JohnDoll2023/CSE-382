using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MarginAndPadding {
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CellPage : ContentPage {
		public CellPage() {
			InitializeComponent();
		}
		private void PaddingChanged(object sender, ValueChangedEventArgs e) {
			double value = paddingSlider.Value;
			if (value < 0)
                value = 0;
			Thickness thick = new Thickness(value);
			cell00.Padding = thick;
			cell01.Padding = thick;
			cell10.Padding = thick;
			cell11.Padding = thick;
			cell20.Padding = thick;
			cell21.Padding = thick;
			paddingLabel.Text = "Padding = " + value;
		}
		private void MarginChanged(object sender, ValueChangedEventArgs e) {
			cell00.Margin = new Thickness(marginSlider.Value);
			cell01.Margin = new Thickness(marginSlider.Value);
			cell10.Margin = new Thickness(marginSlider.Value);
			cell11.Margin = new Thickness(marginSlider.Value);
			cell20.Margin = new Thickness(marginSlider.Value);
			cell21.Margin = new Thickness(marginSlider.Value);
			marginLabel.Text = "Margin = " + marginSlider.Value;
		}
		private void SpacingChanged(object sender, ValueChangedEventArgs e) {
			spacingLabel.Text = "Spacing = " + spacingSlider.Value;
		}
		private void ResetClicked(object sender, EventArgs e) {
			paddingSlider.Value = 0;
			marginSlider.Value = 0;
			spacingSlider.Value = 0;
		}
	}
}