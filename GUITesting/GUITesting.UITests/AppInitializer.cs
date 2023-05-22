using System;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace GUITesting.UITests {
	public class AppInitializer {
		public static IApp StartApp(Platform platform) {
			IApp app = null;
			try {
                if (platform == Platform.Android)
                {
                    app = ConfigureApp.Android.InstalledApp("miamioh.guitesting").StartApp();
                }
                else if (platform == Platform.iOS)
                {
                    app = ConfigureApp.iOS.StartApp();
                }
            }
            catch (Exception e) {
                Console.WriteLine(e.ToString());
            }

            return app;
		}
	}
}