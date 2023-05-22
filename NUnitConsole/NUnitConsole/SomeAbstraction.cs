using System;
using System.Collections.Generic;
using System.Text;

namespace NUnitConsole {
	public class SomeAbstraction {
		private int value;
		public SomeAbstraction(int v) {
			value = v;
		}
		public SomeAbstraction DoubleIt() {
			return new SomeAbstraction(2 * value);
		}
		public SomeAbstraction Add(SomeAbstraction other) {
			return new SomeAbstraction(value + other.value);
		}
		public SomeAbstraction Min(SomeAbstraction other) {  // Has error
			int smallest = 0;
			if (value < other.value)
				smallest = value;
			else if (other.value < value)
				smallest = other.value;
			return new SomeAbstraction(smallest);
		}
		public void SomeFunction() {   // Takes too long for big values
			double loops = Math.Pow(2, value);
			double sum = 0;
			for (long i = 0; i < loops; i++) {
				sum += Math.Cos(i);
			}
		}
		public int GetValue() {
			return value;
		}
		public override string ToString() {
			return value.ToString();
		}
		public static void Main1(string[] args) {
			SomeAbstraction item1 = new SomeAbstraction(3);
			SomeAbstraction item2 = new SomeAbstraction(9);

			Console.WriteLine(item1.DoubleIt());
			Console.WriteLine(item1.Min(item2));
			Console.WriteLine(item2.Min(item1));

			for (int i = -4; i <= 4; i++) {
				for (int j = i; j <= 4; j++) {
					SomeAbstraction a = new SomeAbstraction(i);
					SomeAbstraction b = new SomeAbstraction(j);
					Console.WriteLine("{0} {1}", a.Min(b), b.Min(a));
				}
			}
		}
		public static bool CheckIt(int expected, int actual) {
			return expected == actual;
		}
		public static void Main2(string[] args) {
			SomeAbstraction item1 = new SomeAbstraction(3);
			SomeAbstraction item2 = new SomeAbstraction(9);

			Console.WriteLine(CheckIt(6, item1.DoubleIt().GetValue()));
			Console.WriteLine(CheckIt(3, item1.Min(item2).GetValue()));
			Console.WriteLine(CheckIt(3, item2.Min(item1).GetValue()));

			for (int i = -4; i <= 4; i++) {
				for (int j = i; j <= 4; j++) {
					SomeAbstraction a = new SomeAbstraction(i);
					SomeAbstraction b = new SomeAbstraction(j);
					Console.WriteLine("{0} {1}", CheckIt(i, a.Min(b).GetValue()), CheckIt(i, b.Min(a).GetValue()));
				}
			}
		}
		public static void Main(string[] args) {
			//Main1(args);
			Main2(args);
		}
	}
}
