using GamingCatalogue.Core.Interface;
using Microsoft.EntityFrameworkCore;

namespace GamingCatalogue.Infrastructure.Repositories
{
	public abstract class GenericRepository<T> : IGenericRepository<T> where T : class
	{
		protected readonly GamingCatalogueDbContext _dbContext;

		protected GenericRepository(GamingCatalogueDbContext context)
		{
			_dbContext = context;
		}

		public async Task<T> GetByIdAsync(int id)
		{
			return await _dbContext.Set<T>().FindAsync(id);
		}

		public async Task<IEnumerable<T>> GetAllAsync()
		{
			return await _dbContext.Set<T>().ToListAsync();
		}

		public async Task AddAsync(T entity)
		{
			await _dbContext.Set<T>().AddAsync(entity);
		}

		public void DeleteAsync(T entity)
		{
			_dbContext.Set<T>().Remove(entity);
		}

		public void UpdateAsync(T entity)
		{
			_dbContext.Set<T>().Update(entity);
		}
	}
}
