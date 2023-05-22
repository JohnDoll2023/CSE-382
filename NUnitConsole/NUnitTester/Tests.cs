using NUnit.Framework;
using NUnitConsole;

namespace NUnitTester {
	[TestFixture]
	public class Tests {
		[SetUp]
		public void Setup() {
		}
		[Test]
		public void Test1() {
			SomeAbstraction a = new SomeAbstraction(3);
			Assert.AreEqual(3, a.GetValue());
		}
		[Test]
		[TestCase(3)]
		[TestCase(-3)]
		[TestCase(10)]
		public void TestConstructorAndGetValue(int x) {
			SomeAbstraction a = new SomeAbstraction(x);
			Assert.AreEqual(x, a.GetValue());
		}
		[Test]
		[TestCase(3, 3, 3)]
		[TestCase(-3, -3, -3)]
		[TestCase(1, 3, 1)]
		[TestCase(-1, 3, -1)]
		[TestCase(1, -3, -3)]
		[TestCase(-1, -3, -3)]
		public void TestMin(int a, int b, int expected) {
			SomeAbstraction A = new SomeAbstraction(a);
			SomeAbstraction B = new SomeAbstraction(b);
			Assert.AreEqual(expected, A.Min(B).GetValue());
			Assert.AreEqual(expected, B.Min(A).GetValue());
		}
		[Test]
		[TestCase(10)]
		[TestCase(10), Timeout(1000)]
		[TestCase(4)]
		public void TestSomeFunction(int a) {
			SomeAbstraction A = new SomeAbstraction(a);
			A.SomeFunction();
			Assert.Pass();
		}
	}
}