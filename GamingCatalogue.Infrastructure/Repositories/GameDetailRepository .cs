using GamingCatalogue.Core.Interface;
using GamingCatalogue.Core.Model;

namespace GamingCatalogue.Infrastructure.Repositories
{

	public class GameDetailRepository : GenericRepository<GameDetail>, IGameDetailRepository
	{
		public GameDetailRepository(GamingCatalogueDbContext dbContext) : base(dbContext)
		{

		}
	}
}
