using System.Reflection;

using System.ComponentModel.DataAnnotations;

public class Student {
    public Student() {
        Name = "John Doe";
        DOB = new DateOnly(2000, 1, 1);
    }
    public string Name { get; set; }
    public DateOnly DOB { get; set; }
    public override string ToString() {
        return Name + " " + DOB;
    }
}

public class School {
    public School() {
        Name = "default school";
        StudentBody = 0;
    }

    public string Name { get; set; }
    public int StudentBody { get; set; }
    public override string ToString() {
        return Name + " " + StudentBody;
    }
}

public class Reflection {
    public static void Display(string message, Object obj, string propName) {
        Console.WriteLine(message + ": " + obj);
        Type type = obj.GetType();

        PropertyInfo? propInfo = type.GetProperty(propName);
        if (propInfo != null) {
            Console.WriteLine(propName + "=" + propInfo.GetValue(obj));
            Console.WriteLine(propInfo.PropertyType);
        } else {
            Console.WriteLine(propName + " not defined");
        }
        Console.WriteLine("=============");
    }
    public static void Main(string[] args) {
        Student s1 = new Student { Name = "John Doe", DOB = new DateOnly(2003, 2, 28) };
        Student s2 = new Student { Name = "Jane Doe", DOB = new DateOnly(2005, 12, 25) };
        School miami = new School { Name = "Miami University", StudentBody = 16000 };

        Display("Student 1 - Name", s1, "Name");
        Display("Student 2 - Name", s2, "Name");
        Display("Miami - Name", miami, "Name");

        Display("Student 1 - DOB", s1, "DOB");
        Display("Student 2 - DOB", s2, "DOB");
        Display("Miami - DOB", miami, "DOB");
    }
}