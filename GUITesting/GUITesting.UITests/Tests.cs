using System;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace GUITesting.UITests {
	[TestFixture(Platform.Android)]
	//   [TestFixture(Platform.iOS)]
	public class Tests {
		IApp app;
		Platform platform;

		public Tests(Platform platform) {
			this.platform = platform;
		}

		[SetUp]
		public void BeforeEachTest() {
			app = AppInitializer.StartApp(platform);
			Console.WriteLine("?");
		}

		[Test]
		[TestCase(2, 3)]
		[TestCase(22, 300)]
		[TestCase(-22, -300)]
		public void Test1(int a, int b) {
			// Arrange
			app.EnterText("FirstEntry", a.ToString());
			app.EnterText("SecondEntry", b.ToString());

			// Act
			app.Tap("AddButton");

			// Assert
			int sum = a + b;
			var result = app.Query("SummationLabel").First(control => control.Text == sum.ToString());
			Assert.NotNull(result, "Problem with summation");
		}
	}
}
