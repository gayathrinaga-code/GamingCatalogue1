using GamingCatalogue.Core.Interface;
using GamingCatalogue.Core.Model;
using GamingCatalogue.Infrastructure;
using GamingCatalogue.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;


namespace GamingCatalogue.UnitTest
{
	public class UnitOfWorkTests
	{
		private GamingCatalogueDbContext _context;
		private IUnitOfWork _unitOfWork;

		[SetUp]
		public void Setup()
		{
			var options = new DbContextOptionsBuilder<GamingCatalogueDbContext>()
			.UseInMemoryDatabase(databaseName: "TestDatabase")
			.Options;
			_context = new GamingCatalogueDbContext(options);
			_unitOfWork = new UnitOfWork(_context);
		}

		[TearDown]
		public void TearDown()
		{
			_context.Database.EnsureDeleted();
			_context.Dispose();
			_unitOfWork.Dispose();
		}

		[Test]
		public void AddGameDetailsToRepository()
		{
			// Arrange
			var gameDetail = new GameDetail { Id = 1, Title = "ff", Description = "ss", Platform = "44" };
			_unitOfWork.GameDetails.AddAsync(gameDetail);
			_unitOfWork.SaveAsync();
			// Act
			var userFromDb = _context.GameDetails.Find(gameDetail.Id);
			// Assert
			Assert.IsNotNull(userFromDb);
		}

		[Test]
		public void updateGameDetailsToRepository()
		{
			// Arrange
			var gameDetail = new GameDetail { Id = 1, Title = "ff", Description = "ss", Platform = "44" };
			_unitOfWork.GameDetails.AddAsync(gameDetail);
			_unitOfWork.SaveAsync();

			// Act
			gameDetail.Title = "Title Changed";
			_unitOfWork.GameDetails.UpdateAsync(gameDetail);
			_unitOfWork.SaveAsync();
			var updatedFromDb = _context.GameDetails.Find(gameDetail.Id);

			// Assert
			Assert.IsNotNull(gameDetail);
			Assert.That(gameDetail.Title, Is.EqualTo("Title Changed"));
		}

		[Test]
		public void DeleteGameDetailsToRepository()
		{
			// Arrange 
			var gameDetail = new GameDetail { Id = 10, Title = "ff", Description = "ss", Platform = "44" };
			_unitOfWork.GameDetails.AddAsync(gameDetail);
			_unitOfWork.SaveAsync();

			// Act
			_unitOfWork.GameDetails.DeleteAsync(gameDetail);
			_unitOfWork.SaveAsync();
			var deletedUser = _context.GameDetails.Find(gameDetail.Id);

			// Assert
			Assert.IsNull(deletedUser);
		}

		[Test]
		public void GetAllGameDetailsToRepository()
		{
			// Arrange 
			var gameDetail1 = new GameDetail { Id = 1, Title = "ff", Description = "ss", Platform = "44" };
			_unitOfWork.GameDetails.AddAsync(gameDetail1);
			_unitOfWork.SaveAsync();

			var gameDetail2 = new GameDetail { Id = 2, Title = "fff", Description = "sss", Platform = "44" };
			_unitOfWork.GameDetails.AddAsync(gameDetail2);
			_unitOfWork.SaveAsync();

			var gameDetail3 = new GameDetail { Id = 3, Title = "ff", Description = "ss", Platform = "44" };
			_unitOfWork.GameDetails.AddAsync(gameDetail3);
			_unitOfWork.SaveAsync();

			// Act
   
			var GetAllList = _context.GameDetails.Count();

			// Assert
			Assert.IsNotNull(GetAllList);
			Assert.That(GetAllList, Is.EqualTo(3));
		}

	}
}
