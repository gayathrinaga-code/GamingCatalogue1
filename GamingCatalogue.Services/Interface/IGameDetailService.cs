using GamingCatalogue.Core.Model;

namespace GamingCatalogue.Services.Interface
{

	public interface IGameDetailService	
	{
		Task<bool> CreateGameAsync(GameDetail gameDetails);

		Task<IEnumerable<GameDetail>> GetAllGamesAsync();

		Task<GameDetail> GetGameByIdAsync(int gameId);

		Task<bool> UpdateGameAsync(GameDetail gameDetails);

		Task<bool> DeleteGameAsync(int gameId);
	}
}
