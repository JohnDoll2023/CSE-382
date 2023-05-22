using System;
using SQLite;
namespace DBIntro
{
    [Table("person")]
    public class Person
    {
        // PrimaryKey is typically numeric 
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int Id { get; set; }

        [MaxLength(250)]
        public string name { get; set; }
        public DateTime date { get; set; }
        [MaxLength(9), Unique]
        public string Ssn { get; set; }
        public override string ToString()
        {
            return string.Format("{0} ({1}) {2} {3}", name, Id, date.ToShortDateString(), Ssn);
        }
    }
}

