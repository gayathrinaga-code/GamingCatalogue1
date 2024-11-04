
namespace GamingCatalogue.Core.Model
{
	public class GameDetail
	{
		public int Id { get; set; }
		public required string Title { get; set; }
		public required string Description { get; set; }
		public required string Platform { get; set; }
		public string? DevelopedBy { get; set; }
		public int AgeLevel { get; set; }
		public float Game_Price { get; set; }

	}
}
