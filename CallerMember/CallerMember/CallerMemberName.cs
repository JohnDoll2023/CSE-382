using System;
using System.Runtime.CompilerServices;

namespace CallerMember {
	public class CallerMemberName {
		public static void f([CallerMemberName] string memberName = "",
								[CallerFilePath] string filePath = "",
								[CallerLineNumber] int sourceLine = 0) {
			Console.WriteLine("Caller: {0}", memberName);
			Console.WriteLine("File: {0}", filePath);
			Console.WriteLine("#: {0}", sourceLine);
		}
		public static void g() {
			f();
		}
		public static void Main(string[] args) {
			f();
			g();
		}
	}
}
/* Output:
Caller: Main
File: C:\Users\zmuda\OneDrive\Desktop\Courses\CSE470\CallerMember\CallerMember\Program.cs
#: 17
Caller: g
File: C:\Users\zmuda\OneDrive\Desktop\Courses\CSE470\CallerMember\CallerMember\Program.cs
#: 14
*/
