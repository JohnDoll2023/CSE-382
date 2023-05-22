using SQLite;

namespace SportStat
{
	[Table("teams")]
	public class Team
	{

        [PrimaryKey, AutoIncrement]
		public int Id { get; set; }
		public string Name { get; set; }
		public int Wins { get; set; }
		public int Losses { get; set; }
		public string Sport { get; set; }
		public string Image { get; set; }
        public override string ToString()
        {
			return string.Format("{0} {1}-{2}", Name, Wins, Losses);
        }
		public string Record
		{
			get
			{
				return string.Format("{0}-{1}", Wins, Losses);
			}
		}
    }
}