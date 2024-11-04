using GamingCatalogue.Core.Interface;
using GamingCatalogue.Core.Model;
using GamingCatalogue.Services.Interface;


namespace GamingCatalogue.Services
{
	public class GameDetailService : IGameDetailService
	{
		public IUnitOfWork _unitOfWork;

		public GameDetailService(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public async Task<bool> CreateGameAsync(GameDetail gameDetails)
		{
			if (gameDetails != null)
			{
				await _unitOfWork.GameDetails.AddAsync(gameDetails);
				return await _unitOfWork.SaveAsync();

			}
			return false;
		} 

		public async Task<bool> DeleteGameAsync(int gameId)
		{
			if (gameId > 0)
			{
				var gameDetails = await _unitOfWork.GameDetails.GetByIdAsync(gameId);
				if (gameDetails != null)
				{
					_unitOfWork.GameDetails.DeleteAsync(gameDetails);
					return await _unitOfWork.SaveAsync();
				}
			}
			return false;
		}

		public async Task<IEnumerable<GameDetail>> GetAllGamesAsync()
		{
			var gameDetailsList = await _unitOfWork.GameDetails.GetAllAsync();
			return gameDetailsList;
		}

		public async Task<GameDetail?> GetGameByIdAsync(int gameId)
		{
			if (gameId > 0)
			{
				var gameDetails = await _unitOfWork.GameDetails.GetByIdAsync(gameId);
				if (gameDetails != null)
				{
					return gameDetails;
				}
			}
			return null;
		}

		public async Task<bool> UpdateGameAsync(GameDetail gameDetails)
		{
			if (gameDetails != null)
			{
				var game = await _unitOfWork.GameDetails.GetByIdAsync(gameDetails.Id);
				if (game != null)
				{
					game.Title = gameDetails.Title;
					game.Description = gameDetails.Description;
					game.Platform = gameDetails.Platform;
					game.AgeLevel = gameDetails.AgeLevel;

					_unitOfWork.GameDetails.UpdateAsync(game);

					return await _unitOfWork.SaveAsync();


				}
			}
			return false;
		}
	}
}
