using GamingCatalogue.Core.Interface;
using GamingCatalogue.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GamingCatalogue.Infrastructure
{
	public static class ServiceExtension
	{
		public static IServiceCollection AddInfraConfigServices(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddDbContext<GamingCatalogueDbContext>(options =>
			{
				options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
			});
			services.AddScoped<IUnitOfWork, UnitOfWork>();
			services.AddScoped<IGameDetailRepository, GameDetailRepository>();

			return services;
		}
	}
}
