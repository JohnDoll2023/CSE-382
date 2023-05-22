using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MarginAndPadding {
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ControlsPage : ContentPage {
		public ControlsPage() {
			InitializeComponent();
		}
		private void PaddingChanged(object sender, ValueChangedEventArgs e) {
			paddingLabel.Text = "Padding = " + paddingSlider.Value;
		}
		private void MarginChanged(object sender, ValueChangedEventArgs e) {
			redBox1.Margin = marginSlider.Value;
			greenBox1.Margin = marginSlider.Value;
			blueBox1.Margin = marginSlider.Value;
			redBox2.Margin = marginSlider.Value;
			greenBox2.Margin = marginSlider.Value;
			blueBox2.Margin = marginSlider.Value;
			redBox3.Margin = marginSlider.Value;
			greenBox3.Margin = marginSlider.Value;
			blueBox3.Margin = marginSlider.Value;
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