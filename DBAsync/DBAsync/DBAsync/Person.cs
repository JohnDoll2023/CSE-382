using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace DBAsync
{
    [Table("person")]
    public class Person
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        [Unique]
        public string SSN { get; set; }
        public override string ToString()
        {
            return string.Format("{0} of {1} ({2})", Name, City, SSN);
        }
    }
}
