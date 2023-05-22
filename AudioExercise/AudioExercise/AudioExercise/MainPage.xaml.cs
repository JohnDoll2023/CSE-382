using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Plugin.SimpleAudioPlayer;
using Xamarin.Forms;

namespace AudioExercise
{
    public partial class MainPage : ContentPage
    {
        //bool loop = false;
        ISimpleAudioPlayer player = CrossSimpleAudioPlayer.CreateSimpleAudioPlayer();
        public MainPage()
        {
            InitializeComponent();
            Load("start.mp3");
        }

        private void Load(string file)
        {
            var assembly = typeof(App).GetTypeInfo().Assembly;
            String ns = "Audio";
            Stream audioStream = assembly.GetManifestResourceStream(ns + "." + file);
            player.Load(audioStream);
        }

        void Button_Clicked(System.Object sender, System.EventArgs e)
        {
            Button b = (Button)sender;
            if (b.Text.Equals("Start"))
            {
                player.Play();
            } else
            {
                if(player.IsPlaying)
                {
                    player.Stop();
                }
            }
        }

        void Switch_Toggled(System.Object sender, Xamarin.Forms.ToggledEventArgs e)
        {
            if (loop.IsEnabled)
            {
                player.Loop = false;
            } else
            {
                player.Loop = true;
            }
        }
    }
}

