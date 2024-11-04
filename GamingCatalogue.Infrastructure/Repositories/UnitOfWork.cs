using GamingCatalogue.Core.Interface;

namespace GamingCatalogue.Infrastructure.Repositories
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly GamingCatalogueDbContext _dbContext;
		public IGameDetailRepository GameDetails { get; private set; }

		public UnitOfWork(GamingCatalogueDbContext dbContext)
		{
			_dbContext = dbContext;
			GameDetails = new GameDetailRepository(_dbContext);
		}

		public async Task<bool> SaveAsync()
		{
			return await _dbContext.SaveChangesAsync() > 0;
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				_dbContext.Dispose();
			}
		}

	}
}
