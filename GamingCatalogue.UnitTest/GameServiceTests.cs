using GamingCatalogue.Core.Interface;
using GamingCatalogue.Core.Model;
using GamingCatalogue.Services;
using GamingCatalogue.Services.Interface;
using Moq;


namespace GamingCatalogue.UnitTest
{
	public class GameServiceTests
	{
		private Mock<IUnitOfWork> _unitOfWorkMock;
		private IGameDetailService _gameService;

		[SetUp]
		public void SetUp()
		{
			_unitOfWorkMock = new Mock<IUnitOfWork>();
			_gameService = new GameDetailService(_unitOfWorkMock.Object);
		}


		[Test]
		public void GetAllGames_FromService()
		{
			// Arrange
			var gameDetails = new List<GameDetail> { new GameDetail { Id = 1, Title="Test1",Description= "TestDescr1", Platform="P1" },
				new GameDetail { Id = 2, Title = "Test2", Description = "TestDescr2", Platform = "P2" } };
			_unitOfWorkMock.Setup(u => u.GameDetails.GetAllAsync()).ReturnsAsync(gameDetails);

			// Act
			var result = _gameService.GetAllGamesAsync();

			// Assert
			Assert.That(result.Result.Count(), Is.EqualTo(2));
			_unitOfWorkMock.Verify(u => u.GameDetails.GetAllAsync(), Times.Once);
		}

		[Test]
		public void GetUserById_FromService()
		{
			// Arrange
			var gameDetails = new GameDetail { Id = 1, Title = "Test1", Description = "TestDescr1", Platform = "P1" };
			_unitOfWorkMock.Setup(u => u.GameDetails.GetByIdAsync(gameDetails.Id)).ReturnsAsync(gameDetails);

			// Act
			var result = _gameService.GetGameByIdAsync(gameDetails.Id);

			// Assert
			Assert.IsNotNull(result);
			Assert.That(result.Result.Id, Is.EqualTo(gameDetails.Id));
		}

		[Test]
		public void DeleteGame_FromService()
		{
			// Arrange
			var gameDetails = new GameDetail { Id = 1, Title = "Test1", Description = "TestDescr1", Platform = "P1" };
			_unitOfWorkMock.Setup(u => u.GameDetails.GetByIdAsync(gameDetails.Id)).ReturnsAsync(gameDetails);

			// Act
			_gameService.DeleteGameAsync(gameDetails.Id);

			// Assert
			_unitOfWorkMock.Verify(u => u.GameDetails.DeleteAsync(gameDetails), Times.Once);
		}
	}
}
