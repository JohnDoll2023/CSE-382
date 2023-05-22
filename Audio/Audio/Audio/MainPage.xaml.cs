using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Plugin.SimpleAudioPlayer;
using System.IO;
using System.Reflection;

namespace Audio {
	// Learn more about making custom code visible in the Xamarin.Forms previewer
	// by visiting https://aka.ms/xamarinforms-previewer
	[DesignTimeVisible(false)]
	public partial class MainPage : ContentPage {
		ISimpleAudioPlayer player = CrossSimpleAudioPlayer.CreateSimpleAudioPlayer();
		public MainPage() {
			InitializeComponent();
			Load("start.mp3");
		}
		private void Load(string file) {
			var assembly = typeof(App).GetTypeInfo().Assembly;
			String ns = "Audio";
			Stream audioStream = assembly.GetManifestResourceStream(ns + "." + file);
			player.Load(audioStream);
		}
		private void OnClicked(object sender, EventArgs e) {
            Button b = (Button)sender;
            if (b.Text.Equals("Play"))
            {
                player.Play();
            }
            else
            {
                if (player.IsPlaying)
                {
                    player.Stop();
                }
            }
        }

        void loop_Toggled(System.Object sender, Xamarin.Forms.ToggledEventArgs e)
        {
            if (loop.IsToggled)
            {
                player.Loop = true;
            }
            else
            {
                player.Loop = false;
            }
        }
    }
}
