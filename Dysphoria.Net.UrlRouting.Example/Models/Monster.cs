namespace Dysphoria.Net.UrlRouting.Test.Models
{
	public class Monster
	{
		public int Id { get; set; }

		public int? IdOrNull { get { return this.Id == 0 ? (int?)null : this.Id; } }

		public string Name { get; set; }

		public string Description { get; set; }

		public bool IsDangerous { get; set; }

		public string[] Categories { get; set; }
	}
}