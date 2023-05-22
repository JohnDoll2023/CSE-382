using SQLite;

namespace SportStat
{
	[Table("players")]
	public class Player
	{
		[PrimaryKey, AutoIncrement]
		public int Id { get; set; }
		public string Name { get; set; }
		public int Age { get; set; }
		public int GamesPlayed { get; set; }
		public string ClassStanding { get; set; }
		public int Goals { get; set; }
		public int Assists { get; set; }
		public string Gender { get; set; }
		public int Runs { get; set; }
		public int RBI { get; set; }
		public string Team { get; set; }
		public string Sport { get; set; }
        public override string ToString()
        {
			return string.Format("{0}", Name);
        }
    }
}