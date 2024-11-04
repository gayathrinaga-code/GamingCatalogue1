using GamingCatalogue.Core.Model;
using Microsoft.EntityFrameworkCore;

namespace GamingCatalogue.Infrastructure
{
	public class GamingCatalogueDbContext : DbContext
	{
		public GamingCatalogueDbContext(DbContextOptions<GamingCatalogueDbContext> contextOptions) : base(contextOptions)
		{

		}

		public DbSet<GameDetail> GameDetails { get; set; }
	}
}
