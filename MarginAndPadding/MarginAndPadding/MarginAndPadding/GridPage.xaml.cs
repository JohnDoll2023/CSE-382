using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MarginAndPadding {
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class GridPage : ContentPage {
		public GridPage() {
			InitializeComponent();
		}
		private void PaddingChanged(object sender, ValueChangedEventArgs e) {
			gridLayout.Padding = new Thickness(paddingSlider.Value);
			paddingLabel.Text = "Padding = " + paddingSlider.Value;
		}
		private void MarginChanged(object sender, ValueChangedEventArgs e) {
			gridLayout.Margin = new Thickness(marginSlider.Value);
			marginLabel.Text = "Margin = " + marginSlider.Value;
		}
		private void SpacingChanged(object sender, ValueChangedEventArgs e) {
//			gridLayout.Spacing = spacingSlider.Value;
			spacingLabel.Text = "Spacing = " + spacingSlider.Value;
		}
		private void ResetClicked(object sender, EventArgs e) {
			paddingSlider.Value = 0;
			marginSlider.Value = 0;
			spacingSlider.Value = 0;
		}
	}
}