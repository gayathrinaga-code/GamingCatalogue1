

namespace GamingCatalogue.Core.Interface
{
	public interface IGenericRepository<T> where T : class
	{
		Task<T> GetByIdAsync(int id);
		Task<IEnumerable<T>> GetAllAsync();
		Task AddAsync(T entity);
		void DeleteAsync(T entity);
		void UpdateAsync(T entity);
	}
}
