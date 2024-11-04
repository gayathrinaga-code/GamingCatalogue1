using GamingCatalogue.API.Controllers;
using GamingCatalogue.Core.Model;
using GamingCatalogue.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;


namespace GamingCatalogue.UnitTest
{
	public class GameControllerTests
	{
		private Mock<IGameDetailService> _gameDetailServiceMock;
        private Mock<ILogger<GamesController>> _mocklogger; 
        private GamesController _controller;

		[SetUp]
		public void SetUp()
		{
            _mocklogger = new Mock<ILogger<GamesController>>();
            _gameDetailServiceMock = new Mock<IGameDetailService>();
			_controller = new GamesController(_mocklogger.Object, _gameDetailServiceMock.Object);

        }

		[Test]
		public  async Task GetAllGames_ReturnsOk()
		{
			// Arrange
			var gameDetails = new List<GameDetail> { new GameDetail { Id = 1, Title="Test1",Description= "TestDescr1", Platform="P1" },
				new GameDetail { Id = 2, Title = "Test2", Description = "TestDescr2", Platform = "P2" } };

			_gameDetailServiceMock.Setup(u => u.GetAllGamesAsync()).ReturnsAsync(gameDetails);

			// Act
			var result = await _controller.GetGamesList();

			//  Assert
			Assert.IsInstanceOf<OkObjectResult>(result); 
			var okResult = result as OkObjectResult; 
			Assert.IsNotNull(okResult); 
			Assert.AreEqual(2, ((IEnumerable<GameDetail>)okResult.Value).Count());

		}

		[Test]
		public async Task CreateGame_ReturnsCreatedAtAction()
		{
			// Arrange
			var gameDetails = new GameDetail { Id = 1, Title = "Test1", Description = "TestDescr1", Platform = "P1" };
			_gameDetailServiceMock.Setup(s => s.CreateGameAsync(gameDetails)).ReturnsAsync(true);

			// Act
			var result = await _controller.CreateGame(gameDetails);

			// Assert
			Assert.IsInstanceOf<CreatedAtActionResult>(result);
		}

		[Test]
		public async Task DeleteUser_ReturnsOkay()
		{
			// Arrange
			var gameDetails = new GameDetail { Id = 1, Title = "Test1", Description = "TestDescr1", Platform = "P1" };
			_gameDetailServiceMock.Setup(s => s.GetGameByIdAsync(gameDetails.Id)).ReturnsAsync(gameDetails);
			_gameDetailServiceMock.Setup(s => s.DeleteGameAsync(gameDetails.Id)).ReturnsAsync(true);

			// Act
			var result = await _controller.DeleteGame(gameDetails.Id);

			// Assert
			Assert.IsInstanceOf<OkObjectResult>(result);
		}

	}
}
